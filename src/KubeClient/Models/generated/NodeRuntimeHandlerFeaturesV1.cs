using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeRuntimeHandlerFeatures is a set of features implemented by the runtime handler.
    /// </summary>
    public partial class NodeRuntimeHandlerFeaturesV1
    {
        /// <summary>
        ///     RecursiveReadOnlyMounts is set to true if the runtime handler supports RecursiveReadOnlyMounts.
        /// </summary>
        [YamlMember(Alias = "recursiveReadOnlyMounts")]
        [JsonProperty("recursiveReadOnlyMounts", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RecursiveReadOnlyMounts { get; set; }

        /// <summary>
        ///     UserNamespaces is set to true if the runtime handler supports UserNamespaces, including for volumes.
        /// </summary>
        [YamlMember(Alias = "userNamespaces")]
        [JsonProperty("userNamespaces", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UserNamespaces { get; set; }
    }
}
