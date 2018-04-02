using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     defines the host volume conditions that will be enabled by a policy for pods to use. It requires the path prefix to be defined.
    /// </summary>
    [KubeObject("AllowedHostPath", "v1beta1")]
    public partial class AllowedHostPathV1Beta1
    {
        /// <summary>
        ///     is the path prefix that the host volume must match. It does not support `*`. Trailing slashes are trimmed when validating the path prefix with a host path.
        ///     
        ///     Examples: `/foo` would allow `/foo`, `/foo/` and `/foo/bar` `/foo` would not allow `/food` or `/etc/foo`
        /// </summary>
        [JsonProperty("pathPrefix")]
        public string PathPrefix { get; set; }
    }
}
