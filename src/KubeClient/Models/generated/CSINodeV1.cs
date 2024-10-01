using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CSINode holds information about all CSI drivers installed on a node. CSI drivers do not need to create the CSINode object directly. As long as they use the node-driver-registrar sidecar container, the kubelet will automatically populate the CSINode object for the CSI driver as part of kubelet plugin registration. CSINode has the same name as a node. If the object is missing, it means either there are no CSI Drivers available on the node, or the Kubelet version is low enough that it doesn't create this object. CSINode has an OwnerReference that points to the corresponding node object.
    /// </summary>
    [KubeObject("CSINode", "storage.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/storage.k8s.io/v1/csinodes")]
    [KubeApi(KubeAction.Create, "apis/storage.k8s.io/v1/csinodes")]
    [KubeApi(KubeAction.Get, "apis/storage.k8s.io/v1/csinodes/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storage.k8s.io/v1/csinodes/{name}")]
    [KubeApi(KubeAction.Delete, "apis/storage.k8s.io/v1/csinodes/{name}")]
    [KubeApi(KubeAction.Update, "apis/storage.k8s.io/v1/csinodes/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/storage.k8s.io/v1/watch/csinodes")]
    [KubeApi(KubeAction.DeleteCollection, "apis/storage.k8s.io/v1/csinodes")]
    [KubeApi(KubeAction.Watch, "apis/storage.k8s.io/v1/watch/csinodes/{name}")]
    public partial class CSINodeV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the specification of CSINode
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public CSINodeSpecV1 Spec { get; set; }
    }
}
