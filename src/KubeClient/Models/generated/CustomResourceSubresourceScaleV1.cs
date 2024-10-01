using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceSubresourceScale defines how to serve the scale subresource for CustomResources.
    /// </summary>
    public partial class CustomResourceSubresourceScaleV1
    {
        /// <summary>
        ///     labelSelectorPath defines the JSON path inside of a custom resource that corresponds to Scale `status.selector`. Only JSON paths without the array notation are allowed. Must be a JSON Path under `.status` or `.spec`. Must be set to work with HorizontalPodAutoscaler. The field pointed by this JSON path must be a string field (not a complex selector struct) which contains a serialized label selector in string form. More info: https://kubernetes.io/docs/tasks/access-kubernetes-api/custom-resources/custom-resource-definitions#scale-subresource If there is no value under the given path in the custom resource, the `status.selector` value in the `/scale` subresource will default to the empty string.
        /// </summary>
        [YamlMember(Alias = "labelSelectorPath")]
        [JsonProperty("labelSelectorPath", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelSelectorPath { get; set; }

        /// <summary>
        ///     specReplicasPath defines the JSON path inside of a custom resource that corresponds to Scale `spec.replicas`. Only JSON paths without the array notation are allowed. Must be a JSON Path under `.spec`. If there is no value under the given path in the custom resource, the `/scale` subresource will return an error on GET.
        /// </summary>
        [YamlMember(Alias = "specReplicasPath")]
        [JsonProperty("specReplicasPath", NullValueHandling = NullValueHandling.Include)]
        public string SpecReplicasPath { get; set; }

        /// <summary>
        ///     statusReplicasPath defines the JSON path inside of a custom resource that corresponds to Scale `status.replicas`. Only JSON paths without the array notation are allowed. Must be a JSON Path under `.status`. If there is no value under the given path in the custom resource, the `status.replicas` value in the `/scale` subresource will default to 0.
        /// </summary>
        [YamlMember(Alias = "statusReplicasPath")]
        [JsonProperty("statusReplicasPath", NullValueHandling = NullValueHandling.Include)]
        public string StatusReplicasPath { get; set; }
    }
}
