using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ExternalDocumentation allows referencing an external resource for extended documentation.
    /// </summary>
    public partial class ExternalDocumentationV1Beta1 : Models.ExternalDocumentationV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("url")]
        [YamlMember(Alias = "url")]
        public override string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                base.Url = value;

                __ModifiedProperties__.Add("Url");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("description")]
        [YamlMember(Alias = "description")]
        public override string Description
        {
            get
            {
                return base.Description;
            }
            set
            {
                base.Description = value;

                __ModifiedProperties__.Add("Description");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
