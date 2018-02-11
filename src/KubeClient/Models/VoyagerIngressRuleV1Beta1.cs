using System.Runtime.Serialization;

namespace KubeClient.Models
{
    [DataContract]
    public class VoyagerIngressRuleV1Beta1
    {
        [DataMember(Name = "host", EmitDefaultValue = false)]
        public string Host { get; set; }

        [DataMember(Name = "tcp", EmitDefaultValue = false)]
        public VoyagerIngressRuleTcpV1Beta1 Tcp { get; set; }
    }
}
