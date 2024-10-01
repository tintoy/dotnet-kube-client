using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CSIDriver captures information about a Container Storage Interface (CSI) volume driver deployed on the cluster. Kubernetes attach detach controller uses this object to determine whether attach is required. Kubelet uses this object to determine whether pod information needs to be passed on mount. CSIDriver objects are non-namespaced.
    /// </summary>
    [KubeObject("CSIDriver", "storage.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/storage.k8s.io/v1/csidrivers")]
    [KubeApi(KubeAction.Create, "apis/storage.k8s.io/v1/csidrivers")]
    [KubeApi(KubeAction.Get, "apis/storage.k8s.io/v1/csidrivers/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storage.k8s.io/v1/csidrivers/{name}")]
    [KubeApi(KubeAction.Delete, "apis/storage.k8s.io/v1/csidrivers/{name}")]
    [KubeApi(KubeAction.Update, "apis/storage.k8s.io/v1/csidrivers/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/storage.k8s.io/v1/watch/csidrivers")]
    [KubeApi(KubeAction.DeleteCollection, "apis/storage.k8s.io/v1/csidrivers")]
    [KubeApi(KubeAction.Watch, "apis/storage.k8s.io/v1/watch/csidrivers/{name}")]
    public partial class CSIDriverV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec represents the specification of the CSI Driver.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public CSIDriverSpecV1 Spec { get; set; }
    }
}
