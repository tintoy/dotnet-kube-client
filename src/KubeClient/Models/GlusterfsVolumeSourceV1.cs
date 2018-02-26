using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Glusterfs mount that lasts the lifetime of a pod. Glusterfs volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    [KubeObject("GlusterfsVolumeSource", "v1")]
    public class GlusterfsVolumeSourceV1
    {
        /// <summary>
        ///     Path is the Glusterfs volume path. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md#create-a-pod
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        ///     EndpointsName is the endpoint name that details Glusterfs topology. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md#create-a-pod
        /// </summary>
        [JsonProperty("endpoints")]
        public string Endpoints { get; set; }

        /// <summary>
        ///     ReadOnly here will force the Glusterfs volume to be mounted with read-only permissions. Defaults to false. More info: https://releases.k8s.io/HEAD/examples/volumes/glusterfs/README.md#create-a-pod
        /// </summary>
        [JsonProperty("readOnly")]
        public bool ReadOnly { get; set; }
    }
}
