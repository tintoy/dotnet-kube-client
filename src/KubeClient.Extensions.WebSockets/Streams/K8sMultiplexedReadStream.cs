using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        ///     A lock used to synchronise access to reader state.
        /// </summary>
        readonly AsyncLock _mutex;

        /// <summary>
        ///     A condition variable indicating availability of data to read or the stream's completion.
        /// </summary>
        readonly AsyncConditionVariable _completedOrDataAvailable;

        /// <summary>
        ///     The stream's queue of pending read operations.
        /// </summary>
        readonly Queue<PendingRead> _pendingReads = new Queue<PendingRead>();

        /// <summary>
        ///     The number of bytes currently pending in the stream.
        /// </summary>
        int _bytesPending;

        /// <summary>
        ///     If the stream is in the faulted completed or faulted state, this caches the task representing that state.
        /// </summary>
        Task<int> _completion;

        /// <summary>
        ///     Create a new <see cref="K8sMultiplexedReadStream"/>.
        /// </summary>
        /// <param name="streamIndex">
        ///     The Kubernetes stream index of the target input stream.
        /// </param>
        /// <param name="maxPendingBytes">
        ///     The maximum number of pending bytes that the stream can hold.
        /// </param>
        /// <param name="loggerFactory">
        ///     The <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </param>
        public K8sMultiplexedReadStream(byte streamIndex, int maxPendingBytes, ILoggerFactory loggerFactory)
        {
            if (maxPendingBytes < 1)
                throw new ArgumentOutOfRangeException(nameof(maxPendingBytes), maxPendingBytes, "Maximum number of pending bytes cannot be less than 1.");

            _mutex = new AsyncLock();
            _completedOrDataAvailable = new AsyncConditionVariable(_mutex);

            StreamIndex = streamIndex;
            MaxPendingBytes = maxPendingBytes;
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
            if (disposing && !IsDisposed)
            {
                using (_mutex.Lock())
                {
                    IsDisposed = true;

                    _completedOrDataAvailable.NotifyAll(); // Release any tasks that are waiting.

                    // Ensure we don't leak memory.
                    foreach (PendingRead pendingRead in _pendingReads)
                        pendingRead.Release();

                    _pendingReads.Clear();
                }
            }
        }

        /// <summary>
        ///     Check if the stream has been disposed.
        /// </summary>
        void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        /// <summary>
        ///     The Kubernetes stream index of the target input stream.
        /// </summary>
        public byte StreamIndex { get; }

        /// <summary>
        ///     The maximum number of pending bytes that the stream can hold.
        /// </summary>
        public int MaxPendingBytes { get; }

        /// <summary>
        ///     Has the stream been disposed?
        /// </summary>
        public bool IsDisposed { get; private set; }

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
        ///     Is the stream in the completed state?
        /// </summary>
        public bool IsCompleted => _completion != null || IsDisposed;

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

            CheckDisposed();

            if (IsCompleted)
                return _completion.GetAwaiter().GetResult();

            using (_mutex.Lock())
            {
                PendingRead pendingRead;
                if (!_pendingReads.TryPeek(out pendingRead))
                {
                    // Wait for data.
                    _completedOrDataAvailable.Wait();
                    if (!_pendingReads.TryPeek(out pendingRead))
                    {
                        // If we get here, then the stream was completed, so our read should be aborted.
                        Debug.Assert(IsCompleted, "No pending read available, but stream is not completed.");

                        return _completion.GetAwaiter().GetResult();
                    }
                }

                int bytesRead = pendingRead.DrainTo(buffer, offset);
                _bytesPending -= bytesRead;

                if (pendingRead.IsEmpty)
                    Consume(pendingRead); // Source buffer has been consumed.

                return bytesRead;
            }
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

            CheckDisposed();

            if (IsCompleted)
                return await _completion.ConfigureAwait(false);

            PendingRead pendingRead;
            using (await _mutex.LockAsync(cancellationToken).ConfigureAwait(false))
            {
                if (!_pendingReads.TryPeek(out pendingRead))
                {
                    // Wait for data.
                    await _completedOrDataAvailable.WaitAsync(cancellationToken).ConfigureAwait(false);
                    if (!_pendingReads.TryPeek(out pendingRead))
                    {
                        // If we get here, then the stream was completed, so our read should be aborted.
                        Debug.Assert(IsCompleted, "No pending read available, but stream is not completed.");

                        return await _completion.ConfigureAwait(false);
                    }
                }

                // Last chance to cancel non-destructively.
                cancellationToken.ThrowIfCancellationRequested();

                int bytesRead = pendingRead.DrainTo(buffer, offset);
                _bytesPending -= bytesRead;

                if (pendingRead.IsEmpty)
                    Consume(pendingRead); // Source buffer has been consumed.

                return bytesRead;
            }
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

            using (_mutex.Lock())
            {
                if (IsDisposed)
                {
                    ArrayPool<byte>.Shared.Return(data.Array, clearArray: true);

                    return;
                }

                if (_bytesPending + data.Count > MaxPendingBytes)
                {
                    Fault(new IOException(
                        $"Capacity of read buffer for stream {StreamIndex} ({MaxPendingBytes} bytes) has been exceeded."
                    ));

                    ArrayPool<byte>.Shared.Return(data.Array, clearArray: true);

                    return;
                }

                _pendingReads.Enqueue(new PendingRead(data));
                _bytesPending += data.Count;
                _completedOrDataAvailable.Notify();
            }
        }

        /// <summary>
        ///     Mark the stream as completed.
        /// </summary>
        internal void Complete()
        {
            using (_mutex.Lock())
            {
                if (IsDisposed)
                    return;

                if (_completion == null)
                {
                    _completion = Task.FromResult(0);
                
                    Log.LogTrace("Marked read stream {StreamIndex} as successfully completed.", StreamIndex);
                }
            }
        }

        /// <summary>
        ///     Mark the stream as faulted.
        /// </summary>
        /// <param name="exception">
        ///     An <see cref="Exception"/> representing the cause of the fault.
        /// </param>
        internal void Fault(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            using (_mutex.Lock())
            {
                if (IsDisposed)
                    return;
                
                if (_completion == null)
                {
                    _completion = Task.FromException<int>(exception);

                    Log.LogTrace("Marked read stream {StreamIndex} as faulted ({ExceptionType}).", StreamIndex, exception.GetType().FullName);
                }    
            }
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

                Release();

                return bytesAvailable;
            }

            /// <summary>
            ///     Release any the buffer (if any) held by the pending read.
            /// </summary>
            public void Release()
            {
                if (_data.Array != null)
                {
                    ArrayPool<byte>.Shared.Return(_data.Array, clearArray: true);
                    _data = ArraySegment<byte>.Empty;
                }
            }
        }
    }
}
