using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIService represents a server for a particular GroupVersion. Name must be "version.group".
    /// </summary>
    [KubeObject("APIService", "v1beta1")]
    public partial class APIServiceV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec contains information for locating and communicating with a server
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public virtual APIServiceSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status contains derived information about an API server
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public virtual APIServiceStatusV1Beta1 Status { get; set; }
    }
}
