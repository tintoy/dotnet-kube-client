using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressList is a list of Ingresses.
    /// </summary>
    [DataContract]
    public class VoyagerIngressListV1Beta1
    {
        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents.
        /// 
        ///     Servers may infer this from the endpoint the client submits requests to.
        ///     Cannot be updated.
        ///     In CamelCase.
        /// 
        ///     More info: http://releases.k8s.io/HEAD/docs/devel/api-conventions.md#types-kinds
        /// </summary>
        [DataMember(Name = "kind", EmitDefaultValue = false)]
        public string Kind { get; set; }

        /// <summary>
        ///     APIVersion defines the versioned schema of this representation of an object.
        /// 
        ///     Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: http://releases.k8s.io/HEAD/docs/devel/api-conventions.md#resources
        /// </summary>
        [DataMember(Name = "apiVersion", EmitDefaultValue = false)]
        public string ApiVersion { get; set; }

        /// <summary>
        ///     Standard list metadata.
        /// 
        ///     More info: http://releases.k8s.io/HEAD/docs/devel/api-conventions.md#types-kinds
        /// </summary>
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public ListMetaV1 Metadata { get; set; }

        /// <summary>
        ///     List of ingresses.
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public List<VoyagerIngressV1Beta1> Items { get; set; }
    }
}
