using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     HTTPHeader describes a custom header to be used in HTTP probes
    /// </summary>
    public partial class HTTPHeaderV1 : Models.HTTPHeaderV1, ITracked
    {
        /// <summary>
        ///     The header field name
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     The header field value
        /// </summary>
        [JsonProperty("value")]
        [YamlMember(Alias = "value")]
        public override string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;

                __ModifiedProperties__.Add("Value");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
