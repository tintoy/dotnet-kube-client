using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EnvFromSource represents the source of a set of ConfigMaps
    /// </summary>
    public partial class EnvFromSourceV1 : Models.EnvFromSourceV1, ITracked
    {
        /// <summary>
        ///     The ConfigMap to select from
        /// </summary>
        [JsonProperty("configMapRef")]
        [YamlMember(Alias = "configMapRef")]
        public override Models.ConfigMapEnvSourceV1 ConfigMapRef
        {
            get
            {
                return base.ConfigMapRef;
            }
            set
            {
                base.ConfigMapRef = value;

                __ModifiedProperties__.Add("ConfigMapRef");
            }
        }


        /// <summary>
        ///     The Secret to select from
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public override Models.SecretEnvSourceV1 SecretRef
        {
            get
            {
                return base.SecretRef;
            }
            set
            {
                base.SecretRef = value;

                __ModifiedProperties__.Add("SecretRef");
            }
        }


        /// <summary>
        ///     An optional identifer to prepend to each key in the ConfigMap. Must be a C_IDENTIFIER.
        /// </summary>
        [JsonProperty("prefix")]
        [YamlMember(Alias = "prefix")]
        public override string Prefix
        {
            get
            {
                return base.Prefix;
            }
            set
            {
                base.Prefix = value;

                __ModifiedProperties__.Add("Prefix");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
