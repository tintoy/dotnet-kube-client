using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Projection that may be projected along with other supported volume types
    /// </summary>
    [KubeObject("VolumeProjection", "v1")]
    public partial class VolumeProjectionV1
    {
        /// <summary>
        ///     information about the downwardAPI data to project
        /// </summary>
        [JsonProperty("downwardAPI")]
        public DownwardAPIProjectionV1 DownwardAPI { get; set; }

        /// <summary>
        ///     information about the configMap data to project
        /// </summary>
        [JsonProperty("configMap")]
        public ConfigMapProjectionV1 ConfigMap { get; set; }

        /// <summary>
        ///     information about the secret data to project
        /// </summary>
        [JsonProperty("secret")]
        public SecretProjectionV1 Secret { get; set; }
    }
}
