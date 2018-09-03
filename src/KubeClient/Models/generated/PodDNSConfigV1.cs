using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDNSConfig defines the DNS parameters of a pod in addition to those generated from DNSPolicy.
    /// </summary>
    public partial class PodDNSConfigV1
    {
        /// <summary>
        ///     A list of DNS name server IP addresses. This will be appended to the base nameservers generated from DNSPolicy. Duplicated nameservers will be removed.
        /// </summary>
        [YamlMember(Alias = "nameservers")]
        [JsonProperty("nameservers", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Nameservers { get; set; } = new List<string>();

        /// <summary>
        ///     A list of DNS resolver options. This will be merged with the base options generated from DNSPolicy. Duplicated entries will be removed. Resolution options given in Options will override those that appear in the base DNSPolicy.
        /// </summary>
        [YamlMember(Alias = "options")]
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public List<PodDNSConfigOptionV1> Options { get; set; } = new List<PodDNSConfigOptionV1>();

        /// <summary>
        ///     A list of DNS search domains for host-name lookup. This will be appended to the base search paths generated from DNSPolicy. Duplicated search paths will be removed.
        /// </summary>
        [YamlMember(Alias = "searches")]
        [JsonProperty("searches", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Searches { get; set; } = new List<string>();
    }
}
