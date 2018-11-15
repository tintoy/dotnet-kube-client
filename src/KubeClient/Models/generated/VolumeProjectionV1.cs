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
        [YamlMember(Alias = "downwardAPI")]
        [JsonProperty("downwardAPI", NullValueHandling = NullValueHandling.Ignore)]
        public DownwardAPIProjectionV1 DownwardAPI { get; set; }

        /// <summary>
        ///     information about the serviceAccountToken data to project
        /// </summary>
        [YamlMember(Alias = "serviceAccountToken")]
        [JsonProperty("serviceAccountToken", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceAccountTokenProjectionV1 ServiceAccountToken { get; set; }

        /// <summary>
        ///     information about the configMap data to project
        /// </summary>
        [YamlMember(Alias = "configMap")]
        [JsonProperty("configMap", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigMapProjectionV1 ConfigMap { get; set; }

        /// <summary>
        ///     information about the secret data to project
        /// </summary>
        [YamlMember(Alias = "secret")]
        [JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
        public SecretProjectionV1 Secret { get; set; }
    }
}
