using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceClass is a vendor- or admin-provided resource that contains device configuration and selectors. It can be referenced in the device requests of a claim to apply these presets. Cluster scoped.
    ///     
    ///     This is an alpha type and requires enabling the DynamicResourceAllocation feature gate.
    /// </summary>
    [KubeObject("DeviceClass", "resource.k8s.io/v1alpha3")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/deviceclasses")]
    [KubeApi(KubeAction.Create, "apis/resource.k8s.io/v1alpha3/deviceclasses")]
    [KubeApi(KubeAction.Get, "apis/resource.k8s.io/v1alpha3/deviceclasses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/resource.k8s.io/v1alpha3/deviceclasses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/resource.k8s.io/v1alpha3/deviceclasses/{name}")]
    [KubeApi(KubeAction.Update, "apis/resource.k8s.io/v1alpha3/deviceclasses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/deviceclasses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/resource.k8s.io/v1alpha3/deviceclasses")]
    [KubeApi(KubeAction.Watch, "apis/resource.k8s.io/v1alpha3/watch/deviceclasses/{name}")]
    public partial class DeviceClassV1Alpha3 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines what can be allocated and how to configure it.
        ///     
        ///     This is mutable. Consumers have to be prepared for classes changing at any time, either because they get updated or replaced. Claim allocations are done once based on whatever was set in classes at the time of allocation.
        ///     
        ///     Changing the spec automatically increments the metadata.generation number.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public DeviceClassSpecV1Alpha3 Spec { get; set; }
    }
}
