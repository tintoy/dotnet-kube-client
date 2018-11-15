using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicationControllerSpec is the specification of a replication controller.
    /// </summary>
    public partial class ReplicationControllerSpecV1
    {
        /// <summary>
        ///     Template is the object that describes the pod that will be created if insufficient replicas are detected. This takes precedence over a TemplateRef. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller#pod-template
        /// </summary>
        [YamlMember(Alias = "template")]
        [JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
        public PodTemplateSpecV1 Template { get; set; }

        /// <summary>
        ///     Selector is a label query over pods that should match the Replicas count. If Selector is empty, it is defaulted to the labels present on the Pod template. Label keys and values that must match in order to be controlled by this replication controller, if empty defaulted to labels on Pod template. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Selector { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Selector"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSelector() => Selector.Count > 0;

        /// <summary>
        ///     Minimum number of seconds for which a newly created pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready)
        /// </summary>
        [YamlMember(Alias = "minReadySeconds")]
        [JsonProperty("minReadySeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinReadySeconds { get; set; }

        /// <summary>
        ///     Replicas is the number of desired replicas. This is a pointer to distinguish between explicit zero and unspecified. Defaults to 1. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller#what-is-a-replicationcontroller
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? Replicas { get; set; }
    }
}
