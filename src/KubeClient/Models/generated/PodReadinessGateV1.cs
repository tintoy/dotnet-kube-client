using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodReadinessGate contains the reference to a pod condition
    /// </summary>
    public partial class PodReadinessGateV1
    {
        /// <summary>
        ///     ConditionType refers to a condition in the pod's condition list with matching type.
        /// </summary>
        [YamlMember(Alias = "conditionType")]
        [JsonProperty("conditionType", NullValueHandling = NullValueHandling.Include)]
        public string ConditionType { get; set; }
    }
}
