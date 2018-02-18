using YamlDotNet.Serialization;

namespace KubeClient.KubeConfig.Models
{
    /// <summary>
    ///     User identity configuration for a Kubernetes client.
    /// </summary>
    public class UserIdentityConfig
    {
        /// <summary>
        ///     A Base64-encoded string containing the access token to use (will be placed in the "Authorization" header).
        /// </summary>
        [YamlMember(Alias = "token")]
        public string Token { get; set; }

        /// <summary>
        ///     A file containing the PEM-encoded client certificate to use.
        /// </summary>
        [YamlMember(Alias = "client-certificate")]
        public string ClientCertificateFile { get; set; }

        /// <summary>
        ///     A file containing the PEM-encoded private key associated with the client certificate to use.
        /// </summary>
        [YamlMember(Alias = "client-key")]
        public string ClientKeyFile { get; set; }

        /// <summary>
        ///     The client certificate to use (Base64-encoded bytes representing a PEM block).
        /// </summary>
        [YamlMember(Alias = "client-certificate-data")]
        public string ClientCertificateData { get; set; }

        /// <summary>
        ///     The private key to use (Base64-encoded bytes representing a PEM block).
        /// </summary>
        [YamlMember(Alias = "client-key-data")]
        public string ClientKeyData { get; set; }
    }
}