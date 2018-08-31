using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodTemplateSpec describes the data a pod should have when created from a template
    /// </summary>
    public partial class PodTemplateSpecV1 : Models.PodTemplateSpecV1
    {
        /// <summary>
        ///     Standard object's metadata. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
        /// </summary>
        [JsonProperty("metadata")]
        [YamlMember(Alias = "metadata")]
        public override Models.ObjectMetaV1 Metadata
        {
            get
            {
                return base.Metadata;
            }
            set
            {
                base.Metadata = value;

                __ModifiedProperties__.Add("Metadata");
            }
        }


        /// <summary>
        ///     Specification of the desired behavior of the pod. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.PodSpecV1 Spec
        {
            get
            {
                return base.Spec;
            }
            set
            {
                base.Spec = value;

                __ModifiedProperties__.Add("Spec");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
