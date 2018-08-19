using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetStatus represents the current status of a daemon set.
    /// </summary>
    public partial class DaemonSetStatusV1Beta1
    {
        /// <summary>
        ///     The number of nodes that are running at least 1 daemon pod and are supposed to run the daemon pod. More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [JsonProperty("currentNumberScheduled")]
        [YamlMember(Alias = "currentNumberScheduled")]
        public int CurrentNumberScheduled { get; set; }

        /// <summary>
        ///     The total number of nodes that should be running the daemon pod (including nodes correctly running the daemon pod). More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [JsonProperty("desiredNumberScheduled")]
        [YamlMember(Alias = "desiredNumberScheduled")]
        public int DesiredNumberScheduled { get; set; }

        /// <summary>
        ///     The number of nodes that are running the daemon pod, but are not supposed to run the daemon pod. More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [JsonProperty("numberMisscheduled")]
        [YamlMember(Alias = "numberMisscheduled")]
        public int NumberMisscheduled { get; set; }

        /// <summary>
        ///     The total number of nodes that are running updated daemon pod
        /// </summary>
        [JsonProperty("updatedNumberScheduled")]
        [YamlMember(Alias = "updatedNumberScheduled")]
        public int UpdatedNumberScheduled { get; set; }

        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have one or more of the daemon pod running and available (ready for at least spec.minReadySeconds)
        /// </summary>
        [JsonProperty("numberAvailable")]
        [YamlMember(Alias = "numberAvailable")]
        public int NumberAvailable { get; set; }

        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have none of the daemon pod running and available (ready for at least spec.minReadySeconds)
        /// </summary>
        [JsonProperty("numberUnavailable")]
        [YamlMember(Alias = "numberUnavailable")]
        public int NumberUnavailable { get; set; }

        /// <summary>
        ///     The most recent generation observed by the daemon set controller.
        /// </summary>
        [JsonProperty("observedGeneration")]
        [YamlMember(Alias = "observedGeneration")]
        public int ObservedGeneration { get; set; }

        /// <summary>
        ///     Count of hash collisions for the DaemonSet. The DaemonSet controller uses this field as a collision avoidance mechanism when it needs to create the name for the newest ControllerRevision.
        /// </summary>
        [JsonProperty("collisionCount")]
        [YamlMember(Alias = "collisionCount")]
        public int CollisionCount { get; set; }

        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have one or more of the daemon pod running and ready.
        /// </summary>
        [JsonProperty("numberReady")]
        [YamlMember(Alias = "numberReady")]
        public int NumberReady { get; set; }
    }
}
