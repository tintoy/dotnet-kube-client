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
        ///     BinaryData contains the binary data. Each key must consist of alphanumeric characters, '-', '_' or '.'. BinaryData can contain byte sequences that are not in the UTF-8 range. The keys stored in BinaryData must not overlap with the ones in the Data field, this is enforced during validation process. Using this field will require 1.10+ apiserver and kubelet.
        /// </summary>
        [YamlMember(Alias = "binaryData")]
        [JsonProperty("binaryData", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> BinaryData { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="BinaryData"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeBinaryData() => BinaryData.Count > 0;

        /// <summary>
        ///     Data contains the configuration data. Each key must consist of alphanumeric characters, '-', '_' or '.'. Values with non-UTF-8 byte sequences must use the BinaryData field. The keys stored in Data must not overlap with the keys in the BinaryData field, this is enforced during validation process.
        /// </summary>
        [YamlMember(Alias = "data")]
        [JsonProperty("data", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Data { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Data"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeData() => Data.Count > 0;
    }
}
