using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Preconditions must be fulfilled before an operation (update, delete, etc.) is carried out.
    /// </summary>
    public partial class PreconditionsV1
    {
        /// <summary>
        ///     Specifies the target UID.
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }
    }
}
