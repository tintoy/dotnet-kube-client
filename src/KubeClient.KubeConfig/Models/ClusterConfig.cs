using YamlDotNet.Serialization;

namespace KubeClient.KubeConfig.Models
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
    }
}