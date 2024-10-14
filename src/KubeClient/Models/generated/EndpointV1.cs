using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Endpoint represents a single logical "backend" implementing a service.
    /// </summary>
    public partial class EndpointV1
    {
        /// <summary>
        ///     hostname of this endpoint. This field may be used by consumers of endpoints to distinguish endpoints from each other (e.g. in DNS names). Multiple endpoints which use the same hostname should be considered fungible (e.g. multiple A values in DNS). Must be lowercase and pass DNS Label (RFC 1123) validation.
        /// </summary>
        [YamlMember(Alias = "hostname")]
        [JsonProperty("hostname", NullValueHandling = NullValueHandling.Ignore)]
        public string Hostname { get; set; }

        /// <summary>
        ///     nodeName represents the name of the Node hosting this endpoint. This can be used to determine endpoints local to a Node.
        /// </summary>
        [YamlMember(Alias = "nodeName")]
        [JsonProperty("nodeName", NullValueHandling = NullValueHandling.Ignore)]
        public string NodeName { get; set; }

        /// <summary>
        ///     zone is the name of the Zone this endpoint exists in.
        /// </summary>
        [YamlMember(Alias = "zone")]
        [JsonProperty("zone", NullValueHandling = NullValueHandling.Ignore)]
        public string Zone { get; set; }

        /// <summary>
        ///     targetRef is a reference to a Kubernetes object that represents this endpoint.
        /// </summary>
        [YamlMember(Alias = "targetRef")]
        [JsonProperty("targetRef", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectReferenceV1 TargetRef { get; set; }

        /// <summary>
        ///     addresses of this endpoint. The contents of this field are interpreted according to the corresponding EndpointSlice addressType field. Consumers must handle different types of addresses in the context of their own capabilities. This must contain at least one address but no more than 100. These are all assumed to be fungible and clients may choose to only use the first element. Refer to: https://issue.k8s.io/106267
        /// </summary>
        [YamlMember(Alias = "addresses")]
        [JsonProperty("addresses", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Addresses { get; } = new List<string>();

        /// <summary>
        ///     conditions contains information about the current status of the endpoint.
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public EndpointConditionsV1 Conditions { get; set; }

        /// <summary>
        ///     hints contains information associated with how an endpoint should be consumed.
        /// </summary>
        [YamlMember(Alias = "hints")]
        [JsonProperty("hints", NullValueHandling = NullValueHandling.Ignore)]
        public EndpointHintsV1 Hints { get; set; }

        /// <summary>
        ///     deprecatedTopology contains topology information part of the v1beta1 API. This field is deprecated, and will be removed when the v1beta1 API is removed (no sooner than kubernetes v1.24).  While this field can hold values, it is not writable through the v1 API, and any attempts to write to it will be silently ignored. Topology information can be found in the zone and nodeName fields instead.
        /// </summary>
        [YamlMember(Alias = "deprecatedTopology")]
        [JsonProperty("deprecatedTopology", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> DeprecatedTopology { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="DeprecatedTopology"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDeprecatedTopology() => DeprecatedTopology.Count > 0;
    }
}
