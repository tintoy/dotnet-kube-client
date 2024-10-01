using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecurityContext holds security configuration that will be applied to a container. Some fields are present in both SecurityContext and PodSecurityContext.  When both are set, the values in SecurityContext take precedence.
    /// </summary>
    public partial class SecurityContextV1
    {
        /// <summary>
        ///     Run container in privileged mode. Processes in privileged containers are essentially equivalent to root on the host. Defaults to false. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "privileged")]
        [JsonProperty("privileged", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Privileged { get; set; }

        /// <summary>
        ///     appArmorProfile is the AppArmor options to use by this container. If set, this profile overrides the pod's appArmorProfile. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "appArmorProfile")]
        [JsonProperty("appArmorProfile", NullValueHandling = NullValueHandling.Ignore)]
        public AppArmorProfileV1 AppArmorProfile { get; set; }

        /// <summary>
        ///     The seccomp options to use by this container. If seccomp options are provided at both the pod &amp; container level, the container options override the pod options. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "seccompProfile")]
        [JsonProperty("seccompProfile", NullValueHandling = NullValueHandling.Ignore)]
        public SeccompProfileV1 SeccompProfile { get; set; }

        /// <summary>
        ///     Whether this container has a read-only root filesystem. Default is false. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "readOnlyRootFilesystem")]
        [JsonProperty("readOnlyRootFilesystem", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnlyRootFilesystem { get; set; }

        /// <summary>
        ///     AllowPrivilegeEscalation controls whether a process can gain more privileges than its parent process. This bool directly controls if the no_new_privs flag will be set on the container process. AllowPrivilegeEscalation is true always when the container is: 1) run as Privileged 2) has CAP_SYS_ADMIN Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "allowPrivilegeEscalation")]
        [JsonProperty("allowPrivilegeEscalation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowPrivilegeEscalation { get; set; }

        /// <summary>
        ///     The GID to run the entrypoint of the container process. Uses runtime default if unset. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "runAsGroup")]
        [JsonProperty("runAsGroup", NullValueHandling = NullValueHandling.Ignore)]
        public long? RunAsGroup { get; set; }

        /// <summary>
        ///     The UID to run the entrypoint of the container process. Defaults to user specified in image metadata if unspecified. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "runAsUser")]
        [JsonProperty("runAsUser", NullValueHandling = NullValueHandling.Ignore)]
        public long? RunAsUser { get; set; }

        /// <summary>
        ///     The capabilities to add/drop when running containers. Defaults to the default set of capabilities granted by the container runtime. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "capabilities")]
        [JsonProperty("capabilities", NullValueHandling = NullValueHandling.Ignore)]
        public CapabilitiesV1 Capabilities { get; set; }

        /// <summary>
        ///     The SELinux context to be applied to the container. If unspecified, the container runtime will allocate a random SELinux context for each container.  May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "seLinuxOptions")]
        [JsonProperty("seLinuxOptions", NullValueHandling = NullValueHandling.Ignore)]
        public SELinuxOptionsV1 SeLinuxOptions { get; set; }

        /// <summary>
        ///     The Windows specific settings applied to all containers. If unspecified, the options from the PodSecurityContext will be used. If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence. Note that this field cannot be set when spec.os.name is linux.
        /// </summary>
        [YamlMember(Alias = "windowsOptions")]
        [JsonProperty("windowsOptions", NullValueHandling = NullValueHandling.Ignore)]
        public WindowsSecurityContextOptionsV1 WindowsOptions { get; set; }

        /// <summary>
        ///     procMount denotes the type of proc mount to use for the containers. The default value is Default which uses the container runtime defaults for readonly paths and masked paths. This requires the ProcMountType feature flag to be enabled. Note that this field cannot be set when spec.os.name is windows.
        /// </summary>
        [YamlMember(Alias = "procMount")]
        [JsonProperty("procMount", NullValueHandling = NullValueHandling.Ignore)]
        public string ProcMount { get; set; }

        /// <summary>
        ///     Indicates that the container must run as a non-root user. If true, the Kubelet will validate the image at runtime to ensure that it does not run as UID 0 (root) and fail to start the container if it does. If unset or false, no such validation will be performed. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [YamlMember(Alias = "runAsNonRoot")]
        [JsonProperty("runAsNonRoot", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RunAsNonRoot { get; set; }
    }
}
