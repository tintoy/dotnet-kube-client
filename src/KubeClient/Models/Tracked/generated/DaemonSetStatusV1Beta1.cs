using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DaemonSetStatus represents the current status of a daemon set.
    /// </summary>
    public partial class DaemonSetStatusV1Beta1 : Models.DaemonSetStatusV1Beta1, ITracked
    {
        /// <summary>
        ///     The number of nodes that are running at least 1 daemon pod and are supposed to run the daemon pod. More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [JsonProperty("currentNumberScheduled")]
        [YamlMember(Alias = "currentNumberScheduled")]
        public override int CurrentNumberScheduled
        {
            get
            {
                return base.CurrentNumberScheduled;
            }
            set
            {
                base.CurrentNumberScheduled = value;

                __ModifiedProperties__.Add("CurrentNumberScheduled");
            }
        }


        /// <summary>
        ///     The total number of nodes that should be running the daemon pod (including nodes correctly running the daemon pod). More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [JsonProperty("desiredNumberScheduled")]
        [YamlMember(Alias = "desiredNumberScheduled")]
        public override int DesiredNumberScheduled
        {
            get
            {
                return base.DesiredNumberScheduled;
            }
            set
            {
                base.DesiredNumberScheduled = value;

                __ModifiedProperties__.Add("DesiredNumberScheduled");
            }
        }


        /// <summary>
        ///     The number of nodes that are running the daemon pod, but are not supposed to run the daemon pod. More info: https://kubernetes.io/docs/concepts/workloads/controllers/daemonset/
        /// </summary>
        [JsonProperty("numberMisscheduled")]
        [YamlMember(Alias = "numberMisscheduled")]
        public override int NumberMisscheduled
        {
            get
            {
                return base.NumberMisscheduled;
            }
            set
            {
                base.NumberMisscheduled = value;

                __ModifiedProperties__.Add("NumberMisscheduled");
            }
        }


        /// <summary>
        ///     The total number of nodes that are running updated daemon pod
        /// </summary>
        [JsonProperty("updatedNumberScheduled")]
        [YamlMember(Alias = "updatedNumberScheduled")]
        public override int UpdatedNumberScheduled
        {
            get
            {
                return base.UpdatedNumberScheduled;
            }
            set
            {
                base.UpdatedNumberScheduled = value;

                __ModifiedProperties__.Add("UpdatedNumberScheduled");
            }
        }


        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have one or more of the daemon pod running and available (ready for at least spec.minReadySeconds)
        /// </summary>
        [JsonProperty("numberAvailable")]
        [YamlMember(Alias = "numberAvailable")]
        public override int NumberAvailable
        {
            get
            {
                return base.NumberAvailable;
            }
            set
            {
                base.NumberAvailable = value;

                __ModifiedProperties__.Add("NumberAvailable");
            }
        }


        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have none of the daemon pod running and available (ready for at least spec.minReadySeconds)
        /// </summary>
        [JsonProperty("numberUnavailable")]
        [YamlMember(Alias = "numberUnavailable")]
        public override int NumberUnavailable
        {
            get
            {
                return base.NumberUnavailable;
            }
            set
            {
                base.NumberUnavailable = value;

                __ModifiedProperties__.Add("NumberUnavailable");
            }
        }


        /// <summary>
        ///     The most recent generation observed by the daemon set controller.
        /// </summary>
        [JsonProperty("observedGeneration")]
        [YamlMember(Alias = "observedGeneration")]
        public override int ObservedGeneration
        {
            get
            {
                return base.ObservedGeneration;
            }
            set
            {
                base.ObservedGeneration = value;

                __ModifiedProperties__.Add("ObservedGeneration");
            }
        }


        /// <summary>
        ///     Count of hash collisions for the DaemonSet. The DaemonSet controller uses this field as a collision avoidance mechanism when it needs to create the name for the newest ControllerRevision.
        /// </summary>
        [JsonProperty("collisionCount")]
        [YamlMember(Alias = "collisionCount")]
        public override int CollisionCount
        {
            get
            {
                return base.CollisionCount;
            }
            set
            {
                base.CollisionCount = value;

                __ModifiedProperties__.Add("CollisionCount");
            }
        }


        /// <summary>
        ///     The number of nodes that should be running the daemon pod and have one or more of the daemon pod running and ready.
        /// </summary>
        [JsonProperty("numberReady")]
        [YamlMember(Alias = "numberReady")]
        public override int NumberReady
        {
            get
            {
                return base.NumberReady;
            }
            set
            {
                base.NumberReady = value;

                __ModifiedProperties__.Add("NumberReady");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
