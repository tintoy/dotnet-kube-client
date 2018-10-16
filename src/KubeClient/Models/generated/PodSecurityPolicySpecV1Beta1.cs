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
        [JsonProperty("hostIPC")]
        [YamlMember(Alias = "hostIPC")]
        public bool HostIPC { get; set; }

        /// <summary>
        ///     hostPID determines if the policy allows the use of HostPID in the pod spec.
        /// </summary>
        [JsonProperty("hostPID")]
        [YamlMember(Alias = "hostPID")]
        public bool HostPID { get; set; }

        /// <summary>
        ///     privileged determines if a pod can request to be run as privileged.
        /// </summary>
        [JsonProperty("privileged")]
        [YamlMember(Alias = "privileged")]
        public bool Privileged { get; set; }

        /// <summary>
        ///     hostNetwork determines if the policy allows the use of HostNetwork in the pod spec.
        /// </summary>
        [JsonProperty("hostNetwork")]
        [YamlMember(Alias = "hostNetwork")]
        public bool HostNetwork { get; set; }

        /// <summary>
        ///     readOnlyRootFilesystem when set to true will force containers to run with a read only root file system.  If the container specifically requests to run with a non-read only root file system the PSP should deny the pod. If set to false the container may run with a read only root file system if it wishes but it will not be forced to.
        /// </summary>
        [JsonProperty("readOnlyRootFilesystem")]
        [YamlMember(Alias = "readOnlyRootFilesystem")]
        public bool ReadOnlyRootFilesystem { get; set; }

        /// <summary>
        ///     allowPrivilegeEscalation determines if a pod can request to allow privilege escalation. If unspecified, defaults to true.
        /// </summary>
        [JsonProperty("allowPrivilegeEscalation")]
        [YamlMember(Alias = "allowPrivilegeEscalation")]
        public bool AllowPrivilegeEscalation { get; set; }

        /// <summary>
        ///     defaultAllowPrivilegeEscalation controls the default setting for whether a process can gain more privileges than its parent process.
        /// </summary>
        [JsonProperty("defaultAllowPrivilegeEscalation")]
        [YamlMember(Alias = "defaultAllowPrivilegeEscalation")]
        public bool DefaultAllowPrivilegeEscalation { get; set; }

        /// <summary>
        ///     fsGroup is the strategy that will dictate what fs group is used by the SecurityContext.
        /// </summary>
        [JsonProperty("fsGroup")]
        [YamlMember(Alias = "fsGroup")]
        public FSGroupStrategyOptionsV1Beta1 FsGroup { get; set; }

        /// <summary>
        ///     runAsUser is the strategy that will dictate the allowable RunAsUser values that may be set.
        /// </summary>
        [JsonProperty("runAsUser")]
        [YamlMember(Alias = "runAsUser")]
        public RunAsUserStrategyOptionsV1Beta1 RunAsUser { get; set; }

        /// <summary>
        ///     allowedCapabilities is a list of capabilities that can be requested to add to the container. Capabilities in this field may be added at the pod author's discretion. You must not list a capability in both allowedCapabilities and requiredDropCapabilities.
        /// </summary>
        [YamlMember(Alias = "allowedCapabilities")]
        [JsonProperty("allowedCapabilities", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AllowedCapabilities { get; set; } = new List<string>();

        /// <summary>
        ///     allowedFlexVolumes is a whitelist of allowed Flexvolumes.  Empty or nil indicates that all Flexvolumes may be used.  This parameter is effective only when the usage of the Flexvolumes is allowed in the "volumes" field.
        /// </summary>
        [YamlMember(Alias = "allowedFlexVolumes")]
        [JsonProperty("allowedFlexVolumes", NullValueHandling = NullValueHandling.Ignore)]
        public List<AllowedFlexVolumeV1Beta1> AllowedFlexVolumes { get; set; } = new List<AllowedFlexVolumeV1Beta1>();

        /// <summary>
        ///     allowedHostPaths is a white list of allowed host paths. Empty indicates that all host paths may be used.
        /// </summary>
        [YamlMember(Alias = "allowedHostPaths")]
        [JsonProperty("allowedHostPaths", NullValueHandling = NullValueHandling.Ignore)]
        public List<AllowedHostPathV1Beta1> AllowedHostPaths { get; set; } = new List<AllowedHostPathV1Beta1>();

        /// <summary>
        ///     allowedUnsafeSysctls is a list of explicitly allowed unsafe sysctls, defaults to none. Each entry is either a plain sysctl name or ends in "*" in which case it is considered as a prefix of allowed sysctls. Single * means all unsafe sysctls are allowed. Kubelet has to whitelist all allowed unsafe sysctls explicitly to avoid rejection.
        ///     
        ///     Examples: e.g. "foo/*" allows "foo/bar", "foo/baz", etc. e.g. "foo.*" allows "foo.bar", "foo.baz", etc.
        /// </summary>
        [YamlMember(Alias = "allowedUnsafeSysctls")]
        [JsonProperty("allowedUnsafeSysctls", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AllowedUnsafeSysctls { get; set; } = new List<string>();

        /// <summary>
        ///     defaultAddCapabilities is the default set of capabilities that will be added to the container unless the pod spec specifically drops the capability.  You may not list a capability in both defaultAddCapabilities and requiredDropCapabilities. Capabilities added here are implicitly allowed, and need not be included in the allowedCapabilities list.
        /// </summary>
        [YamlMember(Alias = "defaultAddCapabilities")]
        [JsonProperty("defaultAddCapabilities", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> DefaultAddCapabilities { get; set; } = new List<string>();

        /// <summary>
        ///     forbiddenSysctls is a list of explicitly forbidden sysctls, defaults to none. Each entry is either a plain sysctl name or ends in "*" in which case it is considered as a prefix of forbidden sysctls. Single * means all sysctls are forbidden.
        ///     
        ///     Examples: e.g. "foo/*" forbids "foo/bar", "foo/baz", etc. e.g. "foo.*" forbids "foo.bar", "foo.baz", etc.
        /// </summary>
        [YamlMember(Alias = "forbiddenSysctls")]
        [JsonProperty("forbiddenSysctls", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ForbiddenSysctls { get; set; } = new List<string>();

        /// <summary>
        ///     hostPorts determines which host port ranges are allowed to be exposed.
        /// </summary>
        [YamlMember(Alias = "hostPorts")]
        [JsonProperty("hostPorts", NullValueHandling = NullValueHandling.Ignore)]
        public List<HostPortRangeV1Beta1> HostPorts { get; set; } = new List<HostPortRangeV1Beta1>();

        /// <summary>
        ///     requiredDropCapabilities are the capabilities that will be dropped from the container.  These are required to be dropped and cannot be added.
        /// </summary>
        [YamlMember(Alias = "requiredDropCapabilities")]
        [JsonProperty("requiredDropCapabilities", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RequiredDropCapabilities { get; set; } = new List<string>();

        /// <summary>
        ///     supplementalGroups is the strategy that will dictate what supplemental groups are used by the SecurityContext.
        /// </summary>
        [JsonProperty("supplementalGroups")]
        [YamlMember(Alias = "supplementalGroups")]
        public SupplementalGroupsStrategyOptionsV1Beta1 SupplementalGroups { get; set; }

        /// <summary>
        ///     volumes is a white list of allowed volume plugins. Empty indicates that no volumes may be used. To allow all volumes you may use '*'.
        /// </summary>
        [YamlMember(Alias = "volumes")]
        [JsonProperty("volumes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Volumes { get; set; } = new List<string>();

        /// <summary>
        ///     seLinux is the strategy that will dictate the allowable labels that may be set.
        /// </summary>
        [JsonProperty("seLinux")]
        [YamlMember(Alias = "seLinux")]
        public SELinuxStrategyOptionsV1Beta1 SeLinux { get; set; }
    }
}
