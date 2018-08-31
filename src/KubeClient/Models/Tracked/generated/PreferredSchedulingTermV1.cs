using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     An empty preferred scheduling term matches all objects with implicit weight 0 (i.e. it's a no-op). A null preferred scheduling term matches no objects (i.e. is also a no-op).
    /// </summary>
    public partial class PreferredSchedulingTermV1 : Models.PreferredSchedulingTermV1
    {
        /// <summary>
        ///     A node selector term, associated with the corresponding weight.
        /// </summary>
        [JsonProperty("preference")]
        [YamlMember(Alias = "preference")]
        public override Models.NodeSelectorTermV1 Preference
        {
            get
            {
                return base.Preference;
            }
            set
            {
                base.Preference = value;

                __ModifiedProperties__.Add("Preference");
            }
        }


        /// <summary>
        ///     Weight associated with matching the corresponding nodeSelectorTerm, in the range 1-100.
        /// </summary>
        [JsonProperty("weight")]
        [YamlMember(Alias = "weight")]
        public override int Weight
        {
            get
            {
                return base.Weight;
            }
            set
            {
                base.Weight = value;

                __ModifiedProperties__.Add("Weight");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
