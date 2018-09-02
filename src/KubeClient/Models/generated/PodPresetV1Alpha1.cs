using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodPreset is a policy resource that defines additional runtime requirements for a Pod.
    /// </summary>
    [KubeObject("PodPreset", "v1alpha1")]
    [KubeApi("apis/settings.k8s.io/v1alpha1/podpresets", KubeAction.List)]
    [KubeApi("apis/settings.k8s.io/v1alpha1/watch/podpresets", KubeAction.WatchList)]
    [KubeApi("apis/settings.k8s.io/v1alpha1/watch/namespaces/{namespace}/podpresets", KubeAction.WatchList)]
    [KubeApi("apis/settings.k8s.io/v1alpha1/watch/namespaces/{namespace}/podpresets/{name}", KubeAction.Watch)]
    [KubeApi("apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("apis/settings.k8s.io/v1alpha1/namespaces/{namespace}/podpresets/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    public partial class PodPresetV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public PodPresetSpecV1Alpha1 Spec { get; set; }
    }
}
