using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NodeSystemInfo is a set of ids/uuids to uniquely identify the node.
    /// </summary>
    public partial class NodeSystemInfoV1 : Models.NodeSystemInfoV1, ITracked
    {
        /// <summary>
        ///     Boot ID reported by the node.
        /// </summary>
        [JsonProperty("bootID")]
        [YamlMember(Alias = "bootID")]
        public override string BootID
        {
            get
            {
                return base.BootID;
            }
            set
            {
                base.BootID = value;

                __ModifiedProperties__.Add("BootID");
            }
        }


        /// <summary>
        ///     MachineID reported by the node. For unique machine identification in the cluster this field is preferred. Learn more from man(5) machine-id: http://man7.org/linux/man-pages/man5/machine-id.5.html
        /// </summary>
        [JsonProperty("machineID")]
        [YamlMember(Alias = "machineID")]
        public override string MachineID
        {
            get
            {
                return base.MachineID;
            }
            set
            {
                base.MachineID = value;

                __ModifiedProperties__.Add("MachineID");
            }
        }


        /// <summary>
        ///     SystemUUID reported by the node. For unique machine identification MachineID is preferred. This field is specific to Red Hat hosts https://access.redhat.com/documentation/en-US/Red_Hat_Subscription_Management/1/html/RHSM/getting-system-uuid.html
        /// </summary>
        [JsonProperty("systemUUID")]
        [YamlMember(Alias = "systemUUID")]
        public override string SystemUUID
        {
            get
            {
                return base.SystemUUID;
            }
            set
            {
                base.SystemUUID = value;

                __ModifiedProperties__.Add("SystemUUID");
            }
        }


        /// <summary>
        ///     The Architecture reported by the node
        /// </summary>
        [JsonProperty("architecture")]
        [YamlMember(Alias = "architecture")]
        public override string Architecture
        {
            get
            {
                return base.Architecture;
            }
            set
            {
                base.Architecture = value;

                __ModifiedProperties__.Add("Architecture");
            }
        }


        /// <summary>
        ///     OS Image reported by the node from /etc/os-release (e.g. Debian GNU/Linux 7 (wheezy)).
        /// </summary>
        [JsonProperty("osImage")]
        [YamlMember(Alias = "osImage")]
        public override string OsImage
        {
            get
            {
                return base.OsImage;
            }
            set
            {
                base.OsImage = value;

                __ModifiedProperties__.Add("OsImage");
            }
        }


        /// <summary>
        ///     The Operating System reported by the node
        /// </summary>
        [JsonProperty("operatingSystem")]
        [YamlMember(Alias = "operatingSystem")]
        public override string OperatingSystem
        {
            get
            {
                return base.OperatingSystem;
            }
            set
            {
                base.OperatingSystem = value;

                __ModifiedProperties__.Add("OperatingSystem");
            }
        }


        /// <summary>
        ///     ContainerRuntime Version reported by the node through runtime remote API (e.g. docker://1.5.0).
        /// </summary>
        [JsonProperty("containerRuntimeVersion")]
        [YamlMember(Alias = "containerRuntimeVersion")]
        public override string ContainerRuntimeVersion
        {
            get
            {
                return base.ContainerRuntimeVersion;
            }
            set
            {
                base.ContainerRuntimeVersion = value;

                __ModifiedProperties__.Add("ContainerRuntimeVersion");
            }
        }


        /// <summary>
        ///     Kernel Version reported by the node from 'uname -r' (e.g. 3.16.0-0.bpo.4-amd64).
        /// </summary>
        [JsonProperty("kernelVersion")]
        [YamlMember(Alias = "kernelVersion")]
        public override string KernelVersion
        {
            get
            {
                return base.KernelVersion;
            }
            set
            {
                base.KernelVersion = value;

                __ModifiedProperties__.Add("KernelVersion");
            }
        }


        /// <summary>
        ///     KubeProxy Version reported by the node.
        /// </summary>
        [JsonProperty("kubeProxyVersion")]
        [YamlMember(Alias = "kubeProxyVersion")]
        public override string KubeProxyVersion
        {
            get
            {
                return base.KubeProxyVersion;
            }
            set
            {
                base.KubeProxyVersion = value;

                __ModifiedProperties__.Add("KubeProxyVersion");
            }
        }


        /// <summary>
        ///     Kubelet Version reported by the node.
        /// </summary>
        [JsonProperty("kubeletVersion")]
        [YamlMember(Alias = "kubeletVersion")]
        public override string KubeletVersion
        {
            get
            {
                return base.KubeletVersion;
            }
            set
            {
                base.KubeletVersion = value;

                __ModifiedProperties__.Add("KubeletVersion");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
