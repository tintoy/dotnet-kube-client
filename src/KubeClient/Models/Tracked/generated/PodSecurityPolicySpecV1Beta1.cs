using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Pod Security Policy Spec defines the policy enforced.
    /// </summary>
    public partial class PodSecurityPolicySpecV1Beta1 : Models.PodSecurityPolicySpecV1Beta1, ITracked
    {
        /// <summary>
        ///     hostIPC determines if the policy allows the use of HostIPC in the pod spec.
        /// </summary>
        [JsonProperty("hostIPC")]
        [YamlMember(Alias = "hostIPC")]
        public override bool HostIPC
        {
            get
            {
                return base.HostIPC;
            }
            set
            {
                base.HostIPC = value;

                __ModifiedProperties__.Add("HostIPC");
            }
        }


        /// <summary>
        ///     hostPID determines if the policy allows the use of HostPID in the pod spec.
        /// </summary>
        [JsonProperty("hostPID")]
        [YamlMember(Alias = "hostPID")]
        public override bool HostPID
        {
            get
            {
                return base.HostPID;
            }
            set
            {
                base.HostPID = value;

                __ModifiedProperties__.Add("HostPID");
            }
        }


        /// <summary>
        ///     privileged determines if a pod can request to be run as privileged.
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
        ///     hostNetwork determines if the policy allows the use of HostNetwork in the pod spec.
        /// </summary>
        [JsonProperty("hostNetwork")]
        [YamlMember(Alias = "hostNetwork")]
        public override bool HostNetwork
        {
            get
            {
                return base.HostNetwork;
            }
            set
            {
                base.HostNetwork = value;

                __ModifiedProperties__.Add("HostNetwork");
            }
        }


        /// <summary>
        ///     ReadOnlyRootFilesystem when set to true will force containers to run with a read only root file system.  If the container specifically requests to run with a non-read only root file system the PSP should deny the pod. If set to false the container may run with a read only root file system if it wishes but it will not be forced to.
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
        ///     FSGroup is the strategy that will dictate what fs group is used by the SecurityContext.
        /// </summary>
        [JsonProperty("fsGroup")]
        [YamlMember(Alias = "fsGroup")]
        public override Models.FSGroupStrategyOptionsV1Beta1 FsGroup
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
        ///     runAsUser is the strategy that will dictate the allowable RunAsUser values that may be set.
        /// </summary>
        [JsonProperty("runAsUser")]
        [YamlMember(Alias = "runAsUser")]
        public override Models.RunAsUserStrategyOptionsV1Beta1 RunAsUser
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
        ///     AllowedCapabilities is a list of capabilities that can be requested to add to the container. Capabilities in this field may be added at the pod author's discretion. You must not list a capability in both AllowedCapabilities and RequiredDropCapabilities.
        /// </summary>
        [YamlMember(Alias = "allowedCapabilities")]
        [JsonProperty("allowedCapabilities", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> AllowedCapabilities { get; set; } = new List<string>();

        /// <summary>
        ///     DefaultAddCapabilities is the default set of capabilities that will be added to the container unless the pod spec specifically drops the capability.  You may not list a capabiility in both DefaultAddCapabilities and RequiredDropCapabilities.
        /// </summary>
        [YamlMember(Alias = "defaultAddCapabilities")]
        [JsonProperty("defaultAddCapabilities", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> DefaultAddCapabilities { get; set; } = new List<string>();

        /// <summary>
        ///     hostPorts determines which host port ranges are allowed to be exposed.
        /// </summary>
        [YamlMember(Alias = "hostPorts")]
        [JsonProperty("hostPorts", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.HostPortRangeV1Beta1> HostPorts { get; set; } = new List<Models.HostPortRangeV1Beta1>();

        /// <summary>
        ///     RequiredDropCapabilities are the capabilities that will be dropped from the container.  These are required to be dropped and cannot be added.
        /// </summary>
        [YamlMember(Alias = "requiredDropCapabilities")]
        [JsonProperty("requiredDropCapabilities", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> RequiredDropCapabilities { get; set; } = new List<string>();

        /// <summary>
        ///     SupplementalGroups is the strategy that will dictate what supplemental groups are used by the SecurityContext.
        /// </summary>
        [JsonProperty("supplementalGroups")]
        [YamlMember(Alias = "supplementalGroups")]
        public override Models.SupplementalGroupsStrategyOptionsV1Beta1 SupplementalGroups
        {
            get
            {
                return base.SupplementalGroups;
            }
            set
            {
                base.SupplementalGroups = value;

                __ModifiedProperties__.Add("SupplementalGroups");
            }
        }


        /// <summary>
        ///     volumes is a white list of allowed volume plugins.  Empty indicates that all plugins may be used.
        /// </summary>
        [YamlMember(Alias = "volumes")]
        [JsonProperty("volumes", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Volumes { get; set; } = new List<string>();

        /// <summary>
        ///     seLinux is the strategy that will dictate the allowable labels that may be set.
        /// </summary>
        [JsonProperty("seLinux")]
        [YamlMember(Alias = "seLinux")]
        public override Models.SELinuxStrategyOptionsV1Beta1 SeLinux
        {
            get
            {
                return base.SeLinux;
            }
            set
            {
                base.SeLinux = value;

                __ModifiedProperties__.Add("SeLinux");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
