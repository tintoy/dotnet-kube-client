using System;
using System.Buffers;
using System.Threading;

namespace KubeClient.Extensions.WebSockets
{
    /// <summary>
    ///     Helper functions.
    /// </summary>
    static class Utilities
    {
        /// <summary>
        ///     Create a new array segment, dropping the specified number of elements from the start of the array segment.
        /// </summary>
        /// <param name="arraySegment">
        ///     The array segment.
        /// </param>
        /// <param name="count">
        ///     The number of elements to drop.
        /// </param>
        /// <returns>
        ///     The new array segment.
        /// </returns>
        public static ArraySegment<T> DropLeft<T>(this ArraySegment<T> arraySegment, int count)
        {
            if (count > arraySegment.Count)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot drop more elements than the array segment contains.");

            return new ArraySegment<T>(arraySegment.Array,
                offset: arraySegment.Offset + 1,
                count: arraySegment.Count - count
            );
        }

        /// <summary>
        ///     Create a new array segment, dropping the specified number of elements from the start of the array segment.
        /// </summary>
        /// <param name="arraySegment">
        ///     The array segment.
        /// </param>
        /// <param name="count">
        ///     The number of elements to drop.
        /// </param>
        /// <returns>
        ///     The new array segment.
        /// </returns>
        public static ArraySegment<T> DropRight<T>(this ArraySegment<T> arraySegment, int count)
        {
            if (count > arraySegment.Count)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot drop more elements than the array segment contains.");

            return new ArraySegment<T>(arraySegment.Array,
                offset: arraySegment.Offset,
                count: arraySegment.Count - count
            );
        }

        /// <summary>
        ///     Create a new array segment containing the specified elements from the start of the array segment.
        /// </summary>
        /// <param name="arraySegment">
        ///     The array segment.
        /// </param>
        /// <param name="count">
        ///     The number of elements the slice will contain.
        /// </param>
        /// <returns>
        ///     The new array segment.
        /// </returns>
        public static ArraySegment<T> Slice<T>(this ArraySegment<T> arraySegment, int count) => arraySegment.Slice(arraySegment.Offset, count);

        /// <summary>
        ///     Create a new array segment containing the specified elements from the array segment.
        /// </summary>
        /// <param name="arraySegment">
        ///     The array segment.
        /// </param>
        /// <param name="offset">
        ///     The offset, within the array segment, where the slice will start.
        /// </param>
        /// <param name="count">
        ///     The number of elements the slice will contain.
        /// </param>
        /// <returns>
        ///     The new array segment.
        /// </returns>
        public static ArraySegment<T> Slice<T>(this ArraySegment<T> arraySegment, int offset, int count)
        {
            if (offset < 0 || offset >= arraySegment.Count)
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "Offset must be within the bounds of the array.");

            if ((offset + count) > arraySegment.Count)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Slice cannot contain more elements than the source array segment.");

            return new ArraySegment<T>(arraySegment.Array, offset, count);
        }

        /// <summary>
        ///     Return the underlying array to the shared pool.
        /// </summary>
        /// <param name="arraySegment">
        ///     The array segment.
        /// </param>
        /// <param name="clearArray">
        ///     Initialise the array's elements to their default values?
        /// </param>
        /// <returns>
        ///     <see cref="ArraySegment{T}.Empty"/>.
        /// </returns>
        public static ArraySegment<T> Release<T>(this ArraySegment<T> arraySegment, bool clearArray = false)
        {
            if (arraySegment.Array != null)
                ArrayPool<T>.Shared.Return(arraySegment.Array, clearArray);

            return ArraySegment<T>.Empty;
        }

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