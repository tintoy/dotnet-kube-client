using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
    /// </summary>
    public partial class AzureFileVolumeSourceV1
    {
        /// <summary>
        ///     the name of secret that contains Azure Storage Account Name and Key
        /// </summary>
        [JsonProperty("secretName")]
        [YamlMember(Alias = "secretName")]
        public virtual string SecretName { get; set; }

        /// <summary>
        ///     Share Name
        /// </summary>
        [JsonProperty("shareName")]
        [YamlMember(Alias = "shareName")]
        public virtual string ShareName { get; set; }

        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public virtual bool ReadOnly { get; set; }
    }
}
