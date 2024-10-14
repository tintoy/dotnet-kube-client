using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaimTemplate is used to produce PersistentVolumeClaim objects as part of an EphemeralVolumeSource.
    /// </summary>
    public partial class PersistentVolumeClaimTemplateV1
    {
        /// <summary>
        ///     May contain labels and annotations that will be copied into the PVC when creating it. No other fields are allowed and will be rejected during validation.
        /// </summary>
        [YamlMember(Alias = "metadata")]
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectMetaV1 Metadata { get; set; }

        /// <summary>
        ///     The specification for the PersistentVolumeClaim. The entire content is copied unchanged into the PVC that gets created from this template. The same fields as in a PersistentVolumeClaim are also valid here.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public PersistentVolumeClaimSpecV1 Spec { get; set; }
    }
}
