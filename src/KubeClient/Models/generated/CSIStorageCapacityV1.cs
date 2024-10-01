using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CSIStorageCapacity stores the result of one CSI GetCapacity call. For a given StorageClass, this describes the available capacity in a particular topology segment.  This can be used when considering where to instantiate new PersistentVolumes.
    ///     
    ///     For example this can express things like: - StorageClass "standard" has "1234 GiB" available in "topology.kubernetes.io/zone=us-east1" - StorageClass "localssd" has "10 GiB" available in "kubernetes.io/hostname=knode-abc123"
    ///     
    ///     The following three cases all imply that no capacity is available for a certain combination: - no object exists with suitable topology and storage class name - such an object exists, but the capacity is unset - such an object exists, but the capacity is zero
    ///     
    ///     The producer of these objects can decide which approach is more suitable.
    ///     
    ///     They are consumed by the kube-scheduler when a CSI driver opts into capacity-aware scheduling with CSIDriverSpec.StorageCapacity. The scheduler compares the MaximumVolumeSize against the requested size of pending volumes to filter out unsuitable nodes. If MaximumVolumeSize is unset, it falls back to a comparison against the less precise Capacity. If that is also unset, the scheduler assumes that capacity is insufficient and tries some other node.
    /// </summary>
    [KubeObject("CSIStorageCapacity", "storage.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/storage.k8s.io/v1/csistoragecapacities")]
    [KubeApi(KubeAction.WatchList, "apis/storage.k8s.io/v1/watch/csistoragecapacities")]
    [KubeApi(KubeAction.List, "apis/storage.k8s.io/v1/namespaces/{namespace}/csistoragecapacities")]
    [KubeApi(KubeAction.Create, "apis/storage.k8s.io/v1/namespaces/{namespace}/csistoragecapacities")]
    [KubeApi(KubeAction.Get, "apis/storage.k8s.io/v1/namespaces/{namespace}/csistoragecapacities/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storage.k8s.io/v1/namespaces/{namespace}/csistoragecapacities/{name}")]
    [KubeApi(KubeAction.Delete, "apis/storage.k8s.io/v1/namespaces/{namespace}/csistoragecapacities/{name}")]
    [KubeApi(KubeAction.Update, "apis/storage.k8s.io/v1/namespaces/{namespace}/csistoragecapacities/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/storage.k8s.io/v1/watch/namespaces/{namespace}/csistoragecapacities")]
    [KubeApi(KubeAction.DeleteCollection, "apis/storage.k8s.io/v1/namespaces/{namespace}/csistoragecapacities")]
    [KubeApi(KubeAction.Watch, "apis/storage.k8s.io/v1/watch/namespaces/{namespace}/csistoragecapacities/{name}")]
    public partial class CSIStorageCapacityV1 : KubeResourceV1
    {
        /// <summary>
        ///     maximumVolumeSize is the value reported by the CSI driver in its GetCapacityResponse for a GetCapacityRequest with topology and parameters that match the previous fields.
        ///     
        ///     This is defined since CSI spec 1.4.0 as the largest size that may be used in a CreateVolumeRequest.capacity_range.required_bytes field to create a volume with the same parameters as those in GetCapacityRequest. The corresponding value in the Kubernetes API is ResourceRequirements.Requests in a volume claim.
        /// </summary>
        [YamlMember(Alias = "maximumVolumeSize")]
        [JsonProperty("maximumVolumeSize", NullValueHandling = NullValueHandling.Ignore)]
        public string MaximumVolumeSize { get; set; }

        /// <summary>
        ///     storageClassName represents the name of the StorageClass that the reported capacity applies to. It must meet the same requirements as the name of a StorageClass object (non-empty, DNS subdomain). If that object no longer exists, the CSIStorageCapacity object is obsolete and should be removed by its creator. This field is immutable.
        /// </summary>
        [YamlMember(Alias = "storageClassName")]
        [JsonProperty("storageClassName", NullValueHandling = NullValueHandling.Include)]
        public string StorageClassName { get; set; }

        /// <summary>
        ///     capacity is the value reported by the CSI driver in its GetCapacityResponse for a GetCapacityRequest with topology and parameters that match the previous fields.
        ///     
        ///     The semantic is currently (CSI spec 1.2) defined as: The available capacity, in bytes, of the storage that can be used to provision volumes. If not set, that information is currently unavailable.
        /// </summary>
        [YamlMember(Alias = "capacity")]
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public string Capacity { get; set; }

        /// <summary>
        ///     nodeTopology defines which nodes have access to the storage for which capacity was reported. If not set, the storage is not accessible from any node in the cluster. If empty, the storage is accessible from all nodes. This field is immutable.
        /// </summary>
        [YamlMember(Alias = "nodeTopology")]
        [JsonProperty("nodeTopology", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 NodeTopology { get; set; }
    }
}
