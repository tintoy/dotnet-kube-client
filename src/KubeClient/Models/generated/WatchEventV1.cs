using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Event represents a single event to a watched resource.
    /// </summary>
    public partial class WatchEventV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     Object is:
        ///      * If Type is Added or Modified: the new state of the object.
        ///      * If Type is Deleted: the state of the object immediately before deletion.
        ///      * If Type is Error: *Status is recommended; other types may make sense
        ///        depending on context.
        /// </summary>
        [YamlMember(Alias = "object")]
        [JsonProperty("object", NullValueHandling = NullValueHandling.Include)]
        public RawExtensionRuntime Object { get; set; }
    }
}
