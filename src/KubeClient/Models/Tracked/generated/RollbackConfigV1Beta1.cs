using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class RollbackConfigV1Beta1 : Models.RollbackConfigV1Beta1, ITracked
    {
        /// <summary>
        ///     The revision to rollback to. If set to 0, rollback to the last revision.
        /// </summary>
        [JsonProperty("revision")]
        [YamlMember(Alias = "revision")]
        public override int Revision
        {
            get
            {
                return base.Revision;
            }
            set
            {
                base.Revision = value;

                __ModifiedProperties__.Add("Revision");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
