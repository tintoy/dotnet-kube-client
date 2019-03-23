using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED.
    /// </summary>
    public partial class RollbackConfigV1Beta1
    {
        /// <summary>
        ///     The revision to rollback to. If set to 0, rollback to the last revision.
        /// </summary>
        [YamlMember(Alias = "revision")]
        [JsonProperty("revision", NullValueHandling = NullValueHandling.Ignore)]
        public long? Revision { get; set; }
    }
}
