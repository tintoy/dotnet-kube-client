using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class RollbackConfigV1Beta1
    {
        /// <summary>
        ///     The revision to rollback to. If set to 0, rollback to the last revision.
        /// </summary>
        [JsonProperty("revision")]
        [YamlMember(Alias = "revision")]
        public virtual int Revision { get; set; }
    }
}
