using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeSystemInfo is a set of ids/uuids to uniquely identify the node.
    /// </summary>
    public partial class NodeSystemInfoV1
    {
        /// <summary>
        ///     Boot ID reported by the node.
        /// </summary>
        [YamlMember(Alias = "bootID")]
        [JsonProperty("bootID", NullValueHandling = NullValueHandling.Include)]
        public string BootID { get; set; }

        /// <summary>
        ///     MachineID reported by the node. For unique machine identification in the cluster this field is preferred. Learn more from man(5) machine-id: http://man7.org/linux/man-pages/man5/machine-id.5.html
        /// </summary>
        [YamlMember(Alias = "machineID")]
        [JsonProperty("machineID", NullValueHandling = NullValueHandling.Include)]
        public string MachineID { get; set; }

        /// <summary>
        ///     SystemUUID reported by the node. For unique machine identification MachineID is preferred. This field is specific to Red Hat hosts https://access.redhat.com/documentation/en-US/Red_Hat_Subscription_Management/1/html/RHSM/getting-system-uuid.html
        /// </summary>
        [YamlMember(Alias = "systemUUID")]
        [JsonProperty("systemUUID", NullValueHandling = NullValueHandling.Include)]
        public string SystemUUID { get; set; }

        /// <summary>
        ///     The Architecture reported by the node
        /// </summary>
        [YamlMember(Alias = "architecture")]
        [JsonProperty("architecture", NullValueHandling = NullValueHandling.Include)]
        public string Architecture { get; set; }

        /// <summary>
        ///     OS Image reported by the node from /etc/os-release (e.g. Debian GNU/Linux 7 (wheezy)).
        /// </summary>
        [YamlMember(Alias = "osImage")]
        [JsonProperty("osImage", NullValueHandling = NullValueHandling.Include)]
        public string OsImage { get; set; }

        /// <summary>
        ///     The Operating System reported by the node
        /// </summary>
        [YamlMember(Alias = "operatingSystem")]
        [JsonProperty("operatingSystem", NullValueHandling = NullValueHandling.Include)]
        public string OperatingSystem { get; set; }

        /// <summary>
        ///     ContainerRuntime Version reported by the node through runtime remote API (e.g. docker://1.5.0).
        /// </summary>
        [YamlMember(Alias = "containerRuntimeVersion")]
        [JsonProperty("containerRuntimeVersion", NullValueHandling = NullValueHandling.Include)]
        public string ContainerRuntimeVersion { get; set; }

        /// <summary>
        ///     Kernel Version reported by the node from 'uname -r' (e.g. 3.16.0-0.bpo.4-amd64).
        /// </summary>
        [YamlMember(Alias = "kernelVersion")]
        [JsonProperty("kernelVersion", NullValueHandling = NullValueHandling.Include)]
        public string KernelVersion { get; set; }

        /// <summary>
        ///     KubeProxy Version reported by the node.
        /// </summary>
        [YamlMember(Alias = "kubeProxyVersion")]
        [JsonProperty("kubeProxyVersion", NullValueHandling = NullValueHandling.Include)]
        public string KubeProxyVersion { get; set; }

        /// <summary>
        ///     Kubelet Version reported by the node.
        /// </summary>
        [YamlMember(Alias = "kubeletVersion")]
        [JsonProperty("kubeletVersion", NullValueHandling = NullValueHandling.Include)]
        public string KubeletVersion { get; set; }
    }
}
