using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED.
    /// </summary>
    [KubeObject("RollbackConfig", "v1beta1")]
    public partial class RollbackConfigV1Beta1
    {
        /// <summary>
        ///     The revision to rollback to. If set to 0, rollback to the last revision.
        /// </summary>
        [JsonProperty("revision")]
        public int Revision { get; set; }
    }
}
