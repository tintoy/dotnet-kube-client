using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodTemplate describes a template for creating copies of a predefined pod.
    /// </summary>
    [KubeObject("PodTemplate", "v1")]
    [KubeApi(KubeAction.List, "api/v1/podtemplates")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/podtemplates")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/podtemplates")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/podtemplates")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/podtemplates/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/podtemplates/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/podtemplates/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/podtemplates/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/podtemplates")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/podtemplates")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/podtemplates/{name}")]
    public partial class PodTemplateV1 : KubeResourceV1
    {
        /// <summary>
        ///     Template defines the pods that will be created from this pod template. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "template")]
        [JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
        public PodTemplateSpecV1 Template { get; set; }
    }
}
