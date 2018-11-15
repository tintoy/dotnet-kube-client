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
        [JsonProperty("nameservers", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Nameservers { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Nameservers"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNameservers() => Nameservers.Count > 0;

        /// <summary>
        ///     A list of DNS resolver options. This will be merged with the base options generated from DNSPolicy. Duplicated entries will be removed. Resolution options given in Options will override those that appear in the base DNSPolicy.
        /// </summary>
        [YamlMember(Alias = "options")]
        [JsonProperty("options", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodDNSConfigOptionV1> Options { get; } = new List<PodDNSConfigOptionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Options"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeOptions() => Options.Count > 0;

        /// <summary>
        ///     A list of DNS search domains for host-name lookup. This will be appended to the base search paths generated from DNSPolicy. Duplicated search paths will be removed.
        /// </summary>
        [YamlMember(Alias = "searches")]
        [JsonProperty("searches", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Searches { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Searches"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSearches() => Searches.Count > 0;
    }
}
