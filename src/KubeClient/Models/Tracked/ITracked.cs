using Newtonsoft.Json;
using System.Collections.Immutable;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Affinity is a group of affinity scheduling rules.
    /// </summary>
    public partial class ITracked
    {
        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public IImmutableSet<string> __ModifiedProperties__;
    }
}
