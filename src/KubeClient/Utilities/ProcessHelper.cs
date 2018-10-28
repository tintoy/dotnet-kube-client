using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Utilities
{
    /// <summary>
    ///     Helper methods for working with a <see cref="Process"/>.
    /// </summary>
    static class ProcessHelper
    {
        /// <summary>
        ///     Asynchronously wait for the process to exit.
        /// </summary>
        /// <param name="process">
        ///     The target <see cref="Process"/>.
        /// </param>
        /// <returns>
        ///     The process exit code.
        /// </returns>
        public static Task<int> WaitForExitAsync(this Process process)
        {
            return process.WaitForExitAsync(CancellationToken.None);
        }
        
        /// <summary>
        ///     Asynchronously wait for the process to exit.
        /// </summary>
        /// <param name="process">
        ///     The target <see cref="Process"/>.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the wait.
        /// </param>
        /// <returns>
        ///     The process exit code.
        /// </returns>
        public static Task<int> WaitForExitAsync(this Process process, CancellationToken cancellationToken)
        {
            return process.WaitForExitAsync(cancellationToken, killIfCancelled: false);
        }

        /// <summary>
        ///     Asynchronously wait for the process to exit.
        /// </summary>
        /// <param name="process">
        ///     The target <see cref="Process"/>.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the wait.
        /// </param>
        /// <param name="killIfCancelled">
        ///     Kill the process if the <paramref name="cancellationToken"/> is cancelled?
        /// </param>
        /// <returns>
        ///     The process exit code.
        /// </returns>
        public static Task<int> WaitForExitAsync(this Process process, CancellationToken cancellationToken, bool killIfCancelled)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            // Short-circuit: already exited.
            if (process.HasExited)
                return Task.FromResult(process.ExitCode);
            
            TaskCompletionSource<int> completion = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
            
            cancellationToken.Register(() =>
            {
                try
                {
                    if (killIfCancelled && !process.HasExited)
                        process.Kill();
                }
                finally
                {
                    completion.TrySetCanceled(cancellationToken);
                }
            });

            process.Exited += (sender, eventArgs) =>
            {
                completion.TrySetResult(process.ExitCode);
            };
            process.EnableRaisingEvents = true;

            // Short-circuit: exited while we were hooking event (i.e. race condition).
            if (process.HasExited)
                completion.TrySetResult(process.ExitCode);

            return completion.Task;
        }
    }
}
