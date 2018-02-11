using System.Runtime.Serialization;

namespace KubeClient.Models
{
    [DataContract]
    public class VoyagerIngressV1Beta1
    {
        [DataMember(Name = "apiVersion", EmitDefaultValue = false)]
        public string ApiVersion { get; set; }

        [DataMember(Name = "kind", EmitDefaultValue = false)]
        public string Kind { get; set; }

        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public ObjectMetaV1 Metadata { get; set; }

        [DataMember(Name = "spec", EmitDefaultValue = false)]
        public VoyagerIngressSpecV1Beta1 Spec { get; set; }

        public IngressStatusV1Beta1 Status { get; set; }
    }
}
