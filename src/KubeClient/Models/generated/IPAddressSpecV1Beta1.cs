using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IPAddressSpec describe the attributes in an IP Address.
    /// </summary>
    public partial class IPAddressSpecV1Beta1
    {
        /// <summary>
        ///     ParentRef references the resource that an IPAddress is attached to. An IPAddress must reference a parent object.
        /// </summary>
        [YamlMember(Alias = "parentRef")]
        [JsonProperty("parentRef", NullValueHandling = NullValueHandling.Include)]
        public ParentReferenceV1Beta1 ParentRef { get; set; }
    }
}
