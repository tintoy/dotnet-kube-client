using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceReference holds a reference to Service.legacy.k8s.io
    /// </summary>
    [KubeObject("ServiceReference", "v1beta1")]
    public partial class ServiceReferenceV1Beta1
    {
        /// <summary>
        ///     Name is the name of the service
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace is the namespace of the service
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}
