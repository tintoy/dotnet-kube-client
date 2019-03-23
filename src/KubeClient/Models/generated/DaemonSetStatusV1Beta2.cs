using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetStatus represents the current status of a daemon set.
    /// </summary>
    public partial class DaemonSetStatusV1Beta2
    {
        /// <summary>
        ///     The number of nodes that are running at least 1 daemon pod and are supposed to run the daemon pod. More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [YamlMember(Alias = "currentNumberScheduled")]
        [JsonProperty("currentNumberScheduled", NullValueHandling = NullValueHandling.Include)]
        public int CurrentNumberScheduled { get; set; }

        /// <summary>
        ///     The total number of nodes that should be running the daemon pod (including nodes correctly running the daemon pod). More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [YamlMember(Alias = "desiredNumberScheduled")]
        [JsonProperty("desiredNumberScheduled", NullValueHandling = NullValueHandling.Include)]
        public int DesiredNumberScheduled { get; set; }

        /// <summary>
        ///     The number of nodes that are running the daemon pod, but are not supposed to run the daemon pod. More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [YamlMember(Alias = "numberMisscheduled")]
        [JsonProperty("numberMisscheduled", NullValueHandling = NullValueHandling.Include)]
        public int NumberMisscheduled { get; set; }

        /// <summary>
        ///     The total number of nodes that are running updated daemon pod
        /// </summary>
        [YamlMember(Alias = "updatedNumberScheduled")]
        [JsonProperty("updatedNumberScheduled", NullValueHandling = NullValueHandling.Ignore)]
        public int? UpdatedNumberScheduled { get; set; }

        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have one or more of the daemon pod running and available (ready for at least spec.minReadySeconds)
        /// </summary>
        [YamlMember(Alias = "numberAvailable")]
        [JsonProperty("numberAvailable", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberAvailable { get; set; }

        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have none of the daemon pod running and available (ready for at least spec.minReadySeconds)
        /// </summary>
        [YamlMember(Alias = "numberUnavailable")]
        [JsonProperty("numberUnavailable", NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberUnavailable { get; set; }

        /// <summary>
        ///     The most recent generation observed by the daemon set controller.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     Represents the latest available observations of a DaemonSet's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DaemonSetConditionV1Beta2> Conditions { get; } = new List<DaemonSetConditionV1Beta2>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     Count of hash collisions for the DaemonSet. The DaemonSet controller uses this field as a collision avoidance mechanism when it needs to create the name for the newest ControllerRevision.
        /// </summary>
        [YamlMember(Alias = "collisionCount")]
        [JsonProperty("collisionCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? CollisionCount { get; set; }

        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have one or more of the daemon pod running and ready.
        /// </summary>
        [YamlMember(Alias = "numberReady")]
        [JsonProperty("numberReady", NullValueHandling = NullValueHandling.Include)]
        public int NumberReady { get; set; }
    }
}
