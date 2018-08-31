using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Initializers tracks the progress of initialization.
    /// </summary>
    public partial class InitializersV1 : Models.InitializersV1
    {
        /// <summary>
        ///     Pending is a list of initializers that must execute in order before this object is visible. When the last pending initializer is removed, and no failing result is set, the initializers struct will be set to nil and the object is considered as initialized and visible to all clients.
        /// </summary>
        [YamlMember(Alias = "pending")]
        [JsonProperty("pending", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.InitializerV1> Pending { get; set; } = new List<Models.InitializerV1>();

        /// <summary>
        ///     If result is set with the Failure field, the object will be persisted to storage and then deleted, ensuring that other clients can observe the deletion.
        /// </summary>
        [JsonProperty("result")]
        [YamlMember(Alias = "result")]
        public override Models.StatusV1 Result
        {
            get
            {
                return base.Result;
            }
            set
            {
                base.Result = value;

                __ModifiedProperties__.Add("Result");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
