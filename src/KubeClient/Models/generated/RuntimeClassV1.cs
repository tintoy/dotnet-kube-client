using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RuntimeClass defines a class of container runtime supported in the cluster. The RuntimeClass is used to determine which container runtime is used to run all containers in a pod. RuntimeClasses are manually defined by a user or cluster provisioner, and referenced in the PodSpec. The Kubelet is responsible for resolving the RuntimeClassName reference before running the pod.  For more details, see https://kubernetes.io/docs/concepts/containers/runtime-class/
    /// </summary>
    [KubeObject("RuntimeClass", "node.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/node.k8s.io/v1/runtimeclasses")]
    [KubeApi(KubeAction.Create, "apis/node.k8s.io/v1/runtimeclasses")]
    [KubeApi(KubeAction.Get, "apis/node.k8s.io/v1/runtimeclasses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/node.k8s.io/v1/runtimeclasses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/node.k8s.io/v1/runtimeclasses/{name}")]
    [KubeApi(KubeAction.Update, "apis/node.k8s.io/v1/runtimeclasses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/node.k8s.io/v1/watch/runtimeclasses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/node.k8s.io/v1/runtimeclasses")]
    [KubeApi(KubeAction.Watch, "apis/node.k8s.io/v1/watch/runtimeclasses/{name}")]
    public partial class RuntimeClassV1 : KubeResourceV1
    {
        /// <summary>
        ///     overhead represents the resource overhead associated with running a pod for a given RuntimeClass. For more details, see
        ///      https://kubernetes.io/docs/concepts/scheduling-eviction/pod-overhead/
        /// </summary>
        [YamlMember(Alias = "overhead")]
        [JsonProperty("overhead", NullValueHandling = NullValueHandling.Ignore)]
        public OverheadV1 Overhead { get; set; }

        /// <summary>
        ///     scheduling holds the scheduling constraints to ensure that pods running with this RuntimeClass are scheduled to nodes that support it. If scheduling is nil, this RuntimeClass is assumed to be supported by all nodes.
        /// </summary>
        [YamlMember(Alias = "scheduling")]
        [JsonProperty("scheduling", NullValueHandling = NullValueHandling.Ignore)]
        public SchedulingV1 Scheduling { get; set; }

        /// <summary>
        ///     handler specifies the underlying runtime and configuration that the CRI implementation will use to handle pods of this class. The possible values are specific to the node &amp; CRI configuration.  It is assumed that all handlers are available on every node, and handlers of the same name are equivalent on every node. For example, a handler called "runc" might specify that the runc OCI runtime (using native Linux containers) will be used to run the containers in a pod. The Handler must be lowercase, conform to the DNS Label (RFC 1123) requirements, and is immutable.
        /// </summary>
        [YamlMember(Alias = "handler")]
        [JsonProperty("handler", NullValueHandling = NullValueHandling.Include)]
        public string Handler { get; set; }
    }
}
