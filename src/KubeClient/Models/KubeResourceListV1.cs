using System.Collections.Generic;
using Newtonsoft.Json;

namespace KubeClient.Models
{
    /// <summary>
    ///     The base class for Kubernetes resource lists.
    /// </summary>
    public abstract class KubeResourceListV1
        : KubeObjectV1
    {
        /// <summary>
        ///     Standard list metadata. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
        /// </summary>
        [JsonProperty("metadata")]
        public ListMetaV1 Metadata { get; set; }
    }
}
