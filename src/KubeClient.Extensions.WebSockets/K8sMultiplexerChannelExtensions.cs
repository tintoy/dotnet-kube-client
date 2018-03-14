using System;
using System.IO;

namespace KubeClient.Extensions.WebSockets
{
    /// <summary>
    ///     Channel-related extension methods for <see cref="K8sMultiplexer"/>.
    /// </summary>
    public static class K8sMultiplexerChannelExtensions
    {
        /// <summary>
        ///     Get an output stream representing process STDIN.
        /// </summary>
        /// <param name="multiplexer">
        ///     The Kubernetes channel multiplexer.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream"/>, or <c>null</c> if the STDIN stream is not available.
        /// </returns>
        public static Stream GetStdIn(this K8sMultiplexer multiplexer)
        {
            if (multiplexer == null)
                throw new ArgumentNullException(nameof(multiplexer));
            
            return multiplexer.GetOutputStream(K8sChannel.StdIn);
        }

        /// <summary>
        ///     Get an output stream representing process STDOUT.
        /// </summary>
        /// <param name="multiplexer">
        ///     The Kubernetes channel multiplexer.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream"/>, or <c>null</c> if the STDOUT stream is not available.
        /// </returns>
        public static Stream GetStdOut(this K8sMultiplexer multiplexer)
        {
            if (multiplexer == null)
                throw new ArgumentNullException(nameof(multiplexer));
            
            return multiplexer.GetInputStream(K8sChannel.StdOut);
        }

        /// <summary>
        ///     Get an output stream representing process STDERR.
        /// </summary>
        /// <param name="multiplexer">
        ///     The Kubernetes channel multiplexer.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream"/>, or <c>null</c> if the STDERR stream is not available.
        /// </returns>
        public static Stream GetStdErr(this K8sMultiplexer multiplexer)
        {
            if (multiplexer == null)
                throw new ArgumentNullException(nameof(multiplexer));
            
            return multiplexer.GetInputStream(K8sChannel.StdErr);
        }
    }
}
