using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace KubeClient.Extensions.WebSockets
{
    /// <summary>
    ///     Options for connecting to Kubernetes web sockets.
    /// </summary>
    public class K8sWebSocketOptions
    {
        /// <summary>
        ///     The default size (in bytes) for WebSocket send / receive buffers.
        /// </summary>
        public static readonly int DefaultBufferSize = 2048;

        /// <summary>
        ///     Create new <see cref="K8sWebSocketOptions"/>.
        /// </summary>
        public K8sWebSocketOptions()
        {
        }

        /// <summary>
        ///     The requested size (in bytes) of the WebSocket send buffer.
        /// </summary>
        public int SendBufferSize { get; set; } = 2048;

        /// <summary>
        ///     The requested size (in bytes) of the WebSocket receive buffer.
        /// </summary>
        public int ReceiveBufferSize { get; set; } = 2048;

        /// <summary>
        ///     Custom request headers (if any).
        /// </summary>
        public Dictionary<string, string> RequestHeaders { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Requested sub-protocols (if any).
        /// </summary>
        public List<string> RequestedSubProtocols { get; } = new List<string>();

        /// <summary>
        ///     Client certificates (if any) to use for authentication.
        /// </summary>
        public List<X509Certificate2> ClientCertificates = new List<X509Certificate2>();

        /// <summary>
        ///     An optional delegate to use for authenticating the remote server certificate.
        /// </summary>
        public RemoteCertificateValidationCallback ServerCertificateCustomValidationCallback { get; set; }

        /// <summary>
        ///     An <see cref="SslProtocols"/> value representing the SSL protocols that the client supports.
        /// </summary>
        public SslProtocols EnabledSslProtocols { get; set; } = SslProtocols.Tls;
        
        /// <summary>
        ///     The WebSocket keep-alive interval.
        /// </summary>
        public TimeSpan KeepAliveInterval { get; set; } = TimeSpan.FromSeconds(5);
    }
}