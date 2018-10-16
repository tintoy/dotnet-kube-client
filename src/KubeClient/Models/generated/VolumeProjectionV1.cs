using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Projection that may be projected along with other supported volume types
    /// </summary>
    public partial class VolumeProjectionV1
    {
        /// <summary>
        ///     information about the downwardAPI data to project
        /// </summary>
        [JsonProperty("downwardAPI")]
        [YamlMember(Alias = "downwardAPI")]
        public DownwardAPIProjectionV1 DownwardAPI { get; set; }

        /// <summary>
        ///     information about the serviceAccountToken data to project
        /// </summary>
        [JsonProperty("serviceAccountToken")]
        [YamlMember(Alias = "serviceAccountToken")]
        public ServiceAccountTokenProjectionV1 ServiceAccountToken { get; set; }

        /// <summary>
        ///     information about the configMap data to project
        /// </summary>
        [JsonProperty("configMap")]
        [YamlMember(Alias = "configMap")]
        public ConfigMapProjectionV1 ConfigMap { get; set; }

        /// <summary>
        ///     information about the secret data to project
        /// </summary>
        [JsonProperty("secret")]
        [YamlMember(Alias = "secret")]
        public SecretProjectionV1 Secret { get; set; }
    }
}
