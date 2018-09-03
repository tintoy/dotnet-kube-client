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
        ///     Indicates that the container must run as a non-root user. If true, the Kubelet will validate the image at runtime to ensure that it does not run as UID 0 (root) and fail to start the container if it does. If unset or false, no such validation will be performed. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [JsonProperty("runAsNonRoot")]
        [YamlMember(Alias = "runAsNonRoot")]
        public bool RunAsNonRoot { get; set; }

        /// <summary>
        ///     The SELinux context to be applied to the container. If unspecified, the container runtime will allocate a random SELinux context for each container.  May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [JsonProperty("seLinuxOptions")]
        [YamlMember(Alias = "seLinuxOptions")]
        public SELinuxOptionsV1 SeLinuxOptions { get; set; }

        /// <summary>
        ///     Run container in privileged mode. Processes in privileged containers are essentially equivalent to root on the host. Defaults to false.
        /// </summary>
        [JsonProperty("privileged")]
        [YamlMember(Alias = "privileged")]
        public bool Privileged { get; set; }

        /// <summary>
        ///     The GID to run the entrypoint of the container process. Uses runtime default if unset. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [JsonProperty("runAsGroup")]
        [YamlMember(Alias = "runAsGroup")]
        public int RunAsGroup { get; set; }

        /// <summary>
        ///     AllowPrivilegeEscalation controls whether a process can gain more privileges than its parent process. This bool directly controls if the no_new_privs flag will be set on the container process. AllowPrivilegeEscalation is true always when the container is: 1) run as Privileged 2) has CAP_SYS_ADMIN
        /// </summary>
        [JsonProperty("allowPrivilegeEscalation")]
        [YamlMember(Alias = "allowPrivilegeEscalation")]
        public bool AllowPrivilegeEscalation { get; set; }

        /// <summary>
        ///     The UID to run the entrypoint of the container process. Defaults to user specified in image metadata if unspecified. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [JsonProperty("runAsUser")]
        [YamlMember(Alias = "runAsUser")]
        public int RunAsUser { get; set; }

        /// <summary>
        ///     Whether this container has a read-only root filesystem. Default is false.
        /// </summary>
        [JsonProperty("readOnlyRootFilesystem")]
        [YamlMember(Alias = "readOnlyRootFilesystem")]
        public bool ReadOnlyRootFilesystem { get; set; }

        /// <summary>
        ///     The capabilities to add/drop when running containers. Defaults to the default set of capabilities granted by the container runtime.
        /// </summary>
        [JsonProperty("capabilities")]
        [YamlMember(Alias = "capabilities")]
        public CapabilitiesV1 Capabilities { get; set; }
    }
}
