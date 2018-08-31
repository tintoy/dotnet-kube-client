using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StatusCause provides more information about an api.Status failure, including cases when multiple errors are encountered.
    /// </summary>
    public partial class StatusCauseV1 : Models.StatusCauseV1, ITracked
    {
        /// <summary>
        ///     The field of the resource that has caused this error, as named by its JSON serialization. May include dot and postfix notation for nested attributes. Arrays are zero-indexed.  Fields may appear more than once in an array of causes due to fields having multiple errors. Optional.
        ///     
        ///     Examples:
        ///       "name" - the field "name" on the current resource
        ///       "items[0].name" - the field "name" on the first array entry in "items"
        /// </summary>
        [JsonProperty("field")]
        [YamlMember(Alias = "field")]
        public override string Field
        {
            get
            {
                return base.Field;
            }
            set
            {
                base.Field = value;

                __ModifiedProperties__.Add("Field");
            }
        }


        /// <summary>
        ///     A human-readable description of the cause of the error.  This field may be presented as-is to a reader.
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
        ///     A machine-readable description of the cause of the error. If this value is empty there is no information available.
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
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
