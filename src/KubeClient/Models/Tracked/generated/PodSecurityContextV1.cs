using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodSecurityContext holds pod-level security attributes and common container settings. Some fields are also present in container.securityContext.  Field values of container.securityContext take precedence over field values of PodSecurityContext.
    /// </summary>
    public partial class PodSecurityContextV1 : Models.PodSecurityContextV1, ITracked
    {
        /// <summary>
        ///     A special supplemental group that applies to all containers in a pod. Some volume types allow the Kubelet to change the ownership of that volume to be owned by the pod:
        ///     
        ///     1. The owning GID will be the FSGroup 2. The setgid bit is set (new files created in the volume will be owned by FSGroup) 3. The permission bits are OR'd with rw-rw----
        ///     
        ///     If unset, the Kubelet will not modify the ownership and permissions of any volume.
        /// </summary>
        [JsonProperty("fsGroup")]
        [YamlMember(Alias = "fsGroup")]
        public override int FsGroup
        {
            get
            {
                return base.FsGroup;
            }
            set
            {
                base.FsGroup = value;

                __ModifiedProperties__.Add("FsGroup");
            }
        }


        /// <summary>
        ///     The UID to run the entrypoint of the container process. Defaults to user specified in image metadata if unspecified. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence for that container.
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
        ///     The SELinux context to be applied to all containers. If unspecified, the container runtime will allocate a random SELinux context for each container.  May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence for that container.
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
        ///     A list of groups applied to the first process run in each container, in addition to the container's primary GID.  If unspecified, no groups will be added to any container.
        /// </summary>
        [YamlMember(Alias = "supplementalGroups")]
        [JsonProperty("supplementalGroups", NullValueHandling = NullValueHandling.Ignore)]
        public override List<int> SupplementalGroups { get; set; } = new List<int>();

        /// <summary>
        ///     Indicates that the container must run as a non-root user. If true, the Kubelet will validate the image at runtime to ensure that it does not run as UID 0 (root) and fail to start the container if it does. If unset or false, no such validation will be performed. May also be set in SecurityContext.  If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
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
