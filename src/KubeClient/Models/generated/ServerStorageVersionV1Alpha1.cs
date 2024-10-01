using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     An API server instance reports the version it can decode and the version it encodes objects to when persisting objects in the backend.
    /// </summary>
    public partial class ServerStorageVersionV1Alpha1
    {
        /// <summary>
        ///     The ID of the reporting API server.
        /// </summary>
        [YamlMember(Alias = "apiServerID")]
        [JsonProperty("apiServerID", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiServerID { get; set; }

        /// <summary>
        ///     The API server encodes the object to this version when persisting it in the backend (e.g., etcd).
        /// </summary>
        [YamlMember(Alias = "encodingVersion")]
        [JsonProperty("encodingVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string EncodingVersion { get; set; }

        /// <summary>
        ///     The API server can decode objects encoded in these versions. The encodingVersion must be included in the decodableVersions.
        /// </summary>
        [YamlMember(Alias = "decodableVersions")]
        [JsonProperty("decodableVersions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> DecodableVersions { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="DecodableVersions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDecodableVersions() => DecodableVersions.Count > 0;

        /// <summary>
        ///     The API server can serve these versions. DecodableVersions must include all ServedVersions.
        /// </summary>
        [YamlMember(Alias = "servedVersions")]
        [JsonProperty("servedVersions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ServedVersions { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="ServedVersions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeServedVersions() => ServedVersions.Count > 0;
    }
}
