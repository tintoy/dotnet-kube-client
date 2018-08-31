using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodStatus represents information about the status of a pod. Status may trail the actual state of a system.
    /// </summary>
    public partial class PodStatusV1 : Models.PodStatusV1
    {
        /// <summary>
        ///     IP address of the host to which the pod is assigned. Empty if not yet scheduled.
        /// </summary>
        [JsonProperty("hostIP")]
        [YamlMember(Alias = "hostIP")]
        public override string HostIP
        {
            get
            {
                return base.HostIP;
            }
            set
            {
                base.HostIP = value;

                __ModifiedProperties__.Add("HostIP");
            }
        }


        /// <summary>
        ///     IP address allocated to the pod. Routable at least within the cluster. Empty if not yet allocated.
        /// </summary>
        [JsonProperty("podIP")]
        [YamlMember(Alias = "podIP")]
        public override string PodIP
        {
            get
            {
                return base.PodIP;
            }
            set
            {
                base.PodIP = value;

                __ModifiedProperties__.Add("PodIP");
            }
        }


        /// <summary>
        ///     A human readable message indicating details about why the pod is in this condition.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public override string Message
        {
            get
            {
                return base.Message;
            }
            set
            {
                base.Message = value;

                __ModifiedProperties__.Add("Message");
            }
        }


        /// <summary>
        ///     Current condition of the pod. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-phase
        /// </summary>
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public override string Phase
        {
            get
            {
                return base.Phase;
            }
            set
            {
                base.Phase = value;

                __ModifiedProperties__.Add("Phase");
            }
        }


        /// <summary>
        ///     RFC 3339 date and time at which the object was acknowledged by the Kubelet. This is before the Kubelet pulled the container image(s) for the pod.
        /// </summary>
        [JsonProperty("startTime")]
        [YamlMember(Alias = "startTime")]
        public override DateTime? StartTime
        {
            get
            {
                return base.StartTime;
            }
            set
            {
                base.StartTime = value;

                __ModifiedProperties__.Add("StartTime");
            }
        }


        /// <summary>
        ///     A brief CamelCase message indicating details about why the pod is in this state. e.g. 'OutOfDisk'
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public override string Reason
        {
            get
            {
                return base.Reason;
            }
            set
            {
                base.Reason = value;

                __ModifiedProperties__.Add("Reason");
            }
        }


        /// <summary>
        ///     Current service state of pod. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.PodConditionV1> Conditions { get; set; } = new List<Models.PodConditionV1>();

        /// <summary>
        ///     The list has one entry per container in the manifest. Each entry is currently the output of `docker inspect`. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-and-container-status
        /// </summary>
        [YamlMember(Alias = "containerStatuses")]
        [JsonProperty("containerStatuses", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.ContainerStatusV1> ContainerStatuses { get; set; } = new List<Models.ContainerStatusV1>();

        /// <summary>
        ///     The list has one entry per init container in the manifest. The most recent successful init container will have ready = true, the most recently started container will have startTime set. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-and-container-status
        /// </summary>
        [YamlMember(Alias = "initContainerStatuses")]
        [JsonProperty("initContainerStatuses", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.ContainerStatusV1> InitContainerStatuses { get; set; } = new List<Models.ContainerStatusV1>();

        /// <summary>
        ///     The Quality of Service (QOS) classification assigned to the pod based on resource requirements See PodQOSClass type for available QOS classes More info: https://github.com/kubernetes/kubernetes/blob/master/docs/design/resource-qos.md
        /// </summary>
        [JsonProperty("qosClass")]
        [YamlMember(Alias = "qosClass")]
        public override string QosClass
        {
            get
            {
                return base.QosClass;
            }
            set
            {
                base.QosClass = value;

                __ModifiedProperties__.Add("QosClass");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
