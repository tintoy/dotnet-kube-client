using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectMeta is metadata that all persisted resources must have, which includes all objects users must create.
    /// </summary>
    public partial class ObjectMetaV1
    {
        /// <summary>
        ///     Additional data (if any) that does not correspond to properties defined on the <see cref="ObjectMetaV1"/> model.
        /// </summary>
        [JsonExtensionData]
        readonly Dictionary<string, JToken> _extensionData = new Dictionary<string, JToken>();

        /// <summary>
        ///     Additional serialised data (if any) that does not correspond to properties defined on the <see cref="ObjectMetaV1"/> model.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public IDictionary<string, JToken> ExtensionData => _extensionData;
    }
}
