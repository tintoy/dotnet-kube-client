using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JobTemplateSpec describes the data a Job should have when created from a template
    /// </summary>
    public partial class JobTemplateSpecV2Alpha1
    {
        /// <summary>
        ///     Standard object's metadata of the jobs created from this template. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
        /// </summary>
        [YamlMember(Alias = "metadata")]
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectMetaV1 Metadata { get; set; }

        /// <summary>
        ///     Specification of the desired behavior of the job. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public JobSpecV1 Spec { get; set; }
    }
}
