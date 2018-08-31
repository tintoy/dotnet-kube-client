using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JSONSchemaPropsOrStringArray represents a JSONSchemaProps or a string array.
    /// </summary>
    public partial class JSONSchemaPropsOrStringArrayV1Beta1 : Models.JSONSchemaPropsOrStringArrayV1Beta1, ITracked
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Schema")]
        [YamlMember(Alias = "Schema")]
        public override Models.JSONSchemaPropsV1Beta1 Schema
        {
            get
            {
                return base.Schema;
            }
            set
            {
                base.Schema = value;

                __ModifiedProperties__.Add("Schema");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "Property")]
        [JsonProperty("Property", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Property { get; set; } = new List<string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
