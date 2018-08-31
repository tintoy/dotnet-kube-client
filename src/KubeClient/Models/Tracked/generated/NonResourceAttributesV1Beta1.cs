using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NonResourceAttributes includes the authorization attributes available for non-resource requests to the Authorizer interface
    /// </summary>
    public partial class NonResourceAttributesV1Beta1 : Models.NonResourceAttributesV1Beta1, ITracked
    {
        /// <summary>
        ///     Verb is the standard HTTP verb
        /// </summary>
        [JsonProperty("verb")]
        [YamlMember(Alias = "verb")]
        public override string Verb
        {
            get
            {
                return base.Verb;
            }
            set
            {
                base.Verb = value;

                __ModifiedProperties__.Add("Verb");
            }
        }


        /// <summary>
        ///     Path is the URL path of the request
        /// </summary>
        [JsonProperty("path")]
        [YamlMember(Alias = "path")]
        public override string Path
        {
            get
            {
                return base.Path;
            }
            set
            {
                base.Path = value;

                __ModifiedProperties__.Add("Path");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
