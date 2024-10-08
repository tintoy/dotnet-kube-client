using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Glusterfs mount that lasts the lifetime of a pod. Glusterfs volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class GlusterfsPersistentVolumeSourceV1
    {
        /// <summary>
        ///     endpointsNamespace is the namespace that contains Glusterfs endpoint. If this field is empty, the EndpointNamespace defaults to the same namespace as the bound PVC. More info: https://examples.k8s.io/volumes/glusterfs/README.md#create-a-pod
        /// </summary>
        [YamlMember(Alias = "endpointsNamespace")]
        [JsonProperty("endpointsNamespace", NullValueHandling = NullValueHandling.Ignore)]
        public string EndpointsNamespace { get; set; }

        /// <summary>
        ///     path is the Glusterfs volume path. More info: https://examples.k8s.io/volumes/glusterfs/README.md#create-a-pod
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }

        /// <summary>
        ///     endpoints is the endpoint name that details Glusterfs topology. More info: https://examples.k8s.io/volumes/glusterfs/README.md#create-a-pod
        /// </summary>
        [YamlMember(Alias = "endpoints")]
        [JsonProperty("endpoints", NullValueHandling = NullValueHandling.Include)]
        public string Endpoints { get; set; }

        /// <summary>
        ///     readOnly here will force the Glusterfs volume to be mounted with read-only permissions. Defaults to false. More info: https://examples.k8s.io/volumes/glusterfs/README.md#create-a-pod
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}