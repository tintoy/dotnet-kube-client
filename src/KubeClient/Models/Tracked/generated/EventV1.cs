using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Event is a report of an event somewhere in the cluster.
    /// </summary>
    [KubeObject("Event", "v1")]
    public partial class EventV1 : Models.EventV1
    {
        /// <summary>
        ///     A human-readable description of the status of this operation.
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
        ///     The component reporting this event. Should be a short machine understandable string.
        /// </summary>
        [JsonProperty("source")]
        [YamlMember(Alias = "source")]
        public override Models.EventSourceV1 Source
        {
            get
            {
                return base.Source;
            }
            set
            {
                base.Source = value;

                __ModifiedProperties__.Add("Source");
            }
        }


        /// <summary>
        ///     Type of this event (Normal, Warning), new types could be added in the future
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
        ///     This should be a short, machine understandable string that gives the reason for the transition into the object's current status.
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
        ///     The time at which the event was first recorded. (Time of server receipt is in TypeMeta.)
        /// </summary>
        [JsonProperty("firstTimestamp")]
        [YamlMember(Alias = "firstTimestamp")]
        public override DateTime? FirstTimestamp
        {
            get
            {
                return base.FirstTimestamp;
            }
            set
            {
                base.FirstTimestamp = value;

                __ModifiedProperties__.Add("FirstTimestamp");
            }
        }


        /// <summary>
        ///     The time at which the most recent occurrence of this event was recorded.
        /// </summary>
        [JsonProperty("lastTimestamp")]
        [YamlMember(Alias = "lastTimestamp")]
        public override DateTime? LastTimestamp
        {
            get
            {
                return base.LastTimestamp;
            }
            set
            {
                base.LastTimestamp = value;

                __ModifiedProperties__.Add("LastTimestamp");
            }
        }


        /// <summary>
        ///     The number of times this event has occurred.
        /// </summary>
        [JsonProperty("count")]
        [YamlMember(Alias = "count")]
        public override int Count
        {
            get
            {
                return base.Count;
            }
            set
            {
                base.Count = value;

                __ModifiedProperties__.Add("Count");
            }
        }


        /// <summary>
        ///     The object that this event is about.
        /// </summary>
        [JsonProperty("involvedObject")]
        [YamlMember(Alias = "involvedObject")]
        public override Models.ObjectReferenceV1 InvolvedObject
        {
            get
            {
                return base.InvolvedObject;
            }
            set
            {
                base.InvolvedObject = value;

                __ModifiedProperties__.Add("InvolvedObject");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
