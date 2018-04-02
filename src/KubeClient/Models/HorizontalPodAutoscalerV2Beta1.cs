using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscaler is the configuration for a horizontal pod autoscaler, which automatically manages the replica count of any resource implementing the scale subresource based on the metrics specified.
    /// </summary>
    [KubeObject("HorizontalPodAutoscaler", "autoscaling/v2beta1")]
    public partial class HorizontalPodAutoscalerV2Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the specification for the behaviour of the autoscaler. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status.
        /// </summary>
        [JsonProperty("spec")]
        public HorizontalPodAutoscalerSpecV2Beta1 Spec { get; set; }

        /// <summary>
        ///     status is the current information about the autoscaler.
        /// </summary>
        [JsonProperty("status")]
        public HorizontalPodAutoscalerStatusV2Beta1 Status { get; set; }
    }
}
