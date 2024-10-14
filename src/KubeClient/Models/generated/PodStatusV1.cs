using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodStatus represents information about the status of a pod. Status may trail the actual state of a system, especially if the node that hosts the pod cannot contact the control plane.
    /// </summary>
    public partial class PodStatusV1
    {
        /// <summary>
        ///     hostIP holds the IP address of the host to which the pod is assigned. Empty if the pod has not started yet. A pod can be assigned to a node that has a problem in kubelet which in turns mean that HostIP will not be updated even if there is a node is assigned to pod
        /// </summary>
        [YamlMember(Alias = "hostIP")]
        [JsonProperty("hostIP", NullValueHandling = NullValueHandling.Ignore)]
        public string HostIP { get; set; }

        /// <summary>
        ///     podIP address allocated to the pod. Routable at least within the cluster. Empty if not yet allocated.
        /// </summary>
        [YamlMember(Alias = "podIP")]
        [JsonProperty("podIP", NullValueHandling = NullValueHandling.Ignore)]
        public string PodIP { get; set; }

        /// <summary>
        ///     A human readable message indicating details about why the pod is in this condition.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     nominatedNodeName is set only when this pod preempts other pods on the node, but it cannot be scheduled right away as preemption victims receive their graceful termination periods. This field does not guarantee that the pod will be scheduled on this node. Scheduler may decide to place the pod elsewhere if other nodes become available sooner. Scheduler may also decide to give the resources on this node to a higher priority pod that is created after preemption. As a result, this field may be different than PodSpec.nodeName when the pod is scheduled.
        /// </summary>
        [YamlMember(Alias = "nominatedNodeName")]
        [JsonProperty("nominatedNodeName", NullValueHandling = NullValueHandling.Ignore)]
        public string NominatedNodeName { get; set; }

        /// <summary>
        ///     The phase of a Pod is a simple, high-level summary of where the Pod is in its lifecycle. The conditions array, the reason and message fields, and the individual container status arrays contain more detail about the pod's status. There are five possible phase values:
        ///     
        ///     Pending: The pod has been accepted by the Kubernetes system, but one or more of the container images has not been created. This includes time before being scheduled as well as time spent downloading images over the network, which could take a while. Running: The pod has been bound to a node, and all of the containers have been created. At least one container is still running, or is in the process of starting or restarting. Succeeded: All containers in the pod have terminated in success, and will not be restarted. Failed: All containers in the pod have terminated, and at least one container has terminated in failure. The container either exited with non-zero status or was terminated by the system. Unknown: For some reason the state of the pod could not be obtained, typically due to an error in communicating with the host of the pod.
        ///     
        ///     More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-phase
        /// </summary>
        [YamlMember(Alias = "phase")]
        [JsonProperty("phase", NullValueHandling = NullValueHandling.Ignore)]
        public string Phase { get; set; }

        /// <summary>
        ///     Status of resources resize desired for pod's containers. It is empty if no resources resize is pending. Any changes to container resources will automatically set this to "Proposed"
        /// </summary>
        [YamlMember(Alias = "resize")]
        [JsonProperty("resize", NullValueHandling = NullValueHandling.Ignore)]
        public string Resize { get; set; }

        /// <summary>
        ///     RFC 3339 date and time at which the object was acknowledged by the Kubelet. This is before the Kubelet pulled the container image(s) for the pod.
        /// </summary>
        [YamlMember(Alias = "startTime")]
        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        ///     A brief CamelCase message indicating details about why the pod is in this state. e.g. 'Evicted'
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     Current service state of pod. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodConditionV1> Conditions { get; } = new List<PodConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     The list has one entry per container in the manifest. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-and-container-status
        /// </summary>
        [YamlMember(Alias = "containerStatuses")]
        [JsonProperty("containerStatuses", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerStatusV1> ContainerStatuses { get; } = new List<ContainerStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="ContainerStatuses"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeContainerStatuses() => ContainerStatuses.Count > 0;

        /// <summary>
        ///     Status for any ephemeral containers that have run in this pod.
        /// </summary>
        [YamlMember(Alias = "ephemeralContainerStatuses")]
        [JsonProperty("ephemeralContainerStatuses", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerStatusV1> EphemeralContainerStatuses { get; } = new List<ContainerStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="EphemeralContainerStatuses"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeEphemeralContainerStatuses() => EphemeralContainerStatuses.Count > 0;

        /// <summary>
        ///     hostIPs holds the IP addresses allocated to the host. If this field is specified, the first entry must match the hostIP field. This list is empty if the pod has not started yet. A pod can be assigned to a node that has a problem in kubelet which in turns means that HostIPs will not be updated even if there is a node is assigned to this pod.
        /// </summary>
        [MergeStrategy(Key = "ip")]
        [YamlMember(Alias = "hostIPs")]
        [JsonProperty("hostIPs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<HostIPV1> HostIPs { get; } = new List<HostIPV1>();

        /// <summary>
        ///     Determine whether the <see cref="HostIPs"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeHostIPs() => HostIPs.Count > 0;

        /// <summary>
        ///     The list has one entry per init container in the manifest. The most recent successful init container will have ready = true, the most recently started container will have startTime set. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-and-container-status
        /// </summary>
        [YamlMember(Alias = "initContainerStatuses")]
        [JsonProperty("initContainerStatuses", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerStatusV1> InitContainerStatuses { get; } = new List<ContainerStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="InitContainerStatuses"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeInitContainerStatuses() => InitContainerStatuses.Count > 0;

        /// <summary>
        ///     podIPs holds the IP addresses allocated to the pod. If this field is specified, the 0th entry must match the podIP field. Pods may be allocated at most 1 value for each of IPv4 and IPv6. This list is empty if no IPs have been allocated yet.
        /// </summary>
        [MergeStrategy(Key = "ip")]
        [YamlMember(Alias = "podIPs")]
        [JsonProperty("podIPs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodIPV1> PodIPs { get; } = new List<PodIPV1>();

        /// <summary>
        ///     Determine whether the <see cref="PodIPs"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePodIPs() => PodIPs.Count > 0;

        /// <summary>
        ///     The Quality of Service (QOS) classification assigned to the pod based on resource requirements See PodQOSClass type for available QOS classes More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-qos/#quality-of-service-classes
        /// </summary>
        [YamlMember(Alias = "qosClass")]
        [JsonProperty("qosClass", NullValueHandling = NullValueHandling.Ignore)]
        public string QosClass { get; set; }

        /// <summary>
        ///     Status of resource claims.
        /// </summary>
        [RetainKeysStrategy]
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "resourceClaimStatuses")]
        [JsonProperty("resourceClaimStatuses", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodResourceClaimStatusV1> ResourceClaimStatuses { get; } = new List<PodResourceClaimStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="ResourceClaimStatuses"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResourceClaimStatuses() => ResourceClaimStatuses.Count > 0;
    }
}
