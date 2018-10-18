using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodTemplateSpec describes the data a pod should have when created from a template
    /// </summary>
    public partial class PodTemplateSpecV1
    {
        /// <summary>
        ///     Standard object's metadata. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
        /// </summary>
        [YamlMember(Alias = "metadata")]
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectMetaV1 Metadata { get; set; }

        /// <summary>
        ///     Specification of the desired behavior of the pod. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PodSpecV1 Spec { get; set; }
    }
}
