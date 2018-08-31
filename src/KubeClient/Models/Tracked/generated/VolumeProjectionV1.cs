using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Projection that may be projected along with other supported volume types
    /// </summary>
    public partial class VolumeProjectionV1 : Models.VolumeProjectionV1
    {
        /// <summary>
        ///     information about the downwardAPI data to project
        /// </summary>
        [JsonProperty("downwardAPI")]
        [YamlMember(Alias = "downwardAPI")]
        public override Models.DownwardAPIProjectionV1 DownwardAPI
        {
            get
            {
                return base.DownwardAPI;
            }
            set
            {
                base.DownwardAPI = value;

                __ModifiedProperties__.Add("DownwardAPI");
            }
        }


        /// <summary>
        ///     information about the configMap data to project
        /// </summary>
        [JsonProperty("configMap")]
        [YamlMember(Alias = "configMap")]
        public override Models.ConfigMapProjectionV1 ConfigMap
        {
            get
            {
                return base.ConfigMap;
            }
            set
            {
                base.ConfigMap = value;

                __ModifiedProperties__.Add("ConfigMap");
            }
        }


        /// <summary>
        ///     information about the secret data to project
        /// </summary>
        [JsonProperty("secret")]
        [YamlMember(Alias = "secret")]
        public override Models.SecretProjectionV1 Secret
        {
            get
            {
                return base.Secret;
            }
            set
            {
                base.Secret = value;

                __ModifiedProperties__.Add("Secret");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
