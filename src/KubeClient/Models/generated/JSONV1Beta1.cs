using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSON represents any valid JSON value. These types are supported: bool, int64, float64, string, []interface{}, map[string]interface{} and nil.
    /// </summary>
    public partial class JSONV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "Raw")]
        [JsonProperty("Raw", NullValueHandling = NullValueHandling.Include)]
        public string Raw { get; set; }
    }
}
