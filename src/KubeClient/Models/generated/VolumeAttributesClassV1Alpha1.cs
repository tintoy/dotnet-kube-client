using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttributesClass represents a specification of mutable volume attributes defined by the CSI driver. The class can be specified during dynamic provisioning of PersistentVolumeClaims, and changed in the PersistentVolumeClaim spec after provisioning.
    /// </summary>
    [KubeObject("VolumeAttributesClass", "storage.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/storage.k8s.io/v1alpha1/volumeattributesclasses")]
    [KubeApi(KubeAction.Create, "apis/storage.k8s.io/v1alpha1/volumeattributesclasses")]
    [KubeApi(KubeAction.Get, "apis/storage.k8s.io/v1alpha1/volumeattributesclasses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storage.k8s.io/v1alpha1/volumeattributesclasses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/storage.k8s.io/v1alpha1/volumeattributesclasses/{name}")]
    [KubeApi(KubeAction.Update, "apis/storage.k8s.io/v1alpha1/volumeattributesclasses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/storage.k8s.io/v1alpha1/watch/volumeattributesclasses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/storage.k8s.io/v1alpha1/volumeattributesclasses")]
    [KubeApi(KubeAction.Watch, "apis/storage.k8s.io/v1alpha1/watch/volumeattributesclasses/{name}")]
    public partial class VolumeAttributesClassV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Name of the CSI driver This field is immutable.
        /// </summary>
        [YamlMember(Alias = "driverName")]
        [JsonProperty("driverName", NullValueHandling = NullValueHandling.Include)]
        public string DriverName { get; set; }

        /// <summary>
        ///     parameters hold volume attributes defined by the CSI driver. These values are opaque to the Kubernetes and are passed directly to the CSI driver. The underlying storage provider supports changing these attributes on an existing volume, however the parameters field itself is immutable. To invoke a volume update, a new VolumeAttributesClass should be created with new parameters, and the PersistentVolumeClaim should be updated to reference the new VolumeAttributesClass.
        ///     
        ///     This field is required and must contain at least one key/value pair. The keys cannot be empty, and the maximum number of parameters is 512, with a cumulative max size of 256K. If the CSI driver rejects invalid parameters, the target PersistentVolumeClaim will be set to an "Infeasible" state in the modifyVolumeStatus field.
        /// </summary>
        [YamlMember(Alias = "parameters")]
        [JsonProperty("parameters", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Parameters"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeParameters() => Parameters.Count > 0;
    }
}
