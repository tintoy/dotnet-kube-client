using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     GroupVersion contains the "group/version" and "version" string of a version. It is made a struct to keep extensibility.
    /// </summary>
    [KubeResource("GroupVersionForDiscovery", "v1")]
    public class GroupVersionForDiscoveryV1
    {
        /// <summary>
        ///     groupVersion specifies the API group and version in the form "group/version"
        /// </summary>
        [JsonProperty("groupVersion")]
        public string GroupVersion { get; set; }

        /// <summary>
        ///     version specifies the version in the form of "version". This is to save the clients the trouble of splitting the GroupVersion.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
