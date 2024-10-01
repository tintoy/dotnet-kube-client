using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityLevelConfigurationSpec specifies the configuration of a priority level.
    /// </summary>
    public partial class PriorityLevelConfigurationSpecV1
    {
        /// <summary>
        ///     `limited` specifies how requests are handled for a Limited priority level. This field must be non-empty if and only if `type` is `"Limited"`.
        /// </summary>
        [YamlMember(Alias = "limited")]
        [JsonProperty("limited", NullValueHandling = NullValueHandling.Ignore)]
        public LimitedPriorityLevelConfigurationV1 Limited { get; set; }

        /// <summary>
        ///     `type` indicates whether this priority level is subject to limitation on request execution.  A value of `"Exempt"` means that requests of this priority level are not subject to a limit (and thus are never queued) and do not detract from the capacity made available to other priority levels.  A value of `"Limited"` means that (a) requests of this priority level _are_ subject to limits and (b) some of the server's limited capacity is made available exclusively to this priority level. Required.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     `exempt` specifies how requests are handled for an exempt priority level. This field MUST be empty if `type` is `"Limited"`. This field MAY be non-empty if `type` is `"Exempt"`. If empty and `type` is `"Exempt"` then the default values for `ExemptPriorityLevelConfiguration` apply.
        /// </summary>
        [YamlMember(Alias = "exempt")]
        [JsonProperty("exempt", NullValueHandling = NullValueHandling.Ignore)]
        public ExemptPriorityLevelConfigurationV1 Exempt { get; set; }
    }
}
