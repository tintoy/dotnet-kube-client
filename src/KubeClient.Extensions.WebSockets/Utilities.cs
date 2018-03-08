using System;
using System.Threading;

namespace KubeClient.Extensions.WebSockets
{
    /// <summary>
    ///     Helper functions.
    /// </summary>
    static class Utilities
    {
        /// <summary>
        ///     Block the current thread until the <see cref="WaitHandle"/> receives a signal or the specified <see cref="CancellationToken"/> is canceled.
        /// </summary>
        /// <param name="waitHandle">
        ///     The <see cref="WaitHandle"/>.
        /// </param>
        /// <param name="cancellation">
        ///     A <see cref="CancellationToken"/> can can be used to cancel the wait.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="WaitHandle"/> received a signal; <c>false</c>, if the <see cref="CancellationToken"/> is canceled.
        /// </returns>
        public static bool WaitOne(this WaitHandle waitHandle, CancellationToken cancellation)
        {
            if (!cancellation.CanBeCanceled) // e.g. CancellationToken.None.
                return waitHandle.WaitOne();

            int signaledHandleIndex = WaitHandle.WaitAny(new[] { waitHandle, cancellation.WaitHandle });

            return signaledHandleIndex == 0; // WaitAny returns the index of the signaled wait handle.
        }

        /// <summary>
        ///     Block the current thread until the <see cref="WaitHandle"/> receives a signal, the specified timeout period has elapsed, or the specified <see cref="CancellationToken"/> is canceled.
        /// </summary>
        /// <param name="waitHandle">
        ///     The <see cref="WaitHandle"/>.
        /// </param>
        /// <param name="timeout">
        ///     The span of time to wait for a signal.
        /// </param>
        /// <param name="cancellation">
        ///     A <see cref="CancellationToken"/> can can be used to cancel the wait.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the <see cref="WaitHandle"/> received a signal; <c>false</c>, if the <paramref name="timeout"/> expires or the <see cref="CancellationToken"/> is canceled.
        /// </returns>
        public static bool WaitOne(this WaitHandle waitHandle, TimeSpan timeout, CancellationToken cancellation)
        {
            if (!cancellation.CanBeCanceled) // e.g. CancellationToken.None.
                return waitHandle.WaitOne(timeout);

            int signaledHandleIndex = WaitHandle.WaitAny(new[] { waitHandle, cancellation.WaitHandle }, timeout);

            return signaledHandleIndex == 0; // WaitAny returns the index of the signaled wait handle.
        }
    }
}