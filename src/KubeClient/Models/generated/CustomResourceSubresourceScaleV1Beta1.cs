using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceSubresourceScale defines how to serve the scale subresource for CustomResources.
    /// </summary>
    public partial class CustomResourceSubresourceScaleV1Beta1
    {
        /// <summary>
        ///     StatusReplicasPath defines the JSON path inside of a CustomResource that corresponds to Scale.Status.Replicas. Only JSON paths without the array notation are allowed. Must be a JSON Path under .status. If there is no value under the given path in the CustomResource, the status replica value in the /scale subresource will default to 0.
        /// </summary>
        [JsonProperty("statusReplicasPath")]
        [YamlMember(Alias = "statusReplicasPath")]
        public string StatusReplicasPath { get; set; }

        /// <summary>
        ///     LabelSelectorPath defines the JSON path inside of a CustomResource that corresponds to Scale.Status.Selector. Only JSON paths without the array notation are allowed. Must be a JSON Path under .status. Must be set to work with HPA. If there is no value under the given path in the CustomResource, the status label selector value in the /scale subresource will default to the empty string.
        /// </summary>
        [JsonProperty("labelSelectorPath")]
        [YamlMember(Alias = "labelSelectorPath")]
        public string LabelSelectorPath { get; set; }

        /// <summary>
        ///     SpecReplicasPath defines the JSON path inside of a CustomResource that corresponds to Scale.Spec.Replicas. Only JSON paths without the array notation are allowed. Must be a JSON Path under .spec. If there is no value under the given path in the CustomResource, the /scale subresource will return an error on GET.
        /// </summary>
        [JsonProperty("specReplicasPath")]
        [YamlMember(Alias = "specReplicasPath")]
        public string SpecReplicasPath { get; set; }
    }
}
