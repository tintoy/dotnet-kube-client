using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Scale represents a scaling request for a resource.
    /// </summary>
    [KubeObject("Scale", "autoscaling/v1")]
    public partial class ScaleV1 : Models.ScaleV1, ITracked
    {
        /// <summary>
        ///     defines the behavior of the scale. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.ScaleSpecV1 Spec
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
        ///     current status of the scale. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status. Read-only.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public override Models.ScaleStatusV1 Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                base.Status = value;

                __ModifiedProperties__.Add("Status");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
