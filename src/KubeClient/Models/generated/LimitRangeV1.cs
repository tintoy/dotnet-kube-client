using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LimitRange sets resource usage limits for each kind of resource in a Namespace.
    /// </summary>
    [KubeObject("LimitRange", "v1")]
    [KubeApi(KubeAction.List, "api/v1/limitranges")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/limitranges")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/limitranges")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/limitranges")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/limitranges/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/limitranges/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/limitranges/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/limitranges/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/limitranges")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/limitranges")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/limitranges/{name}")]
    public partial class LimitRangeV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the limits enforced. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public LimitRangeSpecV1 Spec { get; set; }
    }
}
