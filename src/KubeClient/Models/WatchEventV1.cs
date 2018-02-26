using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Event represents a single event to a watched resource.
    /// </summary>
    [KubeObject("WatchEvent", "v1")]
    public class WatchEventV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Object is:
        ///      * If Type is Added or Modified: the new state of the object.
        ///      * If Type is Deleted: the state of the object immediately before deletion.
        ///      * If Type is Error: *Status is recommended; other types may make sense
        ///        depending on context.
        /// </summary>
        [JsonProperty("object")]
        public RawExtensionRuntime Object { get; set; }
    }
}
