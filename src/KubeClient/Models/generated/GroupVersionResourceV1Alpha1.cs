using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     The names of the group, the version, and the resource.
    /// </summary>
    public partial class GroupVersionResourceV1Alpha1
    {
        /// <summary>
        ///     The name of the resource.
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public string Resource { get; set; }

        /// <summary>
        ///     The name of the version.
        /// </summary>
        [YamlMember(Alias = "version")]
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        /// <summary>
        ///     The name of the group.
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
    }
}
