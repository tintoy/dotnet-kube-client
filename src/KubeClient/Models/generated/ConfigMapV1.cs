using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ConfigMap holds configuration data for pods to consume.
    /// </summary>
    [KubeObject("ConfigMap", "v1")]
    [KubeApi(KubeAction.List, "api/v1/configmaps")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/configmaps")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/configmaps")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/configmaps")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/configmaps/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/configmaps/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/configmaps/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/configmaps/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/configmaps")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/configmaps")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/configmaps/{name}")]
    public partial class ConfigMapV1 : KubeResourceV1
    {
        /// <summary>
        ///     Data contains the configuration data. Each key must consist of alphanumeric characters, '-', '_' or '.'.
        /// </summary>
        [YamlMember(Alias = "data")]
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }
}
