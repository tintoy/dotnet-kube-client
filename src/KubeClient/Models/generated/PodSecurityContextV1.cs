using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSecurityContext holds pod-level security attributes and common container settings. Some fields are also present in container.securityContext.  Field values of container.securityContext take precedence over field values of PodSecurityContext.
    /// </summary>
    public partial class PodSecurityContextV1
    {
        /// <summary>
        ///     appArmorProfile is the AppArmor options to use by the containers in this pod. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "appArmorProfile")]
        [JsonProperty("appArmorProfile", NullValueHandling = NullValueHandling.Ignore)]
        public AppArmorProfileV1 AppArmorProfile { get; set; }

        /// <summary>
        ///     The seccomp options to use by the containers in this pod. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "seccompProfile")]
        [JsonProperty("seccompProfile", NullValueHandling = NullValueHandling.Ignore)]
        public SeccompProfileV1 SeccompProfile { get; set; }

        /// <summary>
        ///     A special supplemental group that applies to all containers in a pod. Some volume types allow the Kubelet to change the ownership of that volume to be owned by the pod:
        ///     
        ///     1. The owning GID will be the FSGroup 2. The setgid bit is set (new files created in the volume will be owned by FSGroup) 3. The permission bits are OR'd with rw-rw----
        ///     
        ///     If unset, the Kubelet will not modify the ownership and permissions of any volume. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "fsGroup")]
        [JsonProperty("fsGroup", NullValueHandling = NullValueHandling.Ignore)]
        public long? FsGroup { get; set; }

        /// <summary>
        ///     The GID to run the entrypoint of the container process. Uses runtime default if unset. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence for that container. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "runAsGroup")]
        [JsonProperty("runAsGroup", NullValueHandling = NullValueHandling.Ignore)]
        public long? RunAsGroup { get; set; }

        /// <summary>
        ///     The UID to run the entrypoint of the container process. Defaults to user specified in image metadata if unspecified. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence for that container. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "runAsUser")]
        [JsonProperty("runAsUser", NullValueHandling = NullValueHandling.Ignore)]
        public long? RunAsUser { get; set; }

        /// <summary>
        ///     The SELinux context to be applied to all containers. If unspecified, the container runtime will allocate a random SELinux context for each container.  May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence for that container. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "seLinuxOptions")]
        [JsonProperty("seLinuxOptions", NullValueHandling = NullValueHandling.Ignore)]
        public SELinuxOptionsV1 SeLinuxOptions { get; set; }

        /// <summary>
        ///     A list of groups applied to the first process run in each container, in addition to the container's primary GID and fsGroup (if specified).  If the SupplementalGroupsPolicy feature is enabled, the supplementalGroupsPolicy field determines whether these are in addition to or instead of any group memberships defined in the container image. If unspecified, no additional groups are added, though group memberships defined in the container image may still be used, depending on the supplementalGroupsPolicy field. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "supplementalGroups")]
        [JsonProperty("supplementalGroups", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<long> SupplementalGroups { get; } = new List<long>();

        /// <summary>
        ///     Determine whether the <see cref="SupplementalGroups"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSupplementalGroups() => SupplementalGroups.Count > 0;

        /// <summary>
        ///     Sysctls hold a list of namespaced sysctls used for the pod. Pods with unsupported sysctls (by the container runtime) might fail to launch. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "sysctls")]
        [JsonProperty("sysctls", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<SysctlV1> Sysctls { get; } = new List<SysctlV1>();

        /// <summary>
        ///     Determine whether the <see cref="Sysctls"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSysctls() => Sysctls.Count > 0;

        /// <summary>
        ///     The Windows specific settings applied to all containers. If unspecified, the options within a container's SecurityContext will be used. If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence. Note that this field cannot be set when spec.os.name is linux.
        /// </summary>
        [YamlMember(Alias = "windowsOptions")]
        [JsonProperty("windowsOptions", NullValueHandling = NullValueHandling.Ignore)]
        public WindowsSecurityContextOptionsV1 WindowsOptions { get; set; }

        /// <summary>
        ///     Indicates that the container must run as a non-root user. If true, the Kubelet will validate the image at runtime to ensure that it does not run as UID 0 (root) and fail to start the container if it does. If unset or false, no such validation will be performed. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [YamlMember(Alias = "runAsNonRoot")]
        [JsonProperty("runAsNonRoot", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RunAsNonRoot { get; set; }

        /// <summary>
        ///     fsGroupChangePolicy defines behavior of changing ownership and permission of the volume before being exposed inside Pod. This field will only apply to volume types which support fsGroup based ownership(and permissions). It will have no effect on ephemeral volume types such as: secret, configmaps and emptydir. Valid values are "OnRootMismatch" and "Always". If not specified, "Always" is used. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "fsGroupChangePolicy")]
        [JsonProperty("fsGroupChangePolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string FsGroupChangePolicy { get; set; }

        /// <summary>
        ///     Defines how supplemental groups of the first container processes are calculated. Valid values are "Merge" and "Strict". If not specified, "Merge" is used. (Alpha) Using the field requires the SupplementalGroupsPolicy feature gate to be enabled and the container runtime must implement support for this feature. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "supplementalGroupsPolicy")]
        [JsonProperty("supplementalGroupsPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string SupplementalGroupsPolicy { get; set; }
    }
}
