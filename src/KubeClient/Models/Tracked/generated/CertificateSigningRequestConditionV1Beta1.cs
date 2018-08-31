using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class CertificateSigningRequestConditionV1Beta1 : Models.CertificateSigningRequestConditionV1Beta1, ITracked
    {
        /// <summary>
        ///     timestamp for the last update to this condition
        /// </summary>
        [JsonProperty("lastUpdateTime")]
        [YamlMember(Alias = "lastUpdateTime")]
        public override DateTime? LastUpdateTime
        {
            get
            {
                return base.LastUpdateTime;
            }
            set
            {
                base.LastUpdateTime = value;

                __ModifiedProperties__.Add("LastUpdateTime");
            }
        }


        /// <summary>
        ///     human readable message with details about the request state
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public override string Message
        {
            get
            {
                return base.Message;
            }
            set
            {
                base.Message = value;

                __ModifiedProperties__.Add("Message");
            }
        }


        /// <summary>
        ///     request approval state, currently Approved or Denied.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public override string Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;

                __ModifiedProperties__.Add("Type");
            }
        }


        /// <summary>
        ///     brief reason for the request state
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public override string Reason
        {
            get
            {
                return base.Reason;
            }
            set
            {
                base.Reason = value;

                __ModifiedProperties__.Add("Reason");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
