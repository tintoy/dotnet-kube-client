using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSpec is a description of a pod.
    /// </summary>
    public partial class PodSpecV1
    {
        /// <summary>
        ///     Use the host's ipc namespace. Optional: Default to false.
        /// </summary>
        [YamlMember(Alias = "hostIPC")]
        [JsonProperty("hostIPC", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostIPC { get; set; }

        /// <summary>
        ///     Use the host's pid namespace. Optional: Default to false.
        /// </summary>
        [YamlMember(Alias = "hostPID")]
        [JsonProperty("hostPID", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostPID { get; set; }

        /// <summary>
        ///     Specifies the hostname of the Pod If not specified, the pod's hostname will be set to a system-defined value.
        /// </summary>
        [YamlMember(Alias = "hostname")]
        [JsonProperty("hostname", NullValueHandling = NullValueHandling.Ignore)]
        public string Hostname { get; set; }

        /// <summary>
        ///     NodeName is a request to schedule this pod onto a specific node. If it is non-empty, the scheduler simply schedules this pod onto that node, assuming that it fits resource requirements.
        /// </summary>
        [YamlMember(Alias = "nodeName")]
        [JsonProperty("nodeName", NullValueHandling = NullValueHandling.Ignore)]
        public string NodeName { get; set; }

        /// <summary>
        ///     If specified, indicates the pod's priority. "system-node-critical" and "system-cluster-critical" are two special keywords which indicate the highest priorities with the former being the highest priority. Any other name must be defined by creating a PriorityClass object with that name. If not specified, the pod priority will be default or zero if there is no default.
        /// </summary>
        [YamlMember(Alias = "priorityClassName")]
        [JsonProperty("priorityClassName", NullValueHandling = NullValueHandling.Ignore)]
        public string PriorityClassName { get; set; }

        /// <summary>
        ///     If specified, the pod will be dispatched by specified scheduler. If not specified, the pod will be dispatched by default scheduler.
        /// </summary>
        [YamlMember(Alias = "schedulerName")]
        [JsonProperty("schedulerName", NullValueHandling = NullValueHandling.Ignore)]
        public string SchedulerName { get; set; }

        /// <summary>
        ///     ServiceAccountName is the name of the ServiceAccount to use to run this pod. More info: https://kubernetes.io/docs/tasks/configure-pod-container/configure-service-account/
        /// </summary>
        [YamlMember(Alias = "serviceAccountName")]
        [JsonProperty("serviceAccountName", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceAccountName { get; set; }

        /// <summary>
        ///     Share a single process namespace between all of the containers in a pod. When this is set containers will be able to view and signal processes from other containers in the same pod, and the first process in each container will not be assigned PID 1. HostPID and ShareProcessNamespace cannot both be set. Optional: Default to false. This field is alpha-level and is honored only by servers that enable the PodShareProcessNamespace feature.
        /// </summary>
        [YamlMember(Alias = "shareProcessNamespace")]
        [JsonProperty("shareProcessNamespace", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShareProcessNamespace { get; set; }

        /// <summary>
        ///     Specifies the DNS parameters of a pod. Parameters specified here will be merged to the generated DNS configuration based on DNSPolicy.
        /// </summary>
        [YamlMember(Alias = "dnsConfig")]
        [JsonProperty("dnsConfig", NullValueHandling = NullValueHandling.Ignore)]
        public PodDNSConfigV1 DnsConfig { get; set; }

        /// <summary>
        ///     Host networking requested for this pod. Use the host's network namespace. If this option is set, the ports that will be used must be specified. Default to false.
        /// </summary>
        [YamlMember(Alias = "hostNetwork")]
        [JsonProperty("hostNetwork", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostNetwork { get; set; }

        /// <summary>
        ///     AutomountServiceAccountToken indicates whether a service account token should be automatically mounted.
        /// </summary>
        [YamlMember(Alias = "automountServiceAccountToken")]
        [JsonProperty("automountServiceAccountToken", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AutomountServiceAccountToken { get; set; }

        /// <summary>
        ///     If specified, the fully qualified Pod hostname will be "&lt;hostname&gt;.&lt;subdomain&gt;.&lt;pod namespace&gt;.svc.&lt;cluster domain&gt;". If not specified, the pod will not have a domainname at all.
        /// </summary>
        [YamlMember(Alias = "subdomain")]
        [JsonProperty("subdomain", NullValueHandling = NullValueHandling.Ignore)]
        public string Subdomain { get; set; }

        /// <summary>
        ///     NodeSelector is a selector which must be true for the pod to fit on a node. Selector which must match a node's labels for the pod to be scheduled on that node. More info: https://kubernetes.io/docs/concepts/configuration/assign-pod-node/
        /// </summary>
        [YamlMember(Alias = "nodeSelector")]
        [JsonProperty("nodeSelector", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> NodeSelector { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="NodeSelector"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNodeSelector() => NodeSelector.Count > 0;

        /// <summary>
        ///     Optional duration in seconds the pod may be active on the node relative to StartTime before the system will actively try to mark it failed and kill associated containers. Value must be a positive integer.
        /// </summary>
        [YamlMember(Alias = "activeDeadlineSeconds")]
        [JsonProperty("activeDeadlineSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? ActiveDeadlineSeconds { get; set; }

        /// <summary>
        ///     List of containers belonging to the pod. Containers cannot currently be added or removed. There must be at least one container in a Pod. Cannot be updated.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "containers")]
        [JsonProperty("containers", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerV1> Containers { get; } = new List<ContainerV1>();

        /// <summary>
        ///     HostAliases is an optional list of hosts and IPs that will be injected into the pod's hosts file if specified. This is only valid for non-hostNetwork pods.
        /// </summary>
        [MergeStrategy(Key = "ip")]
        [YamlMember(Alias = "hostAliases")]
        [JsonProperty("hostAliases", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<HostAliasV1> HostAliases { get; } = new List<HostAliasV1>();

        /// <summary>
        ///     Determine whether the <see cref="HostAliases"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeHostAliases() => HostAliases.Count > 0;

        /// <summary>
        ///     ImagePullSecrets is an optional list of references to secrets in the same namespace to use for pulling any of the images used by this PodSpec. If specified, these secrets will be passed to individual puller implementations for them to use. For example, in the case of docker, only DockerConfig type secrets are honored. More info: https://kubernetes.io/docs/concepts/containers/images#specifying-imagepullsecrets-on-a-pod
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "imagePullSecrets")]
        [JsonProperty("imagePullSecrets", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<LocalObjectReferenceV1> ImagePullSecrets { get; } = new List<LocalObjectReferenceV1>();

        /// <summary>
        ///     Determine whether the <see cref="ImagePullSecrets"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeImagePullSecrets() => ImagePullSecrets.Count > 0;

        /// <summary>
        ///     List of initialization containers belonging to the pod. Init containers are executed in order prior to containers being started. If any init container fails, the pod is considered to have failed and is handled according to its restartPolicy. The name for an init container or normal container must be unique among all containers. Init containers may not have Lifecycle actions, Readiness probes, or Liveness probes. The resourceRequirements of an init container are taken into account during scheduling by finding the highest request/limit for each resource type, and then using the max of of that value or the sum of the normal containers. Limits are applied to init containers in a similar fashion. Init containers cannot currently be added or removed. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/init-containers/
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "initContainers")]
        [JsonProperty("initContainers", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerV1> InitContainers { get; } = new List<ContainerV1>();

        /// <summary>
        ///     Determine whether the <see cref="InitContainers"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeInitContainers() => InitContainers.Count > 0;

        /// <summary>
        ///     If specified, all readiness gates will be evaluated for pod readiness. A pod is ready when all its containers are ready AND all conditions specified in the readiness gates have status equal to "True" More info: https://github.com/kubernetes/community/blob/master/keps/sig-network/0007-pod-ready%2B%2B.md
        /// </summary>
        [YamlMember(Alias = "readinessGates")]
        [JsonProperty("readinessGates", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodReadinessGateV1> ReadinessGates { get; } = new List<PodReadinessGateV1>();

        /// <summary>
        ///     Determine whether the <see cref="ReadinessGates"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeReadinessGates() => ReadinessGates.Count > 0;

        /// <summary>
        ///     Optional duration in seconds the pod needs to terminate gracefully. May be decreased in delete request. Value must be non-negative integer. The value zero indicates delete immediately. If this value is nil, the default grace period will be used instead. The grace period is the duration in seconds after the processes running in the pod are sent a termination signal and the time when the processes are forcibly halted with a kill signal. Set this value longer than the expected cleanup time for your process. Defaults to 30 seconds.
        /// </summary>
        [YamlMember(Alias = "terminationGracePeriodSeconds")]
        [JsonProperty("terminationGracePeriodSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? TerminationGracePeriodSeconds { get; set; }

        /// <summary>
        ///     If specified, the pod's tolerations.
        /// </summary>
        [YamlMember(Alias = "tolerations")]
        [JsonProperty("tolerations", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<TolerationV1> Tolerations { get; } = new List<TolerationV1>();

        /// <summary>
        ///     Determine whether the <see cref="Tolerations"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTolerations() => Tolerations.Count > 0;

        /// <summary>
        ///     List of volumes that can be mounted by containers belonging to the pod. More info: https://kubernetes.io/docs/concepts/storage/volumes
        /// </summary>
        [RetainKeysStrategy]
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "volumes")]
        [JsonProperty("volumes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<VolumeV1> Volumes { get; } = new List<VolumeV1>();

        /// <summary>
        ///     Determine whether the <see cref="Volumes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumes() => Volumes.Count > 0;

        /// <summary>
        ///     SecurityContext holds pod-level security attributes and common container settings. Optional: Defaults to empty.  See type description for default values of each field.
        /// </summary>
        [YamlMember(Alias = "securityContext")]
        [JsonProperty("securityContext", NullValueHandling = NullValueHandling.Ignore)]
        public PodSecurityContextV1 SecurityContext { get; set; }

        /// <summary>
        ///     DeprecatedServiceAccount is a depreciated alias for ServiceAccountName. Deprecated: Use serviceAccountName instead.
        /// </summary>
        [YamlMember(Alias = "serviceAccount")]
        [JsonProperty("serviceAccount", NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceAccount { get; set; }

        /// <summary>
        ///     If specified, the pod's scheduling constraints
        /// </summary>
        [YamlMember(Alias = "affinity")]
        [JsonProperty("affinity", NullValueHandling = NullValueHandling.Ignore)]
        public AffinityV1 Affinity { get; set; }

        /// <summary>
        ///     Set DNS policy for the pod. Defaults to "ClusterFirst". Valid values are 'ClusterFirstWithHostNet', 'ClusterFirst', 'Default' or 'None'. DNS parameters given in DNSConfig will be merged with the policy selected with DNSPolicy. To have DNS options set along with hostNetwork, you have to specify DNS policy explicitly to 'ClusterFirstWithHostNet'.
        /// </summary>
        [YamlMember(Alias = "dnsPolicy")]
        [JsonProperty("dnsPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string DnsPolicy { get; set; }

        /// <summary>
        ///     The priority value. Various system components use this field to find the priority of the pod. When Priority Admission Controller is enabled, it prevents users from setting this field. The admission controller populates this field from PriorityClassName. The higher the value, the higher the priority.
        /// </summary>
        [YamlMember(Alias = "priority")]
        [JsonProperty("priority", NullValueHandling = NullValueHandling.Ignore)]
        public int? Priority { get; set; }

        /// <summary>
        ///     Restart policy for all containers within the pod. One of Always, OnFailure, Never. Default to Always. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle/#restart-policy
        /// </summary>
        [YamlMember(Alias = "restartPolicy")]
        [JsonProperty("restartPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string RestartPolicy { get; set; }
    }
}
