using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeRuntimeHandler is a set of runtime handler information.
    /// </summary>
    public partial class NodeRuntimeHandlerV1
    {
        /// <summary>
        ///     Runtime handler name. Empty for the default runtime handler.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Supported features.
        /// </summary>
        [YamlMember(Alias = "features")]
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public NodeRuntimeHandlerFeaturesV1 Features { get; set; }
    }
}
