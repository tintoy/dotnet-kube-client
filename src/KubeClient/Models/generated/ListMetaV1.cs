using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ListMeta describes metadata that synthetic resources must have, including lists and various status objects. A resource may have only one of {ObjectMeta, ListMeta}.
    /// </summary>
    public partial class ListMetaV1
    {
        /// <summary>
        ///     SelfLink is a URL representing this object. Populated by the system. Read-only.
        /// </summary>
        [JsonProperty("selfLink")]
        [YamlMember(Alias = "selfLink")]
        public virtual string SelfLink { get; set; }

        /// <summary>
        ///     String that identifies the server's internal version of this object that can be used by clients to determine when objects have changed. Value must be treated as opaque by clients and passed unmodified back to the server. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#concurrency-control-and-consistency
        /// </summary>
        [JsonProperty("resourceVersion")]
        [YamlMember(Alias = "resourceVersion")]
        public virtual string ResourceVersion { get; set; }
    }
}
