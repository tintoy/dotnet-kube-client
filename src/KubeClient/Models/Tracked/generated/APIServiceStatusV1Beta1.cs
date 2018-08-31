using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     APIServiceStatus contains derived information about an API server
    /// </summary>
    public partial class APIServiceStatusV1Beta1 : Models.APIServiceStatusV1Beta1
    {
        /// <summary>
        ///     Current service state of apiService.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.APIServiceConditionV1Beta1> Conditions { get; set; } = new List<Models.APIServiceConditionV1Beta1>();
    }
}
