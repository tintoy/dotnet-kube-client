using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SecurityContext holds security configuration that will be applied to a container. Some fields are present in both SecurityContext and PodSecurityContext.  When both are set, the values in SecurityContext take precedence.
    /// </summary>
    public partial class SecurityContextV1 : Models.SecurityContextV1
    {
        /// <summary>
        ///     Run container in privileged mode. Processes in privileged containers are essentially equivalent to root on the host. Defaults to false.
        /// </summary>
        [JsonProperty("privileged")]
        [YamlMember(Alias = "privileged")]
        public override bool Privileged
        {
            get
            {
                return base.Privileged;
            }
            set
            {
                base.Privileged = value;

                __ModifiedProperties__.Add("Privileged");
            }
        }


        /// <summary>
        ///     Whether this container has a read-only root filesystem. Default is false.
        /// </summary>
        [JsonProperty("readOnlyRootFilesystem")]
        [YamlMember(Alias = "readOnlyRootFilesystem")]
        public override bool ReadOnlyRootFilesystem
        {
            get
            {
                return base.ReadOnlyRootFilesystem;
            }
            set
            {
                base.ReadOnlyRootFilesystem = value;

                __ModifiedProperties__.Add("ReadOnlyRootFilesystem");
            }
        }


        /// <summary>
        ///     The UID to run the entrypoint of the container process. Defaults to user specified in image metadata if unspecified. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [JsonProperty("runAsUser")]
        [YamlMember(Alias = "runAsUser")]
        public override int RunAsUser
        {
            get
            {
                return base.RunAsUser;
            }
            set
            {
                base.RunAsUser = value;

                __ModifiedProperties__.Add("RunAsUser");
            }
        }


        /// <summary>
        ///     The capabilities to add/drop when running containers. Defaults to the default set of capabilities granted by the container runtime.
        /// </summary>
        [JsonProperty("capabilities")]
        [YamlMember(Alias = "capabilities")]
        public override Models.CapabilitiesV1 Capabilities
        {
            get
            {
                return base.Capabilities;
            }
            set
            {
                base.Capabilities = value;

                __ModifiedProperties__.Add("Capabilities");
            }
        }


        /// <summary>
        ///     The SELinux context to be applied to the container. If unspecified, the container runtime will allocate a random SELinux context for each container.  May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [JsonProperty("seLinuxOptions")]
        [YamlMember(Alias = "seLinuxOptions")]
        public override Models.SELinuxOptionsV1 SeLinuxOptions
        {
            get
            {
                return base.SeLinuxOptions;
            }
            set
            {
                base.SeLinuxOptions = value;

                __ModifiedProperties__.Add("SeLinuxOptions");
            }
        }


        /// <summary>
        ///     Indicates that the container must run as a non-root user. If true, the Kubelet will validate the image at runtime to ensure that it does not run as UID 0 (root) and fail to start the container if it does. If unset or false, no such validation will be performed. May also be set in PodSecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [JsonProperty("runAsNonRoot")]
        [YamlMember(Alias = "runAsNonRoot")]
        public override bool RunAsNonRoot
        {
            get
            {
                return base.RunAsNonRoot;
            }
            set
            {
                base.RunAsNonRoot = value;

                __ModifiedProperties__.Add("RunAsNonRoot");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
