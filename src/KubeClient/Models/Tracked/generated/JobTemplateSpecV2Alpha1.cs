using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JobTemplateSpec describes the data a Job should have when created from a template
    /// </summary>
    public partial class JobTemplateSpecV2Alpha1 : Models.JobTemplateSpecV2Alpha1, ITracked
    {
        /// <summary>
        ///     Standard object's metadata of the jobs created from this template. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
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
        ///     Specification of the desired behavior of the job. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.JobSpecV1 Spec
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
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
