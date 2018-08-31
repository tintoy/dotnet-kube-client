using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Information about the condition of a component.
    /// </summary>
    public partial class ComponentConditionV1 : Models.ComponentConditionV1
    {
        /// <summary>
        ///     Message about the condition for a component. For example, information about a health check.
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
        ///     Type of condition for a component. Valid value: "Healthy"
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
        ///     Condition error code for a component. For example, a health check error code.
        /// </summary>
        [JsonProperty("error")]
        [YamlMember(Alias = "error")]
        public override string Error
        {
            get
            {
                return base.Error;
            }
            set
            {
                base.Error = value;

                __ModifiedProperties__.Add("Error");
            }
        }


        /// <summary>
        ///     Status of the condition for a component. Valid values for "Healthy": "True", "False", or "Unknown".
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public override string Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                base.Status = value;

                __ModifiedProperties__.Add("Status");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
