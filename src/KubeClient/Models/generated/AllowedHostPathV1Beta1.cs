using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AllowedHostPath defines the host volume conditions that will be enabled by a policy for pods to use. It requires the path prefix to be defined.
    /// </summary>
    public partial class AllowedHostPathV1Beta1
    {
        /// <summary>
        ///     pathPrefix is the path prefix that the host volume must match. It does not support `*`. Trailing slashes are trimmed when validating the path prefix with a host path.
        ///     
        ///     Examples: `/foo` would allow `/foo`, `/foo/` and `/foo/bar` `/foo` would not allow `/food` or `/etc/foo`
        /// </summary>
        [JsonProperty("pathPrefix")]
        [YamlMember(Alias = "pathPrefix")]
        public string PathPrefix { get; set; }

        /// <summary>
        ///     when set to true, will allow host volumes matching the pathPrefix only if all volume mounts are readOnly.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public bool ReadOnly { get; set; }
    }
}
