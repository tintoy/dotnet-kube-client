using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlowDistinguisherMethod specifies the method of a flow distinguisher.
    /// </summary>
    public partial class FlowDistinguisherMethodV1Beta3
    {
        /// <summary>
        ///     `type` is the type of flow distinguisher method The supported types are "ByUser" and "ByNamespace". Required.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }
    }
}
