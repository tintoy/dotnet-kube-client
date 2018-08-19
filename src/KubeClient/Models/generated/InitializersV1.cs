using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("pending", NullValueHandling = NullValueHandling.Ignore)]
        public List<InitializerV1> Pending { get; set; } = new List<InitializerV1>();

        /// <summary>
        ///     If result is set with the Failure field, the object will be persisted to storage and then deleted, ensuring that other clients can observe the deletion.
        /// </summary>
        [JsonProperty("result")]
        public StatusV1 Result { get; set; }
    }
}
