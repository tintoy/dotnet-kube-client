using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using YamlDotNet.Serialization;

namespace KubeClient.Extensions.KubeConfig.Models
{
    /// <summary>
    ///     Client configuration for a Kubernetes cluster.
    /// </summary>
    public class ClusterConfig
    {
        /// <summary>
        ///     The base address of the target server's API end-point.
        /// </summary>
        [YamlMember(Alias = "server")]
        public string Server { get; set; }

        /// <summary>
        ///     Allow insecure connections (i.e. skip TLS verification)?
        /// </summary>
        [YamlMember(Alias = "insecure-skip-tls-verify")]
        public bool AllowInsecure { get; set; }

        /// <summary>
        ///     A file containing the certification authority's PEM-encoded certificate.
        /// </summary>
        [YamlMember(Alias = "certificate-authority")]
        public string CertificateAuthorityFile { get; set; }

        /// <summary>
        ///     The certification authority's certificate (Base64-encoded bytes representing a PEM block).
        /// </summary>
        [YamlMember(Alias = "certificate-authority-data")]
        public string CertificateAuthorityData { get; set; }

        /// <summary>
        ///     Get the cluster's certification authority certificate.
        /// </summary>
        /// <returns>
        ///     The CA certificate, as an <see cref="X509Certificate2"/>, if configured; otherwise, <c>null</c>.
        /// </returns>
        public X509Certificate2 GetCACertificate()
        {
            byte[] certificateData;
            if (!String.IsNullOrWhiteSpace(CertificateAuthorityFile))
                certificateData = File.ReadAllBytes(CertificateAuthorityFile);
            else if (!String.IsNullOrWhiteSpace(CertificateAuthorityData))
                certificateData = Convert.FromBase64String(CertificateAuthorityData);
            else
                return null;

            return new X509Certificate2(certificateData);
        }
    }
}