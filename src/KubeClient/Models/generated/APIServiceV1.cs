using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIService represents a server for a particular GroupVersion. Name must be "version.group".
    /// </summary>
    [KubeObject("APIService", "apiregistration.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/apiregistration.k8s.io/v1/apiservices")]
    [KubeApi(KubeAction.Create, "apis/apiregistration.k8s.io/v1/apiservices")]
    [KubeApi(KubeAction.Get, "apis/apiregistration.k8s.io/v1/apiservices/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apiregistration.k8s.io/v1/apiservices/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apiregistration.k8s.io/v1/apiservices/{name}")]
    [KubeApi(KubeAction.Update, "apis/apiregistration.k8s.io/v1/apiservices/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apiregistration.k8s.io/v1/watch/apiservices")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apiregistration.k8s.io/v1/apiservices")]
    [KubeApi(KubeAction.Get, "apis/apiregistration.k8s.io/v1/apiservices/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/apiregistration.k8s.io/v1/watch/apiservices/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apiregistration.k8s.io/v1/apiservices/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/apiregistration.k8s.io/v1/apiservices/{name}/status")]
    public partial class APIServiceV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec contains information for locating and communicating with a server
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public APIServiceSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status contains derived information about an API server
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public APIServiceStatusV1 Status { get; set; }
    }
}
