using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace KubeClient.Extensions.WebSockets.Tests.Server
{
    /// <summary>
    ///     Adapter used to capture WebSockets accepted by the test server and provide them to calling test.
    /// </summary>
    /// <remarks>
    ///     Each OnXXX delegate receives an accepted WebSocket and returns a <see cref="Task"/>.
    ///     The server will close the network connection when this <see cref="Task"/> completes.
    /// </remarks>
    public class WebSocketTestAdapter
    {
        /// <summary>
        ///     Called when the server accepts a WebSocket for its mock exec-in-pod API.
        /// </summary>
        public Func<WebSocket, Task> OnPodExec { get; set; }
    }
}