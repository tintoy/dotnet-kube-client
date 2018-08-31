using System.Collections.Generic;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Affinity is a group of affinity scheduling rules.
    /// </summary>
    public interface ITracked
    {
        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        ISet<string> __ModifiedProperties__ { get; }
    }
}
