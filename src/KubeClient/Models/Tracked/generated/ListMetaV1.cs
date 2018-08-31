using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ListMeta describes metadata that synthetic resources must have, including lists and various status objects. A resource may have only one of {ObjectMeta, ListMeta}.
    /// </summary>
    public partial class ListMetaV1 : Models.ListMetaV1, ITracked
    {
        /// <summary>
        ///     SelfLink is a URL representing this object. Populated by the system. Read-only.
        /// </summary>
        [JsonProperty("selfLink")]
        [YamlMember(Alias = "selfLink")]
        public override string SelfLink
        {
            get
            {
                return base.SelfLink;
            }
            set
            {
                base.SelfLink = value;

                __ModifiedProperties__.Add("SelfLink");
            }
        }


        /// <summary>
        ///     String that identifies the server's internal version of this object that can be used by clients to determine when objects have changed. Value must be treated as opaque by clients and passed unmodified back to the server. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#concurrency-control-and-consistency
        /// </summary>
        [JsonProperty("resourceVersion")]
        [YamlMember(Alias = "resourceVersion")]
        public override string ResourceVersion
        {
            get
            {
                return base.ResourceVersion;
            }
            set
            {
                base.ResourceVersion = value;

                __ModifiedProperties__.Add("ResourceVersion");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
