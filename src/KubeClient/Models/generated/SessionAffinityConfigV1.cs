using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SessionAffinityConfig represents the configurations of session affinity.
    /// </summary>
    public partial class SessionAffinityConfigV1
    {
        /// <summary>
        ///     clientIP contains the configurations of Client IP based session affinity.
        /// </summary>
        [YamlMember(Alias = "clientIP")]
        [JsonProperty("clientIP", NullValueHandling = NullValueHandling.Ignore)]
        public ClientIPConfigV1 ClientIP { get; set; }
    }
}
