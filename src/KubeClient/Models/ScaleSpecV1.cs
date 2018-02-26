using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ScaleSpec describes the attributes of a scale subresource.
    /// </summary>
    [KubeObject("ScaleSpec", "v1")]
    public class ScaleSpecV1
    {
        /// <summary>
        ///     desired number of instances for the scaled object.
        /// </summary>
        [JsonProperty("replicas")]
        public int Replicas { get; set; }
    }
}
