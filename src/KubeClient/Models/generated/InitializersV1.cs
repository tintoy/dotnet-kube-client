using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Initializers tracks the progress of initialization.
    /// </summary>
    public partial class InitializersV1
    {
        /// <summary>
        ///     Pending is a list of initializers that must execute in order before this object is visible. When the last pending initializer is removed, and no failing result is set, the initializers struct will be set to nil and the object is considered as initialized and visible to all clients.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "pending")]
        [JsonProperty("pending", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<InitializerV1> Pending { get; } = new List<InitializerV1>();

        /// <summary>
        ///     If result is set with the Failure field, the object will be persisted to storage and then deleted, ensuring that other clients can observe the deletion.
        /// </summary>
        [YamlMember(Alias = "result")]
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public StatusV1 Result { get; set; }
    }
}
