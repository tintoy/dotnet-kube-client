using HTTPlease;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient
{
    using ResourceClients;
    using Extensions.WebSockets;
    using System.Net.WebSockets;

    /// <summary>
    ///     WebSocket-related extension methods for Kubernetes resource clients.
    /// </summary>
    public static class ResourceClientWebSocketExtensions
    {
        /// <summary>
        ///     Start a new process in a Pod's container and open a multiplexed connection to its STDIN / STDOUT / STDERR.
        /// </summary>
        /// <param name="podClient">
        ///     The <see cref="PodClientV1"/>.
        /// </param>
        /// <param name="podName">
        ///     The name of the target pod.
        /// </param>
        /// <param name="command">
        ///     The command to execute (command-line arguments not supported yet).
        /// </param>
        /// <param name="stdin">
        ///     Create an output stream connected to the target container's STDIN?
        /// </param>
        /// <param name="stdout">
        ///     Create an output stream connected to the target container's STDOUT?
        /// </param>
        /// <param name="stderr">
        ///     Create an output stream connected to the target container's STDERR?
        /// </param>
        /// <param name="tty">
        ///     Attach a TTY to the process?
        /// </param>
        /// <param name="container">
        ///     The name of the target container within the pod.
        /// 
        ///     Optional if the pod only has a single container.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The name of the target Kubernetes namespace that contains the pod.
        /// 
        ///     If not specified, the <see cref="KubeApiClient.DefaultNamespace"/> is used.
        /// </param>
        /// <param name="cancellation">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="K8sMultiplexer"/> that provides access to the input and output streams.
        /// </returns>
        public static Task<K8sMultiplexer> ExecAndConnect(this IPodClientV1 podClient, string podName, string command, bool stdin = false, bool stdout = true, bool stderr = false, bool tty = false, string container = null, string kubeNamespace = null, CancellationToken cancellation = default)
        {
            if (podClient == null)
                throw new ArgumentNullException(nameof(podClient));

            byte[] outputStreamIndexes = stdin ? new byte[1] { 0 } : new byte[0];
            byte[] inputStreamIndexes;
            if (stdout && stderr)
                inputStreamIndexes = new byte[2] { 1, 2 };
            else if (stdout)
                inputStreamIndexes = new byte[1] { 1 };
            else if (stderr)
                inputStreamIndexes = new byte[1] { 2 };
            else if (!stdin)
                throw new InvalidOperationException("Must specify at least one of STDIN, STDOUT, or STDERR.");
            else
                inputStreamIndexes = new byte[0];
            
            return podClient.ExecAndConnectRaw(podName, command, stdin, stdout, stderr, tty, container, kubeNamespace, cancellation)
                .Multiplexed(inputStreamIndexes, outputStreamIndexes,
                    loggerFactory: podClient.KubeClient.LoggerFactory
                );
        }

        /// <summary>
        ///     Start a new process in a Pod's container and open a WebSocket connection to its STDIN / STDOUT / STDERR.
        /// </summary>
        /// <param name="podClient">
        ///     The <see cref="PodClientV1"/>.
        /// </param>
        /// <param name="podName">
        ///     The name of the target pod.
        /// </param>
        /// <param name="command">
        ///     The command to execute (command-line arguments not supported yet).
        /// </param>
        /// <param name="stdin">
        ///     Create an output stream connected to the target container's STDIN?
        /// </param>
        /// <param name="stdout">
        ///     Create an output stream connected to the target container's STDOUT?
        /// </param>
        /// <param name="stderr">
        ///     Create an output stream connected to the target container's STDERR?
        /// </param>
        /// <param name="tty">
        ///     Attach a TTY to the process?
        /// </param>
        /// <param name="container">
        ///     The name of the target container within the pod.
        /// 
        ///     Optional if the pod only has a single container.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The name of the target Kubernetes namespace that contains the pod.
        /// 
        ///     If not specified, the <see cref="KubeApiClient.DefaultNamespace"/> is used.
        /// </param>
        /// <param name="cancellation">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The connected <see cref="WebSocket"/>.
        /// </returns>
        public static async Task<WebSocket> ExecAndConnectRaw(this IPodClientV1 podClient, string podName, string command, bool stdin = false, bool stdout = true, bool stderr = false, bool tty = false, string container = null, string kubeNamespace = null, CancellationToken cancellation = default)
        {
            if (podClient == null)
                throw new ArgumentNullException(nameof(podClient));

            var uriTemplateParameters = new
            {
                PodName = podName,
                Command = command,
                StdIn = stdin,
                StdOut = stdout,
                StdErr = stderr,
                TTY = tty,
                Container = container,
                KubeNamespace = kubeNamespace ?? podClient.KubeClient.DefaultNamespace
            };

            return await podClient.KubeClient.ConnectWebSocket(
                "api/v1/namespaces/{KubeNamespace}/pods/{PodName}/exec?stdin={StdIn?}&stdout={StdOut?}&stderr={StdErr?}&tty={TTY?}&command={Command}&container={Container?}",
                uriTemplateParameters,
                cancellation
            );
        }
    }
}
