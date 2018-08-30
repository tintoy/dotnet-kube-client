using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionStatus indicates the state of the CustomResourceDefinition
    /// </summary>
    public partial class CustomResourceDefinitionStatusV1Beta1
    {
        /// <summary>
        ///     AcceptedNames are the names that are actually being used to serve discovery They may be different than the names in spec.
        /// </summary>
        [JsonProperty("acceptedNames")]
        [YamlMember(Alias = "acceptedNames")]
        public virtual CustomResourceDefinitionNamesV1Beta1 AcceptedNames { get; set; }

        /// <summary>
        ///     Conditions indicate state for particular aspects of a CustomResourceDefinition
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<CustomResourceDefinitionConditionV1Beta1> Conditions { get; set; } = new List<CustomResourceDefinitionConditionV1Beta1>();
    }
}
