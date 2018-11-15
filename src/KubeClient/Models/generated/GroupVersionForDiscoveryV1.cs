using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     GroupVersion contains the "group/version" and "version" string of a version. It is made a struct to keep extensibility.
    /// </summary>
    public partial class GroupVersionForDiscoveryV1
    {
        /// <summary>
        ///     groupVersion specifies the API group and version in the form "group/version"
        /// </summary>
        [YamlMember(Alias = "groupVersion")]
        [JsonProperty("groupVersion", NullValueHandling = NullValueHandling.Include)]
        public string GroupVersion { get; set; }

        /// <summary>
        ///     version specifies the version in the form of "version". This is to save the clients the trouble of splitting the GroupVersion.
        /// </summary>
        [YamlMember(Alias = "version")]
        [JsonProperty("version", NullValueHandling = NullValueHandling.Include)]
        public string Version { get; set; }
    }
}
