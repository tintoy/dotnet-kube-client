using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSecurityPolicySpec defines the policy enforced. Deprecated: use PodSecurityPolicySpec from policy API Group instead.
    /// </summary>
    public partial class PodSecurityPolicySpecV1Beta1
    {
        /// <summary>
        ///     hostIPC determines if the policy allows the use of HostIPC in the pod spec.
        /// </summary>
        [YamlMember(Alias = "hostIPC")]
        [JsonProperty("hostIPC", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostIPC { get; set; }

        /// <summary>
        ///     hostPID determines if the policy allows the use of HostPID in the pod spec.
        /// </summary>
        [YamlMember(Alias = "hostPID")]
        [JsonProperty("hostPID", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostPID { get; set; }

        /// <summary>
        ///     privileged determines if a pod can request to be run as privileged.
        /// </summary>
        [YamlMember(Alias = "privileged")]
        [JsonProperty("privileged", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Privileged { get; set; }

        /// <summary>
        ///     hostNetwork determines if the policy allows the use of HostNetwork in the pod spec.
        /// </summary>
        [YamlMember(Alias = "hostNetwork")]
        [JsonProperty("hostNetwork", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostNetwork { get; set; }

        /// <summary>
        ///     readOnlyRootFilesystem when set to true will force containers to run with a read only root file system.  If the container specifically requests to run with a non-read only root file system the PSP should deny the pod. If set to false the container may run with a read only root file system if it wishes but it will not be forced to.
        /// </summary>
        [YamlMember(Alias = "readOnlyRootFilesystem")]
        [JsonProperty("readOnlyRootFilesystem", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnlyRootFilesystem { get; set; }

        /// <summary>
        ///     allowPrivilegeEscalation determines if a pod can request to allow privilege escalation. If unspecified, defaults to true.
        /// </summary>
        [YamlMember(Alias = "allowPrivilegeEscalation")]
        [JsonProperty("allowPrivilegeEscalation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowPrivilegeEscalation { get; set; }

        /// <summary>
        ///     defaultAllowPrivilegeEscalation controls the default setting for whether a process can gain more privileges than its parent process.
        /// </summary>
        [YamlMember(Alias = "defaultAllowPrivilegeEscalation")]
        [JsonProperty("defaultAllowPrivilegeEscalation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DefaultAllowPrivilegeEscalation { get; set; }

        /// <summary>
        ///     fsGroup is the strategy that will dictate what fs group is used by the SecurityContext.
        /// </summary>
        [YamlMember(Alias = "fsGroup")]
        [JsonProperty("fsGroup", NullValueHandling = NullValueHandling.Include)]
        public FSGroupStrategyOptionsV1Beta1 FsGroup { get; set; }

        /// <summary>
        ///     runAsUser is the strategy that will dictate the allowable RunAsUser values that may be set.
        /// </summary>
        [YamlMember(Alias = "runAsUser")]
        [JsonProperty("runAsUser", NullValueHandling = NullValueHandling.Include)]
        public RunAsUserStrategyOptionsV1Beta1 RunAsUser { get; set; }

        /// <summary>
        ///     allowedCapabilities is a list of capabilities that can be requested to add to the container. Capabilities in this field may be added at the pod author's discretion. You must not list a capability in both allowedCapabilities and requiredDropCapabilities.
        /// </summary>
        [YamlMember(Alias = "allowedCapabilities")]
        [JsonProperty("allowedCapabilities", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> AllowedCapabilities { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="AllowedCapabilities"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllowedCapabilities() => AllowedCapabilities.Count > 0;

        /// <summary>
        ///     allowedFlexVolumes is a whitelist of allowed Flexvolumes.  Empty or nil indicates that all Flexvolumes may be used.  This parameter is effective only when the usage of the Flexvolumes is allowed in the "volumes" field.
        /// </summary>
        [YamlMember(Alias = "allowedFlexVolumes")]
        [JsonProperty("allowedFlexVolumes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<AllowedFlexVolumeV1Beta1> AllowedFlexVolumes { get; } = new List<AllowedFlexVolumeV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="AllowedFlexVolumes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllowedFlexVolumes() => AllowedFlexVolumes.Count > 0;

        /// <summary>
        ///     allowedHostPaths is a white list of allowed host paths. Empty indicates that all host paths may be used.
        /// </summary>
        [YamlMember(Alias = "allowedHostPaths")]
        [JsonProperty("allowedHostPaths", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<AllowedHostPathV1Beta1> AllowedHostPaths { get; } = new List<AllowedHostPathV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="AllowedHostPaths"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllowedHostPaths() => AllowedHostPaths.Count > 0;

        /// <summary>
        ///     allowedUnsafeSysctls is a list of explicitly allowed unsafe sysctls, defaults to none. Each entry is either a plain sysctl name or ends in "*" in which case it is considered as a prefix of allowed sysctls. Single * means all unsafe sysctls are allowed. Kubelet has to whitelist all allowed unsafe sysctls explicitly to avoid rejection.
        ///     
        ///     Examples: e.g. "foo/*" allows "foo/bar", "foo/baz", etc. e.g. "foo.*" allows "foo.bar", "foo.baz", etc.
        /// </summary>
        [YamlMember(Alias = "allowedUnsafeSysctls")]
        [JsonProperty("allowedUnsafeSysctls", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> AllowedUnsafeSysctls { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="AllowedUnsafeSysctls"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllowedUnsafeSysctls() => AllowedUnsafeSysctls.Count > 0;

        /// <summary>
        ///     defaultAddCapabilities is the default set of capabilities that will be added to the container unless the pod spec specifically drops the capability.  You may not list a capability in both defaultAddCapabilities and requiredDropCapabilities. Capabilities added here are implicitly allowed, and need not be included in the allowedCapabilities list.
        /// </summary>
        [YamlMember(Alias = "defaultAddCapabilities")]
        [JsonProperty("defaultAddCapabilities", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> DefaultAddCapabilities { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="DefaultAddCapabilities"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDefaultAddCapabilities() => DefaultAddCapabilities.Count > 0;

        /// <summary>
        ///     forbiddenSysctls is a list of explicitly forbidden sysctls, defaults to none. Each entry is either a plain sysctl name or ends in "*" in which case it is considered as a prefix of forbidden sysctls. Single * means all sysctls are forbidden.
        ///     
        ///     Examples: e.g. "foo/*" forbids "foo/bar", "foo/baz", etc. e.g. "foo.*" forbids "foo.bar", "foo.baz", etc.
        /// </summary>
        [YamlMember(Alias = "forbiddenSysctls")]
        [JsonProperty("forbiddenSysctls", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ForbiddenSysctls { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="ForbiddenSysctls"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeForbiddenSysctls() => ForbiddenSysctls.Count > 0;

        /// <summary>
        ///     hostPorts determines which host port ranges are allowed to be exposed.
        /// </summary>
        [YamlMember(Alias = "hostPorts")]
        [JsonProperty("hostPorts", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<HostPortRangeV1Beta1> HostPorts { get; } = new List<HostPortRangeV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="HostPorts"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeHostPorts() => HostPorts.Count > 0;

        /// <summary>
        ///     requiredDropCapabilities are the capabilities that will be dropped from the container.  These are required to be dropped and cannot be added.
        /// </summary>
        [YamlMember(Alias = "requiredDropCapabilities")]
        [JsonProperty("requiredDropCapabilities", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> RequiredDropCapabilities { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="RequiredDropCapabilities"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRequiredDropCapabilities() => RequiredDropCapabilities.Count > 0;

        /// <summary>
        ///     supplementalGroups is the strategy that will dictate what supplemental groups are used by the SecurityContext.
        /// </summary>
        [YamlMember(Alias = "supplementalGroups")]
        [JsonProperty("supplementalGroups", NullValueHandling = NullValueHandling.Include)]
        public SupplementalGroupsStrategyOptionsV1Beta1 SupplementalGroups { get; set; }

        /// <summary>
        ///     volumes is a white list of allowed volume plugins. Empty indicates that no volumes may be used. To allow all volumes you may use '*'.
        /// </summary>
        [YamlMember(Alias = "volumes")]
        [JsonProperty("volumes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Volumes { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Volumes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumes() => Volumes.Count > 0;

        /// <summary>
        ///     seLinux is the strategy that will dictate the allowable labels that may be set.
        /// </summary>
        [YamlMember(Alias = "seLinux")]
        [JsonProperty("seLinux", NullValueHandling = NullValueHandling.Include)]
        public SELinuxStrategyOptionsV1Beta1 SeLinux { get; set; }
    }
}
