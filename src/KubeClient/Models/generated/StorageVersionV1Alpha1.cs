using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Storage version of a specific resource.
    /// </summary>
    [KubeObject("StorageVersion", "internal.apiserver.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions")]
    [KubeApi(KubeAction.Create, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions")]
    [KubeApi(KubeAction.Get, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions/{name}")]
    [KubeApi(KubeAction.Patch, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions/{name}")]
    [KubeApi(KubeAction.Delete, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions/{name}")]
    [KubeApi(KubeAction.Update, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/internal.apiserver.k8s.io/v1alpha1/watch/storageversions")]
    [KubeApi(KubeAction.DeleteCollection, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions")]
    [KubeApi(KubeAction.Get, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/internal.apiserver.k8s.io/v1alpha1/watch/storageversions/{name}")]
    [KubeApi(KubeAction.Patch, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/internal.apiserver.k8s.io/v1alpha1/storageversions/{name}/status")]
    public partial class StorageVersionV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec is an empty spec. It is here to comply with Kubernetes API style.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public StorageVersionSpecV1Alpha1 Spec { get; set; }

        /// <summary>
        ///     API server instances report the version they can decode and the version they encode objects to when persisting objects in the backend.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public StorageVersionStatusV1Alpha1 Status { get; set; }
    }
}
