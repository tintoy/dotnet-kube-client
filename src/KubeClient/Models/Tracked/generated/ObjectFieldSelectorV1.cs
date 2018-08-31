using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ObjectFieldSelector selects an APIVersioned field of an object.
    /// </summary>
    public partial class ObjectFieldSelectorV1 : Models.ObjectFieldSelectorV1
    {
        /// <summary>
        ///     Path of the field to select in the specified API version.
        /// </summary>
        [JsonProperty("fieldPath")]
        [YamlMember(Alias = "fieldPath")]
        public override string FieldPath
        {
            get
            {
                return base.FieldPath;
            }
            set
            {
                base.FieldPath = value;

                __ModifiedProperties__.Add("FieldPath");
            }
        }


        /// <summary>
        ///     Version of the schema the FieldPath is written in terms of, defaults to "v1".
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public override string ApiVersion
        {
            get
            {
                return base.ApiVersion;
            }
            set
            {
                base.ApiVersion = value;

                __ModifiedProperties__.Add("ApiVersion");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
