using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceSlice represents one or more resources in a pool of similar resources, managed by a common driver. A pool may span more than one ResourceSlice, and exactly how many ResourceSlices comprise a pool is determined by the driver.
    ///     
    ///     At the moment, the only supported resources are devices with attributes and capacities. Each device in a given pool, regardless of how many ResourceSlices, must have a unique name. The ResourceSlice in which a device gets published may change over time. The unique identifier for a device is the tuple &lt;driver name&gt;, &lt;pool name&gt;, &lt;device name&gt;.
    ///     
    ///     Whenever a driver needs to update a pool, it increments the pool.Spec.Pool.Generation number and updates all ResourceSlices with that new number and new resource definitions. A consumer must only use ResourceSlices with the highest generation number and ignore all others.
    ///     
    ///     When allocating all resources in a pool matching certain criteria or when looking for the best solution among several different alternatives, a consumer should check the number of ResourceSlices in a pool (included in each ResourceSlice) to determine whether its view of a pool is complete and if not, should wait until the driver has completed updating the pool.
    ///     
    ///     For resources that are not local to a node, the node name is not set. Instead, the driver may use a node selector to specify where the devices are available.
    ///     
    ///     This is an alpha type and requires enabling the DynamicResourceAllocation feature gate.
    /// </summary>
    [KubeObject("ResourceSlice", "resource.k8s.io/v1alpha3")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/resourceslices")]
    [KubeApi(KubeAction.Create, "apis/resource.k8s.io/v1alpha3/resourceslices")]
    [KubeApi(KubeAction.Get, "apis/resource.k8s.io/v1alpha3/resourceslices/{name}")]
    [KubeApi(KubeAction.Patch, "apis/resource.k8s.io/v1alpha3/resourceslices/{name}")]
    [KubeApi(KubeAction.Delete, "apis/resource.k8s.io/v1alpha3/resourceslices/{name}")]
    [KubeApi(KubeAction.Update, "apis/resource.k8s.io/v1alpha3/resourceslices/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/resourceslices")]
    [KubeApi(KubeAction.DeleteCollection, "apis/resource.k8s.io/v1alpha3/resourceslices")]
    [KubeApi(KubeAction.Watch, "apis/resource.k8s.io/v1alpha3/watch/resourceslices/{name}")]
    public partial class ResourceSliceV1Alpha3 : KubeResourceV1
    {
        /// <summary>
        ///     Contains the information published by the driver.
        ///     
        ///     Changing the spec automatically increments the metadata.generation number.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public ResourceSliceSpecV1Alpha3 Spec { get; set; }
    }
}
