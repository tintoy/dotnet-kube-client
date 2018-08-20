using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodStatus represents information about the status of a pod. Status may trail the actual state of a system.
    /// </summary>
    public partial class PodStatusV1
    {
        /// <summary>
        ///     IP address of the host to which the pod is assigned. Empty if not yet scheduled.
        /// </summary>
        [JsonProperty("hostIP")]
        [YamlMember(Alias = "hostIP")]
        public string HostIP { get; set; }

        /// <summary>
        ///     IP address allocated to the pod. Routable at least within the cluster. Empty if not yet allocated.
        /// </summary>
        [JsonProperty("podIP")]
        [YamlMember(Alias = "podIP")]
        public string PodIP { get; set; }

        /// <summary>
        ///     A human readable message indicating details about why the pod is in this condition.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public string Message { get; set; }

        /// <summary>
        ///     Current condition of the pod. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-phase
        /// </summary>
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public string Phase { get; set; }

        /// <summary>
        ///     RFC 3339 date and time at which the object was acknowledged by the Kubelet. This is before the Kubelet pulled the container image(s) for the pod.
        /// </summary>
        [JsonProperty("startTime")]
        [YamlMember(Alias = "startTime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        ///     A brief CamelCase message indicating details about why the pod is in this state. e.g. 'OutOfDisk'
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Current service state of pod. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
        /// </summary>
        [StrategicMergePatch("type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public List<PodConditionV1> Conditions { get; set; } = new List<PodConditionV1>();

        /// <summary>
        ///     The list has one entry per container in the manifest. Each entry is currently the output of `docker inspect`. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-and-container-status
        /// </summary>
        [YamlMember(Alias = "containerStatuses")]
        [JsonProperty("containerStatuses", NullValueHandling = NullValueHandling.Ignore)]
        public List<ContainerStatusV1> ContainerStatuses { get; set; } = new List<ContainerStatusV1>();

        /// <summary>
        ///     The list has one entry per init container in the manifest. The most recent successful init container will have ready = true, the most recently started container will have startTime set. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-and-container-status
        /// </summary>
        [YamlMember(Alias = "initContainerStatuses")]
        [JsonProperty("initContainerStatuses", NullValueHandling = NullValueHandling.Ignore)]
        public List<ContainerStatusV1> InitContainerStatuses { get; set; } = new List<ContainerStatusV1>();

        /// <summary>
        ///     The Quality of Service (QOS) classification assigned to the pod based on resource requirements See PodQOSClass type for available QOS classes More info: https://github.com/kubernetes/kubernetes/blob/master/docs/design/resource-qos.md
        /// </summary>
        [JsonProperty("qosClass")]
        [YamlMember(Alias = "qosClass")]
        public string QosClass { get; set; }
    }
}
