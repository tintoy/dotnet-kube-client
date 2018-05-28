using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Dictionary<string, string> RequestHeaders { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

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
        /// <remarks>
        ///     Default is <see cref="SslProtocols.None"/>, which lets the platform select the most appropriate protocol.
        /// </remarks>
        public SslProtocols EnabledSslProtocols { get; set; } = SslProtocols.None;
        
        /// <summary>
        ///     The WebSocket keep-alive interval.
        /// </summary>
        public TimeSpan KeepAliveInterval { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        ///     Create <see cref="K8sWebSocketOptions"/> using the client's authentication settings.
        /// </summary>
        /// <param name="client">
        ///     The <see cref="KubeApiClient"/>.
        /// </param>
        /// <returns>
        ///     The configured <see cref="K8sWebSocketOptions"/>.
        /// </returns>
        public static K8sWebSocketOptions FromClientOptions(IKubeApiClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            var socketOptions = new K8sWebSocketOptions();

            KubeClientOptions clientOptions = client.GetClientOptions();
            
            if (!String.IsNullOrWhiteSpace(clientOptions.AccessToken))
                socketOptions.RequestHeaders["Authorization"] = $"Bearer {clientOptions.AccessToken}";

            if (clientOptions.ClientCertificate != null)
                socketOptions.ClientCertificates.Add(clientOptions.ClientCertificate);

            if (clientOptions.CertificationAuthorityCertificate != null)
            {
                socketOptions.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
				{
					if (sslPolicyErrors != SslPolicyErrors.RemoteCertificateChainErrors)
						return false;

					try
					{
						using (X509Chain certificateChain = new X509Chain())
						{
							certificateChain.ChainPolicy.ExtraStore.Add(clientOptions.CertificationAuthorityCertificate);
							certificateChain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;
							certificateChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
							
							return certificateChain.Build(
                                (X509Certificate2)certificate
                            );
						}
					}
					catch (Exception chainException)
					{
                        Debug.WriteLine(chainException);

						return false;
					}
				};
            }
            else if (clientOptions.AllowInsecure)
                socketOptions.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            return socketOptions;
        }
    }
}