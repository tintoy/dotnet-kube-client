using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KubeClient.Models
{
    [DataContract]
    public class VoyagerIngressSpecV1Beta1
    {
        [DataMember(Name = "tls", EmitDefaultValue = false)]
        public List<IngressTLSV1Beta1> Tls { get; set; }

        [DataMember(Name = "rules", EmitDefaultValue = false)]
        public List<VoyagerIngressRuleV1Beta1> Rules { get; set; }
    }
}
