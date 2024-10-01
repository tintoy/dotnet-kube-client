using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinition represents a resource that should be exposed on the API server.  Its name MUST be in the format &lt;.spec.name&gt;.&lt;.spec.group&gt;.
    /// </summary>
    [KubeObject("CustomResourceDefinition", "apiextensions.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/apiextensions.k8s.io/v1/customresourcedefinitions")]
    [KubeApi(KubeAction.Create, "apis/apiextensions.k8s.io/v1/customresourcedefinitions")]
    [KubeApi(KubeAction.Get, "apis/apiextensions.k8s.io/v1/customresourcedefinitions/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apiextensions.k8s.io/v1/customresourcedefinitions/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apiextensions.k8s.io/v1/customresourcedefinitions/{name}")]
    [KubeApi(KubeAction.Update, "apis/apiextensions.k8s.io/v1/customresourcedefinitions/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apiextensions.k8s.io/v1/watch/customresourcedefinitions")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apiextensions.k8s.io/v1/customresourcedefinitions")]
    [KubeApi(KubeAction.Get, "apis/apiextensions.k8s.io/v1/customresourcedefinitions/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/apiextensions.k8s.io/v1/watch/customresourcedefinitions/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apiextensions.k8s.io/v1/customresourcedefinitions/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/apiextensions.k8s.io/v1/customresourcedefinitions/{name}/status")]
    public partial class CustomResourceDefinitionV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec describes how the user wants the resources to appear
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public CustomResourceDefinitionSpecV1 Spec { get; set; }

        /// <summary>
        ///     status indicates the actual state of the CustomResourceDefinition
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceDefinitionStatusV1 Status { get; set; }
    }
}
