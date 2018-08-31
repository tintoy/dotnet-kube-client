using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     APIResourceList is a list of APIResource, it is used to expose the name of the resources supported in a specific group and version, and if the resource is namespaced.
    /// </summary>
    public partial class APIResourceListV1 : Models.APIResourceListV1
    {
        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public override string Kind
        {
            get
            {
                return base.Kind;
            }
            set
            {
                base.Kind = value;

                __ModifiedProperties__.Add("Kind");
            }
        }


        /// <summary>
        ///     APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#resources
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public override string ApiVersion
        {
            get
            {
                return base.ApiVersion;
            }
            set
            {
                base.ApiVersion = value;

                __ModifiedProperties__.Add("ApiVersion");
            }
        }


        /// <summary>
        ///     groupVersion is the group and version this APIResourceList is for.
        /// </summary>
        [JsonProperty("groupVersion")]
        [YamlMember(Alias = "groupVersion")]
        public override string GroupVersion
        {
            get
            {
                return base.GroupVersion;
            }
            set
            {
                base.GroupVersion = value;

                __ModifiedProperties__.Add("GroupVersion");
            }
        }


        /// <summary>
        ///     resources contains the name of the resources and if they are namespaced.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.APIResourceV1> Resources { get; set; } = new List<Models.APIResourceV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
