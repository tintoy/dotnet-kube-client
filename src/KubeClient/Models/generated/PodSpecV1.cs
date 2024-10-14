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
        ///     If true the pod's hostname will be configured as the pod's FQDN, rather than the leaf name (the default). In Linux containers, this means setting the FQDN in the hostname field of the kernel (the nodename field of struct utsname). In Windows containers, this means setting the registry value of hostname for the registry key HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters to FQDN. If a pod does not have FQDN, this has no effect. Default to false.
        /// </summary>
        [YamlMember(Alias = "setHostnameAsFQDN")]
        [JsonProperty("setHostnameAsFQDN", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SetHostnameAsFQDN { get; set; }

        /// <summary>
        ///     Overhead represents the resource overhead associated with running a pod for a given RuntimeClass. This field will be autopopulated at admission time by the RuntimeClass admission controller. If the RuntimeClass admission controller is enabled, overhead must not be set in Pod create requests. The RuntimeClass admission controller will reject Pod create requests which have the overhead already set. If RuntimeClass is configured and selected in the PodSpec, Overhead will be set to the value defined in the corresponding RuntimeClass, otherwise it will remain unset and treated as zero. More info: https://git.k8s.io/enhancements/keps/sig-node/688-pod-overhead/README.md
        /// </summary>
        [YamlMember(Alias = "overhead")]
        [JsonProperty("overhead", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Overhead { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Overhead"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeOverhead() => Overhead.Count > 0;

        /// <summary>
        ///     Specifies the hostname of the Pod If not specified, the pod's hostname will be set to a system-defined value.
        /// </summary>
        [YamlMember(Alias = "hostname")]
        [JsonProperty("hostname", NullValueHandling = NullValueHandling.Ignore)]
        public string Hostname { get; set; }

        /// <summary>
        ///     NodeName indicates in which node this pod is scheduled. If empty, this pod is a candidate for scheduling by the scheduler defined in schedulerName. Once this field is set, the kubelet for this node becomes responsible for the lifecycle of this pod. This field should not be used to express a desire for the pod to be scheduled on a specific node. https://kubernetes.io/docs/concepts/scheduling-eviction/assign-pod-node/#nodename
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
        ///     RuntimeClassName refers to a RuntimeClass object in the node.k8s.io group, which should be used to run this pod.  If no RuntimeClass resource matches the named class, the pod will not be run. If unset or empty, the "legacy" RuntimeClass will be used, which is an implicit class with an empty definition that uses the default runtime handler. More info: https://git.k8s.io/enhancements/keps/sig-node/585-runtime-class
        /// </summary>
        [YamlMember(Alias = "runtimeClassName")]
        [JsonProperty("runtimeClassName", NullValueHandling = NullValueHandling.Ignore)]
        public string RuntimeClassName { get; set; }

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
        ///     Share a single process namespace between all of the containers in a pod. When this is set containers will be able to view and signal processes from other containers in the same pod, and the first process in each container will not be assigned PID 1. HostPID and ShareProcessNamespace cannot both be set. Optional: Default to false.
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
        ///     EnableServiceLinks indicates whether information about services should be injected into pod's environment variables, matching the syntax of Docker links. Optional: Defaults to true.
        /// </summary>
        [YamlMember(Alias = "enableServiceLinks")]
        [JsonProperty("enableServiceLinks", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnableServiceLinks { get; set; }

        /// <summary>
        ///     List of ephemeral containers run in this pod. Ephemeral containers may be run in an existing pod to perform user-initiated actions such as debugging. This list cannot be specified when creating a pod, and it cannot be modified by updating the pod spec. In order to add an ephemeral container to an existing pod, use the pod's ephemeralcontainers subresource.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "ephemeralContainers")]
        [JsonProperty("ephemeralContainers", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EphemeralContainerV1> EphemeralContainers { get; } = new List<EphemeralContainerV1>();

        /// <summary>
        ///     Determine whether the <see cref="EphemeralContainers"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeEphemeralContainers() => EphemeralContainers.Count > 0;

        /// <summary>
        ///     HostAliases is an optional list of hosts and IPs that will be injected into the pod's hosts file if specified.
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
        ///     Use the host's user namespace. Optional: Default to true. If set to true or not present, the pod will be run in the host user namespace, useful for when the pod needs a feature only available to the host user namespace, such as loading a kernel module with CAP_SYS_MODULE. When set to false, a new userns is created for the pod. Setting false is useful for mitigating container breakout vulnerabilities even allowing users to run their containers as root without actually having root privileges on the host. This field is alpha-level and is only honored by servers that enable the UserNamespacesSupport feature.
        /// </summary>
        [YamlMember(Alias = "hostUsers")]
        [JsonProperty("hostUsers", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostUsers { get; set; }

        /// <summary>
        ///     ImagePullSecrets is an optional list of references to secrets in the same namespace to use for pulling any of the images used by this PodSpec. If specified, these secrets will be passed to individual puller implementations for them to use. More info: https://kubernetes.io/docs/concepts/containers/images#specifying-imagepullsecrets-on-a-pod
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
        ///     List of initialization containers belonging to the pod. Init containers are executed in order prior to containers being started. If any init container fails, the pod is considered to have failed and is handled according to its restartPolicy. The name for an init container or normal container must be unique among all containers. Init containers may not have Lifecycle actions, Readiness probes, Liveness probes, or Startup probes. The resourceRequirements of an init container are taken into account during scheduling by finding the highest request/limit for each resource type, and then using the max of of that value or the sum of the normal containers. Limits are applied to init containers in a similar fashion. Init containers cannot currently be added or removed. Cannot be updated. More info: https://kubernetes.io/docs/concepts/workloads/pods/init-containers/
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
        ///     Specifies the OS of the containers in the pod. Some pod and container fields are restricted if this is set.
        ///     
        ///     If the OS field is set to linux, the following fields must be unset: -securityContext.windowsOptions
        ///     
        ///     If the OS field is set to windows, following fields must be unset: - spec.hostPID - spec.hostIPC - spec.hostUsers - spec.securityContext.appArmorProfile - spec.securityContext.seLinuxOptions - spec.securityContext.seccompProfile - spec.securityContext.fsGroup - spec.securityContext.fsGroupChangePolicy - spec.securityContext.sysctls - spec.shareProcessNamespace - spec.securityContext.runAsUser - spec.securityContext.runAsGroup - spec.securityContext.supplementalGroups - spec.securityContext.supplementalGroupsPolicy - spec.containers[*].securityContext.appArmorProfile - spec.containers[*].securityContext.seLinuxOptions - spec.containers[*].securityContext.seccompProfile - spec.containers[*].securityContext.capabilities - spec.containers[*].securityContext.readOnlyRootFilesystem - spec.containers[*].securityContext.privileged - spec.containers[*].securityContext.allowPrivilegeEscalation - spec.containers[*].securityContext.procMount - spec.containers[*].securityContext.runAsUser - spec.containers[*].securityContext.runAsGroup
        /// </summary>
        [YamlMember(Alias = "os")]
        [JsonProperty("os", NullValueHandling = NullValueHandling.Ignore)]
        public PodOSV1 Os { get; set; }

        /// <summary>
        ///     If specified, all readiness gates will be evaluated for pod readiness. A pod is ready when all its containers are ready AND all conditions specified in the readiness gates have status equal to "True" More info: https://git.k8s.io/enhancements/keps/sig-network/580-pod-readiness-gates
        /// </summary>
        [YamlMember(Alias = "readinessGates")]
        [JsonProperty("readinessGates", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodReadinessGateV1> ReadinessGates { get; } = new List<PodReadinessGateV1>();

        /// <summary>
        ///     Determine whether the <see cref="ReadinessGates"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeReadinessGates() => ReadinessGates.Count > 0;

        /// <summary>
        ///     ResourceClaims defines which ResourceClaims must be allocated and reserved before the Pod is allowed to start. The resources will be made available to those containers which consume them by name.
        ///     
        ///     This is an alpha field and requires enabling the DynamicResourceAllocation feature gate.
        ///     
        ///     This field is immutable.
        /// </summary>
        [RetainKeysStrategy]
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "resourceClaims")]
        [JsonProperty("resourceClaims", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodResourceClaimV1> ResourceClaims { get; } = new List<PodResourceClaimV1>();

        /// <summary>
        ///     Determine whether the <see cref="ResourceClaims"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResourceClaims() => ResourceClaims.Count > 0;

        /// <summary>
        ///     SchedulingGates is an opaque list of values that if specified will block scheduling the pod. If schedulingGates is not empty, the pod will stay in the SchedulingGated state and the scheduler will not attempt to schedule the pod.
        ///     
        ///     SchedulingGates can only be set at pod creation time, and be removed only afterwards.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "schedulingGates")]
        [JsonProperty("schedulingGates", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodSchedulingGateV1> SchedulingGates { get; } = new List<PodSchedulingGateV1>();

        /// <summary>
        ///     Determine whether the <see cref="SchedulingGates"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSchedulingGates() => SchedulingGates.Count > 0;

        /// <summary>
        ///     Optional duration in seconds the pod needs to terminate gracefully. May be decreased in delete request. Value must be non-negative integer. The value zero indicates stop immediately via the kill signal (no opportunity to shut down). If this value is nil, the default grace period will be used instead. The grace period is the duration in seconds after the processes running in the pod are sent a termination signal and the time when the processes are forcibly halted with a kill signal. Set this value longer than the expected cleanup time for your process. Defaults to 30 seconds.
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
        ///     TopologySpreadConstraints describes how a group of pods ought to spread across topology domains. Scheduler will schedule pods in a way which abides by the constraints. All topologySpreadConstraints are ANDed.
        /// </summary>
        [MergeStrategy(Key = "topologyKey")]
        [YamlMember(Alias = "topologySpreadConstraints")]
        [JsonProperty("topologySpreadConstraints", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<TopologySpreadConstraintV1> TopologySpreadConstraints { get; } = new List<TopologySpreadConstraintV1>();

        /// <summary>
        ///     Determine whether the <see cref="TopologySpreadConstraints"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTopologySpreadConstraints() => TopologySpreadConstraints.Count > 0;

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
        ///     DeprecatedServiceAccount is a deprecated alias for ServiceAccountName. Deprecated: Use serviceAccountName instead.
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
        ///     PreemptionPolicy is the Policy for preempting pods with lower priority. One of Never, PreemptLowerPriority. Defaults to PreemptLowerPriority if unset.
        /// </summary>
        [YamlMember(Alias = "preemptionPolicy")]
        [JsonProperty("preemptionPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string PreemptionPolicy { get; set; }

        /// <summary>
        ///     The priority value. Various system components use this field to find the priority of the pod. When Priority Admission Controller is enabled, it prevents users from setting this field. The admission controller populates this field from PriorityClassName. The higher the value, the higher the priority.
        /// </summary>
        [YamlMember(Alias = "priority")]
        [JsonProperty("priority", NullValueHandling = NullValueHandling.Ignore)]
        public int? Priority { get; set; }

        /// <summary>
        ///     Restart policy for all containers within the pod. One of Always, OnFailure, Never. In some contexts, only a subset of those values may be permitted. Default to Always. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle/#restart-policy
        /// </summary>
        [YamlMember(Alias = "restartPolicy")]
        [JsonProperty("restartPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string RestartPolicy { get; set; }
    }
}
