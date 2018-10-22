using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Endpoints is a collection of endpoints that implement the actual service. Example:
    ///       Name: "mysvc",
    ///       Subsets: [
    ///         {
    ///           Addresses: [{"ip": "10.10.1.1"}, {"ip": "10.10.2.2"}],
    ///           Ports: [{"name": "a", "port": 8675}, {"name": "b", "port": 309}]
    ///         },
    ///         {
    ///           Addresses: [{"ip": "10.10.3.3"}],
    ///           Ports: [{"name": "a", "port": 93}, {"name": "b", "port": 76}]
    ///         },
    ///      ]
    /// </summary>
    [KubeObject("Endpoints", "v1")]
    [KubeApi(KubeAction.List, "api/v1/endpoints")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/endpoints")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/endpoints")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/endpoints")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/endpoints/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/endpoints/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/endpoints/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/endpoints/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/endpoints")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/endpoints")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/endpoints/{name}")]
    public partial class EndpointsV1 : KubeResourceV1
    {
        /// <summary>
        ///     The set of all endpoints is the union of all subsets. Addresses are placed into subsets according to the IPs they share. A single address with multiple ports, some of which are ready and some of which are not (because they come from different containers) will result in the address being displayed in different subsets for the different ports. No address will appear in both Addresses and NotReadyAddresses in the same subset. Sets of addresses and ports that comprise a service.
        /// </summary>
        [YamlMember(Alias = "subsets")]
        [JsonProperty("subsets", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EndpointSubsetV1> Subsets { get; } = new List<EndpointSubsetV1>();

        /// <summary>
        ///     Determine whether the <see cref="Subsets"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSubsets() => Subsets.Count > 0;
    }
}
