using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetSpec is the specification of a daemon set.
    /// </summary>
    public partial class DaemonSetSpecV1
    {
        /// <summary>
        ///     The minimum number of seconds for which a newly created DaemonSet pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready).
        /// </summary>
        [JsonProperty("minReadySeconds")]
        [YamlMember(Alias = "minReadySeconds")]
        public int MinReadySeconds { get; set; }

        /// <summary>
        ///     A label query over pods that are managed by the daemon set. Must match in order to be controlled. It must match the pod template's labels. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
        /// </summary>
        [JsonProperty("selector")]
        [YamlMember(Alias = "selector")]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     An update strategy to replace existing DaemonSet pods with new pods.
        /// </summary>
        [JsonProperty("updateStrategy")]
        [YamlMember(Alias = "updateStrategy")]
        public DaemonSetUpdateStrategyV1 UpdateStrategy { get; set; }

        /// <summary>
        ///     The number of old history to retain to allow rollback. This is a pointer to distinguish between explicit zero and not specified. Defaults to 10.
        /// </summary>
        [JsonProperty("revisionHistoryLimit")]
        [YamlMember(Alias = "revisionHistoryLimit")]
        public int RevisionHistoryLimit { get; set; }

        /// <summary>
        ///     An object that describes the pod that will be created. The DaemonSet will create exactly one copy of this pod on every node that matches the template's node selector (or on every node if no node selector is specified). More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller#pod-template
        /// </summary>
        [JsonProperty("template")]
        [YamlMember(Alias = "template")]
        public PodTemplateSpecV1 Template { get; set; }
    }
}
