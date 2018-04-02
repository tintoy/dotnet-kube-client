using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSpec is a description of a pod.
    /// </summary>
    [KubeObject("PodSpec", "v1")]
    public partial class PodSpecV1
    {
        /// <summary>
        ///     Use the host's ipc namespace. Optional: Default to false.
        /// </summary>
        [JsonProperty("hostIPC")]
        public bool HostIPC { get; set; }

        /// <summary>
        ///     Use the host's pid namespace. Optional: Default to false.
        /// </summary>
        [JsonProperty("hostPID")]
        public bool HostPID { get; set; }

        /// <summary>
        ///     Specifies the hostname of the Pod If not specified, the pod's hostname will be set to a system-defined value.
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        ///     NodeName is a request to schedule this pod onto a specific node. If it is non-empty, the scheduler simply schedules this pod onto that node, assuming that it fits resource requirements.
        /// </summary>
        [JsonProperty("nodeName")]
        public string NodeName { get; set; }

        /// <summary>
        ///     If specified, indicates the pod's priority. "SYSTEM" is a special keyword which indicates the highest priority. Any other name must be defined by creating a PriorityClass object with that name. If not specified, the pod priority will be default or zero if there is no default.
        /// </summary>
        [JsonProperty("priorityClassName")]
        public string PriorityClassName { get; set; }

        /// <summary>
        ///     If specified, the pod will be dispatched by specified scheduler. If not specified, the pod will be dispatched by default scheduler.
        /// </summary>
        [JsonProperty("schedulerName")]
        public string SchedulerName { get; set; }

        /// <summary>
        ///     ServiceAccountName is the name of the ServiceAccount to use to run this pod. More info: https://kubernetes.io/docs/tasks/configure-pod-container/configure-service-account/
        /// </summary>
        [JsonProperty("serviceAccountName")]
        public string ServiceAccountName { get; set; }

        /// <summary>
        ///     Host networking requested for this pod. Use the host's network namespace. If this option is set, the ports that will be used must be specified. Default to false.
        /// </summary>
        [JsonProperty("hostNetwork")]
        public bool HostNetwork { get; set; }

        /// <summary>
        ///     AutomountServiceAccountToken indicates whether a service account token should be automatically mounted.
        /// </summary>
        [JsonProperty("automountServiceAccountToken")]
        public bool AutomountServiceAccountToken { get; set; }

        /// <summary>
        ///     If specified, the fully qualified Pod hostname will be "&lt;hostname&gt;.&lt;subdomain&gt;.&lt;pod namespace&gt;.svc.&lt;cluster domain&gt;". If not specified, the pod will not have a domainname at all.
        /// </summary>
        [JsonProperty("subdomain")]
        public string Subdomain { get; set; }

        /// <summary>
        ///     NodeSelector is a selector which must be true for the pod to fit on a node. Selector which must match a node's labels for the pod to be scheduled on that node. More info: https://kubernetes.io/docs/concepts/configuration/assign-pod-node/
        /// </summary>
        [JsonProperty("nodeSelector", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> NodeSelector { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Optional duration in seconds the pod may be active on the node relative to StartTime before the system will actively try to mark it failed and kill associated containers. Value must be a positive integer.
        /// </summary>
        [JsonProperty("activeDeadlineSeconds")]
        public int? ActiveDeadlineSeconds { get; set; }

        /// <summary>
        ///     List of containers belonging to the pod. Containers cannot currently be added or removed. There must be at least one container in a Pod. Cannot be updated.
        /// </summary>
        [JsonProperty("containers", NullValueHandling = NullValueHandling.Ignore)]
        public List<ContainerV1> Containers { get; set; } = new List<ContainerV1>();

        /// <summary>
        ///     HostAliases is an optional list of hosts and IPs that will be injected into the pod's hosts file if specified. This is only valid for non-hostNetwork pods.
        /// </summary>
        [JsonProperty("hostAliases", NullValueHandling = NullValueHandling.Ignore)]
        public List<HostAliasV1> HostAliases { get; set; } = new List<HostAliasV1>();

        /// <summary>
        ///     ImagePullSecrets is an optional list of references to secrets in the same namespace to use for pulling any of the images used by this PodSpec. If specified, these secrets will be passed to individual puller implementations for them to use. For example, in the case of docker, only DockerConfig type secrets are honored. More info: https://kubernetes.io/docs/concepts/containers/images#specifying-imagepullsecrets-on-a-pod
        /// </summary>
        [JsonProperty("imagePullSecrets", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocalObjectReferenceV1> ImagePullSecrets { get; set; } = new List<LocalObjectReferenceV1>();

        /// <summary>
        ///     List of initialization containers belonging to the pod. Init containers are executed in order prior to containers being started. If any init container fails, the pod is considered to have failed and is handled according to its restartPolicy. The name for an init container or normal container must be unique among all containers. Init containers may not have Lifecycle actions, Readiness probes, or Liveness probes. The resourceRequirements of an init container are taken into account during scheduling by finding the highest request/limit for each resource type, and then using the max of of that value or the sum of the normal containers. Limits are applied to init containers in a similar fashion. Init containers cannot currently be added or removed. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/init-containers/
        /// </summary>
        [JsonProperty("initContainers", NullValueHandling = NullValueHandling.Ignore)]
        public List<ContainerV1> InitContainers { get; set; } = new List<ContainerV1>();

        /// <summary>
        ///     Optional duration in seconds the pod needs to terminate gracefully. May be decreased in delete request. Value must be non-negative integer. The value zero indicates delete immediately. If this value is nil, the default grace period will be used instead. The grace period is the duration in seconds after the processes running in the pod are sent a termination signal and the time when the processes are forcibly halted with a kill signal. Set this value longer than the expected cleanup time for your process. Defaults to 30 seconds.
        /// </summary>
        [JsonProperty("terminationGracePeriodSeconds")]
        public int? TerminationGracePeriodSeconds { get; set; }

        /// <summary>
        ///     If specified, the pod's tolerations.
        /// </summary>
        [JsonProperty("tolerations", NullValueHandling = NullValueHandling.Ignore)]
        public List<TolerationV1> Tolerations { get; set; } = new List<TolerationV1>();

        /// <summary>
        ///     List of volumes that can be mounted by containers belonging to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes
        /// </summary>
        [JsonProperty("volumes", NullValueHandling = NullValueHandling.Ignore)]
        public List<VolumeV1> Volumes { get; set; } = new List<VolumeV1>();

        /// <summary>
        ///     SecurityContext holds pod-level security attributes and common container settings. Optional: Defaults to empty.  See type description for default values of each field.
        /// </summary>
        [JsonProperty("securityContext")]
        public PodSecurityContextV1 SecurityContext { get; set; }

        /// <summary>
        ///     DeprecatedServiceAccount is a depreciated alias for ServiceAccountName. Deprecated: Use serviceAccountName instead.
        /// </summary>
        [JsonProperty("serviceAccount")]
        public string ServiceAccount { get; set; }

        /// <summary>
        ///     If specified, the pod's scheduling constraints
        /// </summary>
        [JsonProperty("affinity")]
        public AffinityV1 Affinity { get; set; }

        /// <summary>
        ///     Set DNS policy for containers within the pod. One of 'ClusterFirstWithHostNet', 'ClusterFirst' or 'Default'. Defaults to "ClusterFirst". To have DNS options set along with hostNetwork, you have to specify DNS policy explicitly to 'ClusterFirstWithHostNet'.
        /// </summary>
        [JsonProperty("dnsPolicy")]
        public string DnsPolicy { get; set; }

        /// <summary>
        ///     The priority value. Various system components use this field to find the priority of the pod. When Priority Admission Controller is enabled, it prevents users from setting this field. The admission controller populates this field from PriorityClassName. The higher the value, the higher the priority.
        /// </summary>
        [JsonProperty("priority")]
        public int Priority { get; set; }

        /// <summary>
        ///     Restart policy for all containers within the pod. One of Always, OnFailure, Never. Default to Always. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle/#restart-policy
        /// </summary>
        [JsonProperty("restartPolicy")]
        public string RestartPolicy { get; set; }
    }
}
