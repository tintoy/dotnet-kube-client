using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Extensions.KubeConfig.Models
{
    /// <summary>
    ///     Configuration for a K8s client authentication provider.
    /// </summary>
    public class AuthProviderConfig
    {
        /// <summary>
        ///     The provider name.
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     The raw provider configuration.
        /// </summary>
        [YamlMember(Alias = "config")]
        public Dictionary<string, object> Config { get; set; } = new Dictionary<string, object>();
    }
}
