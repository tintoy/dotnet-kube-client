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
        [JsonProperty("groupVersion")]
        [YamlMember(Alias = "groupVersion")]
        public virtual string GroupVersion { get; set; }

        /// <summary>
        ///     version specifies the version in the form of "version". This is to save the clients the trouble of splitting the GroupVersion.
        /// </summary>
        [JsonProperty("version")]
        [YamlMember(Alias = "version")]
        public virtual string Version { get; set; }
    }
}
