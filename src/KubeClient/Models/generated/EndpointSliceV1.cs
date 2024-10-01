using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EndpointSlice represents a subset of the endpoints that implement a service. For a given service there may be multiple EndpointSlice objects, selected by labels, which must be joined to produce the full set of endpoints.
    /// </summary>
    [KubeObject("EndpointSlice", "discovery.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/discovery.k8s.io/v1/endpointslices")]
    [KubeApi(KubeAction.WatchList, "apis/discovery.k8s.io/v1/watch/endpointslices")]
    [KubeApi(KubeAction.List, "apis/discovery.k8s.io/v1/namespaces/{namespace}/endpointslices")]
    [KubeApi(KubeAction.Create, "apis/discovery.k8s.io/v1/namespaces/{namespace}/endpointslices")]
    [KubeApi(KubeAction.Get, "apis/discovery.k8s.io/v1/namespaces/{namespace}/endpointslices/{name}")]
    [KubeApi(KubeAction.Patch, "apis/discovery.k8s.io/v1/namespaces/{namespace}/endpointslices/{name}")]
    [KubeApi(KubeAction.Delete, "apis/discovery.k8s.io/v1/namespaces/{namespace}/endpointslices/{name}")]
    [KubeApi(KubeAction.Update, "apis/discovery.k8s.io/v1/namespaces/{namespace}/endpointslices/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/discovery.k8s.io/v1/watch/namespaces/{namespace}/endpointslices")]
    [KubeApi(KubeAction.DeleteCollection, "apis/discovery.k8s.io/v1/namespaces/{namespace}/endpointslices")]
    [KubeApi(KubeAction.Watch, "apis/discovery.k8s.io/v1/watch/namespaces/{namespace}/endpointslices/{name}")]
    public partial class EndpointSliceV1 : KubeResourceV1
    {
        /// <summary>
        ///     addressType specifies the type of address carried by this EndpointSlice. All addresses in this slice must be the same type. This field is immutable after creation. The following address types are currently supported: * IPv4: Represents an IPv4 Address. * IPv6: Represents an IPv6 Address. * FQDN: Represents a Fully Qualified Domain Name.
        /// </summary>
        [YamlMember(Alias = "addressType")]
        [JsonProperty("addressType", NullValueHandling = NullValueHandling.Include)]
        public string AddressType { get; set; }

        /// <summary>
        ///     endpoints is a list of unique endpoints in this slice. Each slice may include a maximum of 1000 endpoints.
        /// </summary>
        [YamlMember(Alias = "endpoints")]
        [JsonProperty("endpoints", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EndpointV1> Endpoints { get; } = new List<EndpointV1>();

        /// <summary>
        ///     ports specifies the list of network ports exposed by each endpoint in this slice. Each port must have a unique name. When ports is empty, it indicates that there are no defined ports. When a port is defined with a nil port value, it indicates "all ports". Each slice may include a maximum of 100 ports.
        /// </summary>
        [YamlMember(Alias = "ports")]
        [JsonProperty("ports", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EndpointPortV1> Ports { get; } = new List<EndpointPortV1>();

        /// <summary>
        ///     Determine whether the <see cref="Ports"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePorts() => Ports.Count > 0;
    }
}
