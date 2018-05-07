using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Extensions.WebSockets
{
    using Streams;

    /// <summary>
    ///     A multiplexer / demultiplexer for Kubernetes-style WebSocket streams.
    /// </summary>
    /// <remarks>
    ///     Kubernetes streams add a single-byte prefix (indicating the stream index) to each payload (this can be one or more WebSocket packets, until EndOfMessage=<c>true</c>).
    /// 
    ///     For example, when using the PodV1 exec API, there are up to 3 separate streams: STDIN, STDOUT, and STDERR (with indexes 0, 1, and 2, respectively).
    /// </remarks>
    public sealed class K8sMultiplexer
        : IDisposable
    {
        /// <summary>
        ///     The default buffer size used for Kubernetes WebSockets.
        /// </summary>
        public const int DefaultBufferSize = 1024;

        /// <summary>
        ///     The default maximum number of pending bytes that input streams can contain.
        /// </summary>
        public const int DefaultMaxInputStreamBytes = 4096;

        /// <summary>
        ///     An asynchronous lock used to synchronise sending to the underlying WebSocket.
        /// </summary>
        readonly AsyncLock _sendMutex = new AsyncLock();

        /// <summary>
        ///     An asynchronous lock used to synchronise receiving from the underlying WebSocket.
        /// </summary>
        readonly AsyncLock _receiveMutex = new AsyncLock();

        /// <summary>
        ///     Input (read) streams, keyed by stream index.
        /// </summary>
        readonly Dictionary<byte, K8sMultiplexedReadStream> _inputStreams = new Dictionary<byte, K8sMultiplexedReadStream>();

        /// <summary>
        ///     Output (write) streams, keyed by stream index.
        /// </summary>
        readonly Dictionary<byte, K8sMultiplexedWriteStream> _outputStreams = new Dictionary<byte, K8sMultiplexedWriteStream>();

        /// <summary>
        ///     Pending write requests from output streams that will be interleaved and written to the WebSocket.
        /// </summary>
        readonly BlockingCollection<PendingSend> _pendingSends = new BlockingCollection<PendingSend>(new ConcurrentQueue<PendingSend>());

        /// <summary>
        ///     Task completion source for <see cref="WhenConnectionClosed"/>.
        /// </summary>
        TaskCompletionSource<object> _closeCompletion = new TaskCompletionSource<object>();

        /// <summary>
        ///     A source for cancellation tokens used to halt the multiplexer's operation.
        /// </summary>
        CancellationTokenSource _cancellationSource;
        
        /// <summary>
        ///     A <see cref="Task"/> representing the WebSocket message-send pump.
        /// </summary>
        Task _sendPump;

        /// <summary>
        ///     A <see cref="Task"/> representing the WebSocket message-receive pump.
        /// </summary>
        Task _receivePump;

        /// <summary>
        ///     Create a new <see cref="K8sMultiplexer"/>.
        /// </summary>
        /// <param name="socket">
        ///     The target WebSocket.
        /// </param>
        /// <param name="inputStreamIndexes">
        ///     An array of bytes containing the indexes of the expected input streams.
        /// </param>
        /// <param name="outputStreamIndexes">
        ///     An array of bytes containing the indexes of the expected output streams.
        /// </param>
        /// <param name="loggerFactory">
        ///     The <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </param>
        /// <param name="maxInputStreamBytes">
        ///     The maximum number of pending bytes that input streams can contain.
        /// </param>
        public K8sMultiplexer(WebSocket socket, byte[] inputStreamIndexes, byte[] outputStreamIndexes, ILoggerFactory loggerFactory, int maxInputStreamBytes = DefaultMaxInputStreamBytes)
        {
            if (socket == null)
                throw new ArgumentNullException(nameof(socket));

            if (inputStreamIndexes == null)
                throw new ArgumentNullException(nameof(inputStreamIndexes));
            
            if (outputStreamIndexes == null)
                throw new ArgumentNullException(nameof(outputStreamIndexes));   

            if (inputStreamIndexes.Length == 0 && outputStreamIndexes.Length == 0)
                throw new ArgumentException($"Must specify at least one of {nameof(inputStreamIndexes)} or {nameof(outputStreamIndexes)}.");

            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            
            Log = loggerFactory.CreateLogger<K8sMultiplexer>();
            Socket = socket;

            foreach (byte inputStreamIndex in inputStreamIndexes)
                _inputStreams[inputStreamIndex] = new K8sMultiplexedReadStream(inputStreamIndex, maxInputStreamBytes, loggerFactory);

            foreach (byte outputStreamIndex in outputStreamIndexes)
                _outputStreams[outputStreamIndex] = new K8sMultiplexedWriteStream(outputStreamIndex, EnqueueSend, loggerFactory);

            Log.LogTrace("K8sMultiplexer created with {InputStreamCount} input streams (indexes: [{InputStreamIndexes}]) and {OutputStreamCount} output streams (indexes: [{OutputStreamIndexes}]).",
                _inputStreams.Count,
                String.Join(", ", _inputStreams.Keys),
                _outputStreams.Count,
                String.Join(", ", _outputStreams.Keys)
            );
        }

        /// <summary>
        ///     Dispose of resources being used by the <see cref="K8sMultiplexer"/>.
        /// </summary>
        public void Dispose()
        {
            Log.LogTrace("Disposing...");

            try
            {
                if (_receivePump != null || _sendPump != null)
                    Shutdown().GetAwaiter().GetResult();
                else
                    _closeCompletion.TrySetResult(null);
            }
            catch (OperationCanceledException)
            {
                // Close operation timed out; nothing useful we can do here.
            }
            catch (AggregateException stopFailed)
            {
                _closeCompletion?.TrySetException(stopFailed);

                stopFailed.Flatten().Handle(exception =>
                {
                    Log.LogError(EventIds.K8sMultiplexer.DisposeStopError, stopFailed, "An unexpected error occurred during disposal of the K8sMultiplexer (failed to stop the send / receive loop).");

                    return true;
                });
            }

            Log.LogTrace("Disposing K8sMultiplexer input / output channel streams...");

            foreach (Stream inputStream in _inputStreams.Values)
                inputStream.Dispose();

            foreach (Stream outputStream in _outputStreams.Values)
                outputStream.Dispose();

            Log.LogTrace("Disposal complete.");
        }

        /// <summary>
        ///     A <see cref="Task"/> that completes when the underlying WebSocket connection is closed.
        /// </summary>
        public Task WhenConnectionClosed => _closeCompletion.Task;

        /// <summary>
        ///     The target WebSocket.
        /// </summary>
        WebSocket Socket { get; }
        
        /// <summary>
        ///     The multiplexer's log facility.
        /// </summary>
        ILogger Log { get; }

        /// <summary>
        ///     The <see cref="CancellationToken"/> used to halt the multiplexer's operation.
        /// </summary>
        CancellationToken Cancellation => _cancellationSource?.Token ?? CancellationToken.None;

        /// <summary>
        ///     Get the input stream (if defined) with the specified stream index.
        /// </summary>
        /// <param name="streamIndex">
        ///     The Kubernetes stream index of the target stream.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream"/>, or <c>null</c> if no stream is defined with the specified index.
        /// </returns>
        public Stream GetInputStream(byte streamIndex)
        {
            K8sMultiplexedReadStream readStream;
            _inputStreams.TryGetValue(streamIndex, out readStream);

            return readStream;
        }

        /// <summary>
        ///     Get the output stream (if defined) with the specified stream index.
        /// </summary>
        /// <param name="streamIndex">
        ///     The Kubernetes stream index of the target stream.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream"/>, or <c>null</c> if no stream is defined with the specified index.
        /// </returns>
        public Stream GetOutputStream(byte streamIndex)
        {
            K8sMultiplexedWriteStream writeStream;
            _outputStreams.TryGetValue(streamIndex, out writeStream);

            return writeStream;
        }

        /// <summary>
        ///     Start processing stream data.
        /// </summary>
        public void Start()
        {
            if (_sendPump != null && _receivePump != null)
                throw new InvalidOperationException("Send / receive pumps are already running.");

            _cancellationSource = new CancellationTokenSource();
            _closeCompletion = new TaskCompletionSource<object>();
            _sendPump = SendPump();
            _receivePump = ReceivePump();
        }

        /// <summary>
        ///     Stop processing stream data and close the underlying <see cref="WebSocket"/>.
        /// </summary>
        /// <param name="cancellation">
        ///     An optional <see cref="CancellationToken"/> that can be used to abort the WebSocket's close operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task Shutdown(CancellationToken cancellation = default)
        {
            if (_sendPump == null && _receivePump == null)
                throw new InvalidOperationException("Send / receive pumps are not running.");

            Log.LogTrace("Shutting down...");

            if (_cancellationSource != null)
            {
                Log.LogTrace("Initiating cancellation of the send / receive pumps...");

                _cancellationSource.Cancel();
                _cancellationSource.Dispose();
                _cancellationSource = null;
            }

            Log.LogTrace("Waiting for send / receive pumps to shut down...");

            Task timeout = Task.Delay(TimeSpan.FromMilliseconds(100), cancellation);
            Task firstCompleted = await Task.WhenAny(Task.WhenAll(_sendPump, _receivePump), timeout).ConfigureAwait(false);
            if (ReferenceEquals(firstCompleted, timeout))
                await timeout.ConfigureAwait(false); // Propagate exception from cancellation (if required), but not from stopping the send / receive pumps.

            _sendPump = null;
            _receivePump = null;

            if (Socket.State == WebSocketState.Open)
            {
                Log.LogTrace("Closing WebSocket...");

                await Socket.CloseAsync(
                     WebSocketCloseStatus.NormalClosure,
                    "Connection closed.",
                    CancellationToken.None
                ).ConfigureAwait(false);

                Log.LogTrace("WebSocket closed.");
            }
            else
                Log.LogTrace("Not closing WebSocket (current state is {WebSocketState}).", Socket.State);

            _closeCompletion.TrySetResult(null);
            _closeCompletion = new TaskCompletionSource<object>();

            Log.LogTrace("Shutdown complete.");
        }

        /// <summary>
        ///     Enqueue a send operation (asynchronously write data to the outgoing stream).
        /// </summary>
        /// <param name="streamIndex">
        ///     The index of the target stream.
        /// </param>
        /// <param name="data">
        ///     The data to write.
        /// </param>
        /// <param name="cancellation">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task EnqueueSend(byte streamIndex, ArraySegment<byte> data, CancellationToken cancellation)
        {
            Log.LogTrace("Enqueuing {DataLength} bytes for sending on stream {StreamIndex}...", data.Count, streamIndex);

            PendingSend pendingSend = new PendingSend(streamIndex, data, cancellation);
            cancellation.Register(
                () => pendingSend.Completion.TrySetCanceled(cancellation)
            );
            _pendingSends.Add(pendingSend);

            Log.LogTrace("Enqueued {DataLength} bytes for sending on stream {StreamIndex}.", data.Count, streamIndex);

            return pendingSend.Completion.Task;
        }

        /// <summary>
        ///     Receive incoming data from the WebSocket.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task"/> representing the pump's operation.
        /// </returns>
        async Task ReceivePump()
        {
            // Capture our cancellation token so it works even if the source is disposed.
            CancellationToken cancellation = Cancellation;

            await Task.Yield();

            Log.LogTrace("Message-receive pump started.");

            ArraySegment<byte> buffer = ArraySegment<byte>.Empty;

            try
            {
                while (Socket.State == WebSocketState.Open)
                {
                    using (await _receiveMutex.LockAsync(cancellation).ConfigureAwait(false))
                    {
                        buffer = CreateReadBuffer();

                        WebSocketReceiveResult readResult = await Socket.ReceiveAsync(buffer, cancellation).ConfigureAwait(false);
                        if (readResult.MessageType == WebSocketMessageType.Close)
                        {
                            Log.LogDebug("Received first half of connection-close handshake (Status = {CloseStatus}, Description = {CloseStatusDescription}).",
                                readResult.CloseStatus.Value,
                                readResult.CloseStatusDescription
                            );

                            Log.LogDebug("Sending second half of connection-close handshake...");
                            await Socket.CloseAsync(readResult.CloseStatus.Value, readResult.CloseStatusDescription, cancellation);
                            Log.LogDebug("Sent second half of connection-close handshake.");

                            _closeCompletion.TrySetResult(null);

                            return;
                        }

                        if (readResult.Count <= 1 && readResult.EndOfMessage)
                        {
                            // Effectively an empty packet; ignore.
                            buffer = buffer.Release();

                            continue;
                        }

                        // Extract stream index.
                        byte streamIndex = buffer[0];

                        Log.LogTrace("Received {DataLength} bytes for stream {StreamIndex} (EndOfMessage = {EndOfMessage}).",
                            readResult.Count - 1,
                            streamIndex,
                            readResult.EndOfMessage
                        );
                        
                        K8sMultiplexedReadStream readStream;
                        if (!_inputStreams.TryGetValue(streamIndex, out readStream))
                        {
                            Log.LogTrace("Stream {StreamIndex} is not registered; discarding data...", streamIndex);

                            await DiscardMessageRemainder(buffer, cancellation).ConfigureAwait(false);
                            buffer = buffer.Release();

                            Log.LogTrace("Ignored remaining data for stream {StreamIndex}.", streamIndex);

                            continue;
                        }
                        
                        if (readStream.IsCompleted)
                        {
                            Log.LogTrace("Stream {StreamIndex} is completed; discarding data...", streamIndex);

                            await DiscardMessageRemainder(buffer, cancellation).ConfigureAwait(false);
                            buffer = buffer.Release();

                            Log.LogTrace("Ignored remaining data for completed stream {StreamIndex}.", streamIndex);

                            continue;
                        }

                        // Skip over stream index.
                        buffer = buffer.Slice(offset: 1, count: readResult.Count - 1);

                        readStream.AddPendingRead(buffer);

                        // Capture any remaining data for this stream segment.
                        while (!readResult.EndOfMessage)
                        {
                            buffer = CreateReadBuffer();
                            readResult = await Socket.ReceiveAsync(buffer, cancellation).ConfigureAwait(false);
                            if (readResult.Count == 0 || readResult.MessageType != WebSocketMessageType.Binary)
                            {
                                buffer = buffer.Release();

                                break;
                            }

                            Log.LogTrace("Received {DataLength} additional bytes for stream {StreamIndex} (EndOfMessage = {EndOfMessage}).",
                                readResult.Count,
                                streamIndex,
                                readResult.EndOfMessage
                            );

                            if (readStream.IsCompleted)
                            {
                                Log.LogTrace("Stream {StreamIndex} is completed; discarding remaining data...", streamIndex);

                                await DiscardMessageRemainder(buffer, cancellation).ConfigureAwait(false);
                                buffer = buffer.Release();

                                Log.LogTrace("Ignored remaining data for completed stream {StreamIndex}.", streamIndex);

                                break;
                            }

                            buffer = buffer.Slice(readResult.Count);
                            readStream.AddPendingRead(buffer);
                        }
                    }
                }

                foreach (K8sMultiplexedReadStream readStream in _inputStreams.Values)
                    readStream.Complete();
            }
            catch (OperationCanceledException)
            {
                // Clean termination.
                
                foreach (K8sMultiplexedReadStream readStream in _inputStreams.Values)
                    readStream.Complete();
            }
            catch (WebSocketException websocketError) when (websocketError.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
            {
                // Connection closed by remote host without completing the close handshake. Consider writing a Trace log entry noting this fact.

                foreach (K8sMultiplexedReadStream readStream in _inputStreams.Values)
                    readStream.Fault(websocketError);
            }
            catch (Exception unexpectedError)
            {
                Log.LogError(EventIds.K8sMultiplexer.ReceivePumpError, unexpectedError, "The message-receive pump encountered an unexpected error.");

                foreach (K8sMultiplexedReadStream readStream in _inputStreams.Values)
                    readStream.Fault(unexpectedError);
            }
            finally
            {
                if (buffer.Array != null)
                    ArrayPool<byte>.Shared.Return(buffer.Array);

                Log.LogTrace("Message-receive pump terminated.");
            }
        }

        /// <summary>
        ///     Send outgoing data to the WebSocket.
        /// </summary>
        /// <returns>
        ///     A <see cref="Task"/> representing the pump's operation.
        /// </returns>
        async Task SendPump()
        {
            // Capture our cancellation token so it works even if the source is disposed.
            CancellationToken cancellation = Cancellation;

            await Task.Yield();

            Log.LogTrace("Message-send pump started.");

            try
            {
                while (!cancellation.IsCancellationRequested && Socket.State == WebSocketState.Open)
                {
                    PendingSend pendingSend;
                    if (_pendingSends.TryTake(out pendingSend, Timeout.Infinite, cancellation) )
                    {
                        using (CancellationTokenSource linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellation, pendingSend.Cancellation))
                        using (await _sendMutex.LockAsync(linkedCancellation.Token).ConfigureAwait(false))
                        {
                            byte[] dataWithPrefix = ArrayPool<byte>.Shared.Rent(pendingSend.Data.Count + 1);

                            try
                            {
                                dataWithPrefix[0] = pendingSend.StreamIndex;
                                Array.Copy(pendingSend.Data.Array, pendingSend.Data.Offset, dataWithPrefix, 1, pendingSend.Data.Count);
                                
                                var sendBuffer = new ArraySegment<byte>(dataWithPrefix, 0, pendingSend.Data.Count + 1);

                                await Socket.SendAsync(sendBuffer,
                                    messageType: WebSocketMessageType.Binary,
                                    endOfMessage: true,
                                    cancellationToken: linkedCancellation.Token
                                ).ConfigureAwait(false);

                                pendingSend.Completion.TrySetResult(null);
                            }
                            catch (OperationCanceledException sendCancelled) when (sendCancelled.CancellationToken == linkedCancellation.Token)
                            {
                                pendingSend.Completion.TrySetCanceled(sendCancelled.CancellationToken);
                            }
                            catch (Exception writeFailed)
                            {
                                pendingSend.Completion.TrySetException(writeFailed);
                            }
                            finally
                            {
                                ArrayPool<byte>.Shared.Return(dataWithPrefix, clearArray: true);
                            }
                        }
                    }
                }
            }
            catch (OperationCanceledException canceled) when (canceled.CancellationToken == cancellation)
            {
                // Clean termination.
            }
            catch (Exception unexpectedError)
            {
                Log.LogError(EventIds.K8sMultiplexer.SendPumpError, unexpectedError, "The message-send pump encountered an unexpected error.");
            }
            finally
            {
                Log.LogTrace("Message-receive pump terminated.");
            }
        }

        /// <summary>
        ///     Rent a buffer from the pool for reading from the transport.
        /// </summary>
        /// <returns>
        ///     The buffer, as an <see cref="ArraySegment{T}"/>.
        /// </returns>
        ArraySegment<byte> CreateReadBuffer() => new ArraySegment<byte>(
            ArrayPool<byte>.Shared.Rent(minimumLength: DefaultBufferSize),
            offset: 0,
            count: DefaultBufferSize
        );

        /// <summary>
        ///     Asynchronously discard the remainder of an incoming message from the underlying WebSocket.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer used to receive data.
        /// </param>
        /// <param name="cancellation">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        async Task DiscardMessageRemainder(ArraySegment<byte> buffer, CancellationToken cancellation)
        {
            WebSocketReceiveResult readResult = readResult = await Socket.ReceiveAsync(buffer, cancellation).ConfigureAwait(false);
            while (!readResult.EndOfMessage)
                readResult = await Socket.ReceiveAsync(buffer, cancellation).ConfigureAwait(false);
        }

        /// <summary>
        ///     Represents a pending send operation.
        /// </summary>
        class PendingSend // TODO: Include stream index here and let the multiplexer prepend it instead of doing it in the write-stream.
        {
            /// <summary>
            ///     Create a new <see cref="PendingSend"/>.
            /// </summary>
            /// <param name="streamIndex">
            ///     The index of the target stream.
            /// </param>
            /// <param name="data">
            ///     The data (including stream-index prefix) to be written to the web socket.
            /// </param>
            /// <param name="cancellation">
            ///     A cancellation token to that can be used to cancel the send operation.
            /// </param>
            public PendingSend(byte streamIndex, ArraySegment<byte> data, CancellationToken cancellation)
            {
                if (data.Array == null)
                    throw new ArgumentNullException(nameof(data));

                StreamIndex = streamIndex;
                Data = data;
                Cancellation = cancellation;
            }

            /// <summary>
            ///     The index of the target stream.
            /// </summary>
            public byte StreamIndex { get; }

            /// <summary>
            ///     The data (including stream-index prefix) to be written to the web socket.
            /// </summary>
            public ArraySegment<byte> Data { get; }

            /// <summary>
            ///     A cancellation token to that can be used to cancel the send operation.
            /// </summary>
            public CancellationToken Cancellation { get; }

            /// <summary>
            ///     A <see cref="TaskCompletionSource{TResult}"/> used to represent the asynchronous send operation.
            /// </summary>
            public TaskCompletionSource<object> Completion { get; } = new TaskCompletionSource<object>();
        }
    }
}
