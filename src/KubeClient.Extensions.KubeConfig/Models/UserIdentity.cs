using YamlDotNet.Serialization;

namespace KubeClient.Extensions.KubeConfig.Models
{
    /// <summary>
    ///     A user identity that clients can use when authenticating to a Kubernetes cluster.
    /// </summary>
    public class UserIdentity
    {
        /// <summary>
        ///     The user identity name.
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     The user identity configuration.
        /// </summary>
        [YamlMember(Alias = "user")]
        public UserIdentityConfig Config { get; set; }
    }
}