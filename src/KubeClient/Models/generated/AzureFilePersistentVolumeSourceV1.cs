using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
    /// </summary>
    public partial class AzureFilePersistentVolumeSourceV1
    {
        /// <summary>
        ///     the name of secret that contains Azure Storage Account Name and Key
        /// </summary>
        [YamlMember(Alias = "secretName")]
        [JsonProperty("secretName", NullValueHandling = NullValueHandling.Include)]
        public string SecretName { get; set; }

        /// <summary>
        ///     the namespace of the secret that contains Azure Storage Account Name and Key default is the same as the Pod
        /// </summary>
        [YamlMember(Alias = "secretNamespace")]
        [JsonProperty("secretNamespace", NullValueHandling = NullValueHandling.Ignore)]
        public string SecretNamespace { get; set; }

        /// <summary>
        ///     Share Name
        /// </summary>
        [YamlMember(Alias = "shareName")]
        [JsonProperty("shareName", NullValueHandling = NullValueHandling.Include)]
        public string ShareName { get; set; }

        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
