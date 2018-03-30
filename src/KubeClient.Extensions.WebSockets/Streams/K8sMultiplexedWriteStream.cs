using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Extensions.WebSockets.Streams
{
    /// <summary>
    ///     Represents a single output substream within a Kubernetes-style multiplexed stream.
    /// </summary>
    sealed class K8sMultiplexedWriteStream
        : Stream
    {
        /// <summary>
        ///     Create a new <see cref="K8sMultiplexedWriteStream"/>.
        /// </summary>
        /// <param name="streamIndex">
        ///     The Kubernetes stream index of the target output stream.
        /// </param>
        /// <param name="sendAsync">
        ///     A delegate used to asynchronously send outgoing data.
        /// </param>
        /// <param name="loggerFactory">
        ///     The <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </param>
        public K8sMultiplexedWriteStream(byte streamIndex, Func<byte, ArraySegment<byte>, CancellationToken, Task> sendAsync, ILoggerFactory loggerFactory)
        {
            if (sendAsync == null)
                throw new ArgumentNullException(nameof(sendAsync));

            StreamIndex = streamIndex;
            SendAsync = sendAsync;
            Log = loggerFactory.CreateLogger<K8sMultiplexedReadStream>();
        }

        /// <summary>
        ///     The Kubernetes stream index of the target output stream.
        /// </summary>
        public byte StreamIndex { get; }

        /// <summary>
        ///     A delegate used to asynchronously send outgoing data.
        /// </summary>
        public Func<byte, ArraySegment<byte>, CancellationToken, Task> SendAsync { get; }

        /// <summary>
        ///     Does the stream support reading?
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        ///     Does the stream support seeking?
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        ///     Does the stream support writing?
        /// </summary>
        public override bool CanWrite => true;

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
        ///     Flush pending data (a no-op for this implementation).
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        ///     Write data to the stream.
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
        public override void Write(byte[] buffer, int offset, int count)
        {
            SendAsync(StreamIndex, new ArraySegment<byte>(buffer, offset, count), CancellationToken.None)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        ///     Asynchronously write data to the stream (not supported).
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
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the write operation.
        /// </param>
        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await SendAsync(StreamIndex, new ArraySegment<byte>(buffer, offset, count), cancellationToken).ConfigureAwait(false);
        }

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
        ///     Read data from the stream (not supported).
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
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException("Stream does not support reading.");
    }
}