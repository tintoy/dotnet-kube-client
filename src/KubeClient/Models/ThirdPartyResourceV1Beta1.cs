using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     A ThirdPartyResource is a generic representation of a resource, it is used by add-ons and plugins to add new resource types to the API.  It consists of one or more Versions of the api.
    /// </summary>
    [KubeObject("ThirdPartyResource", "extensions/v1beta1")]
    public class ThirdPartyResourceV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Description is the description of this object.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Versions are versions for this third party object
        /// </summary>
        [JsonProperty("versions", NullValueHandling = NullValueHandling.Ignore)]
        public List<APIVersionV1Beta1> Versions { get; set; } = new List<APIVersionV1Beta1>();
    }
}
