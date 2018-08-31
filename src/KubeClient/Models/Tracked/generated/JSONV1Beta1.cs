using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JSON represents any valid JSON value. These types are supported: bool, int64, float64, string, []interface{}, map[string]interface{} and nil.
    /// </summary>
    public partial class JSONV1Beta1 : Models.JSONV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Raw")]
        [YamlMember(Alias = "Raw")]
        public override string Raw
        {
            get
            {
                return base.Raw;
            }
            set
            {
                base.Raw = value;

                __ModifiedProperties__.Add("Raw");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
