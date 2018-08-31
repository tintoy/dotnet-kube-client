using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     represents the current status of a scale subresource.
    /// </summary>
    public partial class ScaleStatusV1Beta1 : Models.ScaleStatusV1Beta1, ITracked
    {
        /// <summary>
        ///     label query over pods that should match the replicas count. More info: http://kubernetes.io/docs/user-guide/labels#label-selectors
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Selector { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     label selector for pods that should match the replicas count. This is a serializated version of both map-based and more expressive set-based selectors. This is done to avoid introspection in the clients. The string will be in the same format as the query-param syntax. If the target type only supports map-based selectors, both this field and map-based selector field are populated. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
        /// </summary>
        [JsonProperty("targetSelector")]
        [YamlMember(Alias = "targetSelector")]
        public override string TargetSelector
        {
            get
            {
                return base.TargetSelector;
            }
            set
            {
                base.TargetSelector = value;

                __ModifiedProperties__.Add("TargetSelector");
            }
        }


        /// <summary>
        ///     actual number of observed instances of the scaled object.
        /// </summary>
        [JsonProperty("replicas")]
        [YamlMember(Alias = "replicas")]
        public override int Replicas
        {
            get
            {
                return base.Replicas;
            }
            set
            {
                base.Replicas = value;

                __ModifiedProperties__.Add("Replicas");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
