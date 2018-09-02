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
    [KubeApi("api/v1/namespaces/{namespace}/podtemplates", KubeAction.Create, KubeAction.DeleteCollection)]
    [KubeApi("api/v1/namespaces/{namespace}/podtemplates/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/podtemplates", KubeAction.List)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/podtemplates/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/watch/podtemplates", KubeAction.WatchList)]
    public partial class PodTemplateV1 : KubeResourceV1
    {
        /// <summary>
        ///     Template defines the pods that will be created from this pod template. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("template")]
        [YamlMember(Alias = "template")]
        public PodTemplateSpecV1 Template { get; set; }
    }
}
