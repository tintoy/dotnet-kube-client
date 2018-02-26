using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIService represents a server for a particular GroupVersion. Name must be "version.group".
    /// </summary>
    [KubeObject("APIService", "v1beta1")]
    public class APIServiceV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec contains information for locating and communicating with a server
        /// </summary>
        [JsonProperty("spec")]
        public APIServiceSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status contains derived information about an API server
        /// </summary>
        [JsonProperty("status")]
        public APIServiceStatusV1Beta1 Status { get; set; }
    }
}
