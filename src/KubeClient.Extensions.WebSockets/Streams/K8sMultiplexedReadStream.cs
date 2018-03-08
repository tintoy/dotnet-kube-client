using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Extensions.WebSockets.Streams
{
    /// <summary>
    ///     Represents a single input substream within a Kubernetes-style multiplexed stream.
    /// </summary>
    sealed class K8sMultiplexedReadStream
        : Stream
    {
        /// <summary>
        ///     The stream's queue of pending read operations.
        /// </summary>
        readonly ConcurrentQueue<PendingRead> _pendingReads = new ConcurrentQueue<PendingRead>();

        /// <summary>
        ///     A wait handle representing the availability of data to read.
        /// </summary>
        readonly AutoResetEvent _dataAvailable = new AutoResetEvent(initialState: false);

        /// <summary>
        ///     Create a new <see cref="K8sMultiplexedReadStream"/>.
        /// </summary>
        /// <param name="streamIndex">
        ///     The Kubernetes stream index of the target input stream.
        /// </param>
        /// <param name="loggerFactory">
        ///     The <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </param>
        public K8sMultiplexedReadStream(byte streamIndex, ILoggerFactory loggerFactory)
        {
            StreamIndex = streamIndex;
            Log = loggerFactory.CreateLogger<K8sMultiplexedReadStream>();
        }

        /// <summary>
        ///     Dispose of resources used by the <see cref="K8sMultiplexedReadStream"/>.
        /// </summary>
        /// <param name="disposing">
        ///     Explicit disposal?
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _dataAvailable.Dispose();
        }

        /// <summary>
        ///     The Kubernetes stream index of the target input stream.
        /// </summary>
        public byte StreamIndex { get; }

        /// <summary>
        ///     Does the stream support reading?
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        ///     Does the stream support seeking?
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        ///     Does the stream support writing?
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        ///     The stream length (not supported).
        /// </summary>
        public override long Length => throw new NotSupportedException("The stream does not support seeking.");

        /// <summary>
        ///     The stream position (not supported).
        /// </summary>
        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        ///     The stream's log facility.
        /// </summary>
        ILogger Log { get; }

        /// <summary>
        ///     Read data from the stream.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to which the incoming data will be written.
        /// </param>
        /// <param name="offset">
        ///     The offset within the buffer to which data will be written.
        /// </param>
        /// <param name="count">
        ///     The maximum number of bytes to read.
        /// </param>
        /// <returns>
        ///     The number of bytes that were read from the stream.
        /// </returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            PendingRead pendingRead = NextPendingRead();

            int bytesRead = pendingRead.DrainTo(buffer, offset);
            if (pendingRead.IsEmpty)
                Consume(pendingRead); // Source buffer has been consumed.

            return bytesRead;
        }

        /// <summary>
        ///     Asynchronously read data from the stream.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to which the incoming data will be written.
        /// </param>
        /// <param name="offset">
        ///     The offset within the buffer to which data will be written.
        /// </param>
        /// <param name="count">
        ///     The maximum number of bytes to read.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The number of bytes that were read from the stream.
        /// </returns>
        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            await Task.Yield();

            PendingRead pendingRead = NextPendingRead(cancellationToken);

            // Last chance to cancel non-destructively.
            cancellationToken.ThrowIfCancellationRequested();

            int bytesRead = pendingRead.DrainTo(buffer, offset);
            if (pendingRead.IsEmpty)
                Consume(pendingRead); // Source buffer has been consumed.

            return bytesRead;
        }

        /// <summary>
        ///     Flush pending data (not supported).
        /// </summary>
        public override void Flush() => throw new NotSupportedException("The stream does not support writing.");

        /// <summary>
        ///     Seek to the specified position in the stream (not supported).
        /// </summary>
        /// <param name="offset">
        ///     The seek offset, relative to the specified <paramref name="origin"/>.
        /// </param>
        /// <param name="origin">
        ///     The seek origin.
        /// </param>
        /// <returns>
        ///     The new stream position.
        /// </returns>
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException("Stream does not support seeking.");

        /// <summary>
        ///     Set the stream length (not supported).
        /// </summary>
        /// <param name="value">
        ///     The new stream length.
        /// </param>
        public override void SetLength(long value) => throw new NotSupportedException("Stream does not support seeking.");

        /// <summary>
        ///     Write data to the stream (not supported).
        /// </summary>
        /// <param name="buffer">
        ///     A buffer containing the data to write.
        /// </param>
        /// <param name="offset">
        ///     The offset, within the buffer, of the data to write.
        /// </param>
        /// <param name="count">
        ///     The number of bytes to write.
        /// </param>
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException("Stream does not support writing.");

        /// <summary>
        ///     Make data available to be read from the stream.
        /// </summary>
        /// <param name="data">
        ///     An <see cref="ArraySegment{T}"/> representing the data.
        /// </param>
        internal void AddPendingRead(ArraySegment<byte> data)
        {
            if (data.Array == null)
                throw new ArgumentNullException(nameof(data));

            _pendingReads.Enqueue(new PendingRead(data));
            _dataAvailable.Set();
        }

        /// <summary>
        ///     Get the next available pending read (does not support cancellation).
        /// </summary>
        /// <returns>
        ///     The <see cref="PendingRead"/>.
        /// </returns>
        /// <remarks>
        ///     If no pending read is currently available, blocks until a pending read is available.
        /// </remarks>
        PendingRead NextPendingRead()
        {
            PendingRead pendingRead;
            if (!_pendingReads.TryPeek(out pendingRead))
            {
                // Wait for data.
                while (pendingRead == null)
                {
                    _dataAvailable.WaitOne();
                    _pendingReads.TryPeek(out pendingRead);
                }
            }

            return pendingRead;
        }

        /// <summary>
        ///     Get the next available pending read (supports cancellation).
        /// </summary>
        /// <param name="cancellation">
        ///     A <see cref="CancellationToken"/> that can be used to abort the wait for a pending read.
        /// </param>
        /// <returns>
        ///     The <see cref="PendingRead"/>.
        /// </returns>
        /// <remarks>
        ///     If no pending read is currently available, blocks until a pending read is available or the cancellation token is cancelled.
        /// </remarks>
        PendingRead NextPendingRead(CancellationToken cancellation)
        {
            PendingRead pendingRead;
            if (!_pendingReads.TryPeek(out pendingRead))
            {
                // Wait for data.
                while (pendingRead == null)
                {
                    if (!_dataAvailable.WaitOne(cancellation))
                        throw new OperationCanceledException("Read operation was canceled.", cancellation);

                    _pendingReads.TryPeek(out pendingRead);
                }
            }

            return pendingRead;
        }

        /// <summary>
        ///     Consume the a pending read, removing it from the queue.
        /// </summary>
        /// <remarks>
        ///     The <see cref="PendingRead"/> must be at the head of the <see cref="_pendingReads"/> queue.
        /// </remarks>
        void Consume(PendingRead pending)
        {
            if (pending == null)
                throw new ArgumentNullException(nameof(pending));

            PendingRead consumed;
            bool wasConsumed = _pendingReads.TryDequeue(out consumed);
            Debug.Assert(wasConsumed, "Attempted to consume pending read when none was available.");

            // AF: This should not happen; if it does, then I've fucked up somewhere and we have a race condition.
            Debug.Assert(ReferenceEquals(consumed, pending),
                "Consumed a pending read that was not at the head of the queue."                
            );
        }

        /// <summary>
        ///     Represents a pending read operation.
        /// </summary>
        class PendingRead
        {
            /// <summary>
            ///     The data that will be returned by the pending read.
            /// </summary>
            ArraySegment<byte> _data;

            /// <summary>
            ///     Create a new <see cref="PendingRead"/>.
            /// </summary>
            /// <param name="data">
            ///     The data that will be returned by the pending read.
            /// </param>
            public PendingRead(ArraySegment<byte> data)
            {
                if (data.Array == null)
                    throw new ArgumentNullException(nameof(data));

                _data = data;
            }

            /// <summary>
            ///     The number of bytes available to read.
            /// </summary>
            public int Count => _data.Count;

            /// <summary>
            ///     Has the read buffer been exhausted?
            /// </summary>
            public bool IsEmpty => Count == 0;

            /// <summary>
            ///     Drain the read-buffer into the specified buffer.
            /// </summary>
            /// <param name="buffer">
            ///     The buffer to which data will be written.
            /// </param>
            /// <param name="offset">
            ///     The offset within the buffer at which data will be written.
            /// </param>
            /// <returns>
            ///     The number of bytes written to the buffer.
            /// </returns>
            public int DrainTo(byte[] buffer, int offset)
            {
                int bytesAvailable = _data.Count;
                int bufferCapacity = buffer.Length - (offset + 1);
                if (bufferCapacity <= bytesAvailable)
                {
                    // We still have data remaining.
                    Array.Copy(_data.Array, _data.Offset, buffer, offset, bufferCapacity);
                    _data = new ArraySegment<byte>(_data.Array,
                        offset: _data.Offset + bufferCapacity,
                        count: _data.Count - bufferCapacity
                    );

                    return bufferCapacity;
                }
                
                // This is the last of our data; copy it all.
                Array.Copy(_data.Array, _data.Offset, buffer, offset, bytesAvailable);

                ArrayPool<byte>.Shared.Return(_data.Array, clearArray: true);
                _data = ArraySegment<byte>.Empty;

                return bytesAvailable;
            }
        }
    }
}
