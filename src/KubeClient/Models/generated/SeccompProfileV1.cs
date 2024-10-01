using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SeccompProfile defines a pod/container's seccomp profile settings. Only one profile source may be set.
    /// </summary>
    public partial class SeccompProfileV1
    {
        /// <summary>
        ///     localhostProfile indicates a profile defined in a file on the node should be used. The profile must be preconfigured on the node to work. Must be a descending path, relative to the kubelet's configured seccomp profile location. Must be set if type is "Localhost". Must NOT be set for any other type.
        /// </summary>
        [YamlMember(Alias = "localhostProfile")]
        [JsonProperty("localhostProfile", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalhostProfile { get; set; }

        /// <summary>
        ///     type indicates which kind of seccomp profile will be applied. Valid options are:
        ///     
        ///     Localhost - a profile defined in a file on the node should be used. RuntimeDefault - the container runtime default profile should be used. Unconfined - no profile should be applied.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }
    }
}
