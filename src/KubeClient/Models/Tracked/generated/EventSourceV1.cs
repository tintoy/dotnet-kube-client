using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EventSource contains information for an event.
    /// </summary>
    public partial class EventSourceV1 : Models.EventSourceV1
    {
        /// <summary>
        ///     Component from which the event is generated.
        /// </summary>
        [JsonProperty("component")]
        [YamlMember(Alias = "component")]
        public override string Component
        {
            get
            {
                return base.Component;
            }
            set
            {
                base.Component = value;

                __ModifiedProperties__.Add("Component");
            }
        }


        /// <summary>
        ///     Node name on which the event is generated.
        /// </summary>
        [JsonProperty("host")]
        [YamlMember(Alias = "host")]
        public override string Host
        {
            get
            {
                return base.Host;
            }
            set
            {
                base.Host = value;

                __ModifiedProperties__.Add("Host");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
