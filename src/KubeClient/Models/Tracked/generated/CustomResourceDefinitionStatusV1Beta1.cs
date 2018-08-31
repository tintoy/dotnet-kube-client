using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CustomResourceDefinitionStatus indicates the state of the CustomResourceDefinition
    /// </summary>
    public partial class CustomResourceDefinitionStatusV1Beta1 : Models.CustomResourceDefinitionStatusV1Beta1, ITracked
    {
        /// <summary>
        ///     AcceptedNames are the names that are actually being used to serve discovery They may be different than the names in spec.
        /// </summary>
        [JsonProperty("acceptedNames")]
        [YamlMember(Alias = "acceptedNames")]
        public override Models.CustomResourceDefinitionNamesV1Beta1 AcceptedNames
        {
            get
            {
                return base.AcceptedNames;
            }
            set
            {
                base.AcceptedNames = value;

                __ModifiedProperties__.Add("AcceptedNames");
            }
        }


        /// <summary>
        ///     Conditions indicate state for particular aspects of a CustomResourceDefinition
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.CustomResourceDefinitionConditionV1Beta1> Conditions { get; set; } = new List<Models.CustomResourceDefinitionConditionV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
