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
    [KubeApi("api/v1/namespaces/{namespace}/secrets", KubeAction.Create, KubeAction.DeleteCollection)]
    [KubeApi("api/v1/namespaces/{namespace}/secrets/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/secrets", KubeAction.List)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/secrets/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/watch/secrets", KubeAction.WatchList)]
    public partial class SecretV1 : KubeResourceV1
    {
        /// <summary>
        ///     Data contains the secret data. Each key must consist of alphanumeric characters, '-', '_' or '.'. The serialized form of the secret data is a base64 encoded string, representing the arbitrary (possibly non-string) data value here. Described in https://tools.ietf.org/html/rfc4648#section-4
        /// </summary>
        [YamlMember(Alias = "data")]
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     stringData allows specifying non-binary secret data in string form. It is provided as a write-only convenience method. All keys and values are merged into the data field on write, overwriting any existing values. It is never output when reading from the API.
        /// </summary>
        [YamlMember(Alias = "stringData")]
        [JsonProperty("stringData", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> StringData { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Used to facilitate programmatic handling of secret data.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }
    }
}
