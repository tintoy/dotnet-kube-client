using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Initializer describes the name and the failure policy of an initializer, and what resources it applies to.
    /// </summary>
    public partial class InitializerV1Alpha1
    {
        /// <summary>
        ///     Name is the identifier of the initializer. It will be added to the object that needs to be initialized. Name should be fully qualified, e.g., alwayspullimages.kubernetes.io, where "alwayspullimages" is the name of the webhook, and kubernetes.io is the name of the organization. Required
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Rules describes what resources/subresources the initializer cares about. The initializer cares about an operation if it matches _any_ Rule. Rule.Resources must not include subresources.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<RuleV1Alpha1> Rules { get; } = new List<RuleV1Alpha1>();

        /// <summary>
        ///     Determine whether the <see cref="Rules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRules() => Rules.Count > 0;
    }
}
