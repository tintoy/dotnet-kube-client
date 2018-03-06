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
    using Microsoft.Extensions.Logging;
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
        const int DefaultBufferSize = 1024;

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
        readonly BlockingCollection<PendingSend> _pendingWrites = new BlockingCollection<PendingSend>(new ConcurrentQueue<PendingSend>());

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
        public K8sMultiplexer(WebSocket socket, byte[] inputStreamIndexes, byte[] outputStreamIndexes, ILoggerFactory loggerFactory)
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
                _inputStreams[inputStreamIndex] = new K8sMultiplexedReadStream(inputStreamIndex, loggerFactory);

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
            }
            catch (OperationCanceledException)
            {
                // Close operation timed out; nothing useful we can do here.
            }
            catch (AggregateException stopFailed)
            {
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
            Task firstCompleted = await Task.WhenAny(Task.WhenAll(_sendPump, _receivePump), timeout);
            if (ReferenceEquals(firstCompleted, timeout))
                await timeout; // Propagate exception from cancellation (if required), but not from stopping the send / receive pumps.

            _sendPump = null;
            _receivePump = null;

            if (Socket.State == WebSocketState.Open)
            {
                Log.LogTrace("Closing WebSocket...");

                await Socket.CloseAsync(
                     WebSocketCloseStatus.NormalClosure,
                    "Connection closed.",
                    CancellationToken.None
                );

                Log.LogTrace("WebSocket closed.");
            }
            else
                Log.LogTrace("Not closing WebSocket (current state is {WebSocketState}).", Socket.State);

            Log.LogTrace("Shutdown complete.");
        }

        /// <summary>
        ///     Enqueue a send operation (asynchronously write data to the outgoing stream).
        /// </summary>
        /// <param name="data">
        ///     The data to write.
        /// </param>
        /// <param name="cancellation">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task EnqueueSend(ArraySegment<byte> data, CancellationToken cancellation)
        {
            Log.LogTrace("Enqueuing {DataLength} bytes for send...", data.Count);

            PendingSend pendingWrite = new PendingSend(data, cancellation);
            cancellation.Register(
                () => pendingWrite.Completion.TrySetCanceled(cancellation)
            );
            _pendingWrites.Add(pendingWrite);

            Log.LogTrace("Enqueued {DataLength} bytes for send.", data.Count);

            return pendingWrite.Completion.Task;
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

            ArraySegment<byte> buffer;

            try
            {
                while (Socket.State == WebSocketState.Open)
                {
                    buffer = new ArraySegment<byte>(
                        ArrayPool<byte>.Shared.Rent(minimumLength: DefaultBufferSize)
                    );

                    WebSocketReceiveResult readResult = await Socket.ReceiveAsync(buffer, cancellation);
                    if (readResult.Count <= 1 && readResult.EndOfMessage)
                    {
                        // Effectively an empty packet; ignore.
                        ArrayPool<byte>.Shared.Return(buffer.Array, clearArray: true);

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
                        Log.LogTrace("Stream {StreamIndex} is not registered; ignoring data...", streamIndex);

                        // Unknown stream; discard the rest of the message.
                        while (!readResult.EndOfMessage)
                            readResult = await Socket.ReceiveAsync(buffer, cancellation);

                        ArrayPool<byte>.Shared.Return(buffer.Array, clearArray: true);

                        Log.LogTrace("Ignored remaining data for stream {StreamIndex}.", streamIndex);

                        continue;
                    }

                    // Skip over stream index.
                    buffer = new ArraySegment<byte>(buffer.Array,
                        offset: buffer.Offset + 1,
                        count: readResult.Count - 1
                    );

                    readStream.AddPendingRead(buffer);

                    while (!readResult.EndOfMessage)
                    {
                        buffer = new ArraySegment<byte>(
                            ArrayPool<byte>.Shared.Rent(minimumLength: DefaultBufferSize)
                        );
                        readResult = await Socket.ReceiveAsync(buffer, cancellation);
                        if (readResult.Count == 0 || readResult.MessageType != WebSocketMessageType.Binary)
                        {
                            ArrayPool<byte>.Shared.Return(buffer.Array);

                            break;
                        }

                        Log.LogTrace("Received {DataLength} additional bytes for stream {StreamIndex} (EndOfMessage = {EndOfMessage}).",
                            readResult.Count,
                            streamIndex,
                            readResult.EndOfMessage
                        );

                        buffer = new ArraySegment<byte>(buffer.Array,
                            offset: buffer.Offset,
                            count: readResult.Count - 1
                        );
                        readStream.AddPendingRead(buffer);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Clean termination.
            }
            catch (WebSocketException websocketError) when (websocketError.WebSocketErrorCode == WebSocketError.ConnectionClosedPrematurely)
            {
                // Connection closed by remote host without completing the close handshake. Consider writing a Trace log entry noting this fact.
            }
            catch (Exception unexpectedError)
            {
                Log.LogError(EventIds.K8sMultiplexer.ReceivePumpError, unexpectedError, "The message-receive pump encountered an unexpected error.");
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

            Log.LogTrace("Message-receive pump started.");

            try
            {
                while (!Cancellation.IsCancellationRequested)
                {
                    PendingSend pendingWrite;
                    if (!_pendingWrites.TryTake(out pendingWrite, Timeout.Infinite, Cancellation))
                        continue;

                    using (CancellationTokenSource linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellation, pendingWrite.Cancellation))
                    {
                        try
                        {
                            await Socket.SendAsync(pendingWrite.Data,
                                WebSocketMessageType.Binary,
                                endOfMessage: true,
                                cancellationToken: linkedCancellation.Token
                            );

                            pendingWrite.Completion.TrySetResult(null);
                        }
                        catch (OperationCanceledException sendCancelled) when (sendCancelled.CancellationToken == linkedCancellation.Token)
                        {
                            pendingWrite.Completion.TrySetCanceled(sendCancelled.CancellationToken);
                        }
                        catch (Exception writeFailed)
                        {
                            pendingWrite.Completion.TrySetException(writeFailed);
                        }
                        finally
                        {
                            ArrayPool<byte>.Shared.Return(pendingWrite.Data.Array, clearArray: true);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
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
    }

    /// <summary>
    ///     Represents a pending send operation.
    /// </summary>
    class PendingSend // TODO: Include stream index here and let the multiplexer prepend it instead of doing it in the write-stream.
    {
        /// <summary>
        ///     Create a new <see cref="PendingSend"/>.
        /// </summary>
        /// <param name="data">
        ///     The data (including stream-index prefix) to be written to the web socket.
        /// </param>
        /// <param name="cancellation">
        ///     A cancellation token to that can be used to cancel the send operation.
        /// </param>
        public PendingSend(ArraySegment<byte> data, CancellationToken cancellation)
        {
            if (data.Array == null)
                throw new ArgumentNullException(nameof(data));

            Data = data;
            Cancellation = cancellation;
        }

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
