using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodTemplate describes a template for creating copies of a predefined pod.
    /// </summary>
    [KubeObject("PodTemplate", "v1")]
    public partial class PodTemplateV1 : Models.PodTemplateV1
    {
        /// <summary>
        ///     Template defines the pods that will be created from this pod template. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("template")]
        [YamlMember(Alias = "template")]
        public override Models.PodTemplateSpecV1 Template
        {
            get
            {
                return base.Template;
            }
            set
            {
                base.Template = value;

                __ModifiedProperties__.Add("Template");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
