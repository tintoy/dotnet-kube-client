using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     AzureFile represents an Azure File Service mount on the host and bind mount to the pod.
    /// </summary>
    public partial class AzureFileVolumeSourceV1 : Models.AzureFileVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     the name of secret that contains Azure Storage Account Name and Key
        /// </summary>
        [JsonProperty("secretName")]
        [YamlMember(Alias = "secretName")]
        public override string SecretName
        {
            get
            {
                return base.SecretName;
            }
            set
            {
                base.SecretName = value;

                __ModifiedProperties__.Add("SecretName");
            }
        }


        /// <summary>
        ///     Share Name
        /// </summary>
        [JsonProperty("shareName")]
        [YamlMember(Alias = "shareName")]
        public override string ShareName
        {
            get
            {
                return base.ShareName;
            }
            set
            {
                base.ShareName = value;

                __ModifiedProperties__.Add("ShareName");
            }
        }


        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;

                __ModifiedProperties__.Add("ReadOnly");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
