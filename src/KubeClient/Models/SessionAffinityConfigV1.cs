using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SessionAffinityConfig represents the configurations of session affinity.
    /// </summary>
    [KubeObject("SessionAffinityConfig", "v1")]
    public partial class SessionAffinityConfigV1
    {
        /// <summary>
        ///     clientIP contains the configurations of Client IP based session affinity.
        /// </summary>
        [JsonProperty("clientIP")]
        public ClientIPConfigV1 ClientIP { get; set; }
    }
}
