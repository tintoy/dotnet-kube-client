using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodPreset is a policy resource that defines additional runtime requirements for a Pod.
    /// </summary>
    [KubeObject("PodPreset", "settings.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/settings.k8s.io/v1alpha1/podpresets")]
    [KubeApi(KubeAction.WatchList, "apis/settings.k8s.io/v1alpha1/watch/podpresets")]
    [KubeApi(KubeAction.List, "apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets")]
    [KubeApi(KubeAction.Create, "apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets")]
    [KubeApi(KubeAction.Get, "apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets/{name}")]
    [KubeApi(KubeAction.Delete, "apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets/{name}")]
    [KubeApi(KubeAction.Update, "apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/settings.k8s.io/v1alpha1/watch/namespaces/{namespace}/podpresets")]
    [KubeApi(KubeAction.DeleteCollection, "apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets")]
    [KubeApi(KubeAction.Watch, "apis/settings.k8s.io/v1alpha1/watch/namespaces/{namespace}/podpresets/{name}")]
    public partial class PodPresetV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PodPresetSpecV1Alpha1 Spec { get; set; }
    }
}
