using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JSONSchemaPropsOrBool represents JSONSchemaProps or a boolean value. Defaults to true for the boolean property.
    /// </summary>
    public partial class JSONSchemaPropsOrBoolV1Beta1 : Models.JSONSchemaPropsOrBoolV1Beta1
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
        [JsonProperty("Allows")]
        [YamlMember(Alias = "Allows")]
        public override bool Allows
        {
            get
            {
                return base.Allows;
            }
            set
            {
                base.Allows = value;

                __ModifiedProperties__.Add("Allows");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
