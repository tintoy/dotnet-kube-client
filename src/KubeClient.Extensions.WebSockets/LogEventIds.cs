using Microsoft.Extensions.Logging;

namespace KubeClient.Extensions.WebSockets
{
    /// <summary>
    ///     Well-known event Ids for KubeClient.Extensions.WebSockets log entries.
    /// </summary>
    static class EventIds
    {
        /// <summary>
        ///     Well-known event Ids for <see cref="WebSockets.K8sMultiplexer"/>.
        /// </summary>
        public static class K8sMultiplexer
        {
            /// <summary>
            ///     An unexpected error occurred while running the send  pump.
            /// </summary>
            public static EventId SendPumpError = new EventId(400, nameof(SendPumpError));

            /// <summary>
            ///     An unexpected error occurred while running the receive pump.
            /// </summary>
            public static EventId ReceivePumpError = new EventId(401, nameof(ReceivePumpError));

            /// <summary>
            ///     An error occurred while stopping the multiplexer during disposal.
            /// </summary>
            public static EventId DisposeStopError = new EventId(520, nameof(DisposeStopError));
        }
    }
}