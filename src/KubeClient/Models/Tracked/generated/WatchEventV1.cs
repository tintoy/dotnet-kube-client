using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Event represents a single event to a watched resource.
    /// </summary>
    public partial class WatchEventV1 : Models.WatchEventV1, ITracked
    {
        /// <summary>
        ///     Description not provided.
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
        ///     Object is:
        ///      * If Type is Added or Modified: the new state of the object.
        ///      * If Type is Deleted: the state of the object immediately before deletion.
        ///      * If Type is Error: *Status is recommended; other types may make sense
        ///        depending on context.
        /// </summary>
        [JsonProperty("object")]
        [YamlMember(Alias = "object")]
        public override Models.RawExtensionRuntime Object
        {
            get
            {
                return base.Object;
            }
            set
            {
                base.Object = value;

                __ModifiedProperties__.Add("Object");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
