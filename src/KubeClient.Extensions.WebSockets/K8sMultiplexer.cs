using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
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
        CancellationTokenSource _cancellationSource = new CancellationTokenSource();        
        
        /// <summary>
        ///     A <see cref="Task"/> representing the WebSocket message-receive pump.
        /// </summary>
        Task _receivePump;

        /// <summary>
        ///     A <see cref="Task"/> representing the WebSocket message-send pump.
        /// </summary>
        Task _sendPump;

        /// <summary>
        ///     Create a new <see cref="K8sMultiplexer"/>.
        /// </summary>
        /// <param name="socket">
        ///     The target WebSocket.
        /// </param>
        /// <param name="inputStreamCount">
        ///     The number of of expected input streams.
        /// </param>
        /// <param name="outputStreamCount">
        ///     The number of expected output streams.
        /// </param>
        public K8sMultiplexer(WebSocket socket, byte inputStreamCount, byte outputStreamCount)
        {
            if (socket == null)
                throw new ArgumentNullException(nameof(socket));

            if (inputStreamCount == 0 && outputStreamCount == 0)
                throw new ArgumentException($"Must specify at least one of {nameof(inputStreamCount)} or {nameof(outputStreamCount)}.");
            
            Socket = socket;

            for (byte readStreamIndex = 0; readStreamIndex < inputStreamCount; readStreamIndex++)
                _inputStreams[readStreamIndex] = new K8sMultiplexedReadStream(readStreamIndex);

            for (byte writeStreamIndex = 0; writeStreamIndex < outputStreamCount; writeStreamIndex++)
                _outputStreams[writeStreamIndex] = new K8sMultiplexedWriteStream(writeStreamIndex, EnqueueSend);
        }

        /// <summary>
        ///     Dispose of resources being used by the <see cref="K8sMultiplexer"/>.
        /// </summary>
        public void Dispose()
        {
            if (_cancellationSource != null)
            {
                _cancellationSource.Cancel();
                _cancellationSource.Dispose();
                _cancellationSource = null;
            }

            if (_receivePump != null)
                _receivePump.Wait();
            if (_sendPump != null)
                _sendPump.Wait();
        }

        /// <summary>
        ///     The target WebSocket.
        /// </summary>
        WebSocket Socket { get; }

        /// <summary>
        ///     The <see cref="CancellationToken"/> used to halt the multiplexer's operation.
        /// </summary>
        CancellationToken Cancellation => _cancellationSource.Token;

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
            if (_receivePump != null || _sendPump != null)
                throw new InvalidOperationException("Read / write pumps are already running.");

            _receivePump = ReceivePump();
            _sendPump = SendPump();
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
            PendingSend pendingWrite = new PendingSend(data, cancellation);
            cancellation.Register(
                () => pendingWrite.Completion.TrySetCanceled(cancellation)
            );
            _pendingWrites.Add(pendingWrite);

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
            await Task.Yield();

            ArraySegment<byte> buffer = null;

            try
            {
                while (Socket.State == WebSocketState.Open)
                {
                    buffer = new ArraySegment<byte>(
                        ArrayPool<byte>.Shared.Rent(minimumLength: DefaultBufferSize)
                    );

                    WebSocketReceiveResult readResult = await Socket.ReceiveAsync(buffer, Cancellation);
                    if (readResult.Count <= 1 && readResult.EndOfMessage)
                    {
                        // Effectively an empty packet; ignore.
                        ArrayPool<byte>.Shared.Return(buffer.Array, clearArray: true);

                        continue;
                    }

                    // Extract stream index.
                    byte streamIndex = buffer[0];
                    
                    K8sMultiplexedReadStream readStream;
                    if (!_inputStreams.TryGetValue(streamIndex, out readStream))
                    {
                        // Unknown stream; discard the rest of the message.
                        while (!readResult.EndOfMessage)
                            readResult = await Socket.ReceiveAsync(buffer, Cancellation);

                        ArrayPool<byte>.Shared.Return(buffer.Array, clearArray: true);

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
                        readResult = await Socket.ReceiveAsync(buffer, Cancellation);
                        if (readResult.Count == 0 || readResult.MessageType != WebSocketMessageType.Binary)
                        {
                            ArrayPool<byte>.Shared.Return(buffer.Array);

                            break;
                        }

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
                if (buffer != null)
                    ArrayPool<byte>.Shared.Return(buffer.Array);
            }
            catch (Exception)
            {
                if (buffer != null)
                    ArrayPool<byte>.Shared.Return(buffer.Array);

                throw;
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
            await Task.Yield();

            try
            {
                while (!Cancellation.IsCancellationRequested)
                {
                    PendingSend pendingWrite;
                    if (!_pendingWrites.TryTake(out pendingWrite, Timeout.Infinite, Cancellation))
                        continue;

                    using (CancellationTokenSource linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(Cancellation, pendingWrite.Cancellation))
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
            if (data == null)
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
