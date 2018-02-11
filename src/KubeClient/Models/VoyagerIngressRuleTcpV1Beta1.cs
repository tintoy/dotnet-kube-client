using System.Runtime.Serialization;

namespace KubeClient.Models
{
    [DataContract]
    public class VoyagerIngressRuleTcpV1Beta1
    {
        [DataMember(Name = "backend", EmitDefaultValue = false)]
        public IngressBackendV1Beta1 Backend { get; set; }

        [DataMember(Name = "port", EmitDefaultValue = false)]
        public string Port { get; set; }
    }
}
