using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Secret holds secret data of a certain type. The total bytes of the values in the Data field must be less than MaxSecretSize bytes.
    /// </summary>
    [KubeObject("Secret", "v1")]
    [KubeApi(KubeAction.List, "api/v1/secrets")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/secrets")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/secrets")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/secrets")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/secrets/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/secrets/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/secrets/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/secrets/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/secrets")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/secrets")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/secrets/{name}")]
    public partial class SecretV1 : KubeResourceV1
    {
        /// <summary>
        ///     Data contains the secret data. Each key must consist of alphanumeric characters, '-', '_' or '.'. The serialized form of the secret data is a base64 encoded string, representing the arbitrary (possibly non-string) data value here. Described in https://tools.ietf.org/html/rfc4648#section-4
        /// </summary>
        [YamlMember(Alias = "data")]
        [JsonProperty("data", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Data { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Data"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeData() => Data.Count > 0;

        /// <summary>
        ///     stringData allows specifying non-binary secret data in string form. It is provided as a write-only convenience method. All keys and values are merged into the data field on write, overwriting any existing values. It is never output when reading from the API.
        /// </summary>
        [YamlMember(Alias = "stringData")]
        [JsonProperty("stringData", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> StringData { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="StringData"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeStringData() => StringData.Count > 0;

        /// <summary>
        ///     Used to facilitate programmatic handling of secret data.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
