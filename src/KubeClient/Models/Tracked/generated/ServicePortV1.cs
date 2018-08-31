using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ServicePort contains information on service's port.
    /// </summary>
    public partial class ServicePortV1 : Models.ServicePortV1, ITracked
    {
        /// <summary>
        ///     The name of this port within the service. This must be a DNS_LABEL. All ports within a ServiceSpec must have unique names. This maps to the 'Name' field in EndpointPort objects. Optional if only one ServicePort is defined on this service.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     The IP protocol for this port. Supports "TCP" and "UDP". Default is TCP.
        /// </summary>
        [JsonProperty("protocol")]
        [YamlMember(Alias = "protocol")]
        public override string Protocol
        {
            get
            {
                return base.Protocol;
            }
            set
            {
                base.Protocol = value;

                __ModifiedProperties__.Add("Protocol");
            }
        }


        /// <summary>
        ///     The port on each node on which this service is exposed when type=NodePort or LoadBalancer. Usually assigned by the system. If specified, it will be allocated to the service if unused or else creation of the service will fail. Default is to auto-allocate a port if the ServiceType of this Service requires one. More info: https://kubernetes.io/docs/concepts/services-networking/service/#type-nodeport
        /// </summary>
        [JsonProperty("nodePort")]
        [YamlMember(Alias = "nodePort")]
        public override int NodePort
        {
            get
            {
                return base.NodePort;
            }
            set
            {
                base.NodePort = value;

                __ModifiedProperties__.Add("NodePort");
            }
        }


        /// <summary>
        ///     The port that will be exposed by this service.
        /// </summary>
        [JsonProperty("port")]
        [YamlMember(Alias = "port")]
        public override int Port
        {
            get
            {
                return base.Port;
            }
            set
            {
                base.Port = value;

                __ModifiedProperties__.Add("Port");
            }
        }


        /// <summary>
        ///     Number or name of the port to access on the pods targeted by the service. Number must be in the range 1 to 65535. Name must be an IANA_SVC_NAME. If this is a string, it will be looked up as a named port in the target Pod's container ports. If this is not specified, the value of the 'port' field is used (an identity map). This field is ignored for services with clusterIP=None, and should be omitted or set equal to the 'port' field. More info: https://kubernetes.io/docs/concepts/services-networking/service/#defining-a-service
        /// </summary>
        [JsonProperty("targetPort")]
        [YamlMember(Alias = "targetPort")]
        public override string TargetPort
        {
            get
            {
                return base.TargetPort;
            }
            set
            {
                base.TargetPort = value;

                __ModifiedProperties__.Add("TargetPort");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
