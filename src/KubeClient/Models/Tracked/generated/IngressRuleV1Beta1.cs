using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     IngressRule represents the rules mapping the paths under a specified host to the related backend services. Incoming requests are first evaluated for a host match, then routed to the backend associated with the matching IngressRuleValue.
    /// </summary>
    public partial class IngressRuleV1Beta1 : Models.IngressRuleV1Beta1, ITracked
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("http")]
        [YamlMember(Alias = "http")]
        public override Models.HTTPIngressRuleValueV1Beta1 Http
        {
            get
            {
                return base.Http;
            }
            set
            {
                base.Http = value;

                __ModifiedProperties__.Add("Http");
            }
        }


        /// <summary>
        ///     Host is the fully qualified domain name of a network host, as defined by RFC 3986. Note the following deviations from the "host" part of the URI as defined in the RFC: 1. IPs are not allowed. Currently an IngressRuleValue can only apply to the
        ///     	  IP in the Spec of the parent Ingress.
        ///     2. The `:` delimiter is not respected because ports are not allowed.
        ///     	  Currently the port of an Ingress is implicitly :80 for http and
        ///     	  :443 for https.
        ///     Both these may change in the future. Incoming requests are matched against the host before the IngressRuleValue. If the host is unspecified, the Ingress routes all traffic based on the specified IngressRuleValue.
        /// </summary>
        [JsonProperty("host")]
        [YamlMember(Alias = "host")]
        public override string Host
        {
            get
            {
                return base.Host;
            }
            set
            {
                base.Host = value;

                __ModifiedProperties__.Add("Host");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
