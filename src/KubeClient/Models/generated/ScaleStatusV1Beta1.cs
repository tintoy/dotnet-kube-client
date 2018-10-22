using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     represents the current status of a scale subresource.
    /// </summary>
    public partial class ScaleStatusV1Beta1
    {
        /// <summary>
        ///     label query over pods that should match the replicas count. More info: http://kubernetes.io/docs/user-guide/labels#label-selectors
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Selector { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Selector"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSelector() => Selector.Count > 0;

        /// <summary>
        ///     label selector for pods that should match the replicas count. This is a serializated version of both map-based and more expressive set-based selectors. This is done to avoid introspection in the clients. The string will be in the same format as the query-param syntax. If the target type only supports map-based selectors, both this field and map-based selector field are populated. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
        /// </summary>
        [YamlMember(Alias = "targetSelector")]
        [JsonProperty("targetSelector", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetSelector { get; set; }

        /// <summary>
        ///     actual number of observed instances of the scaled object.
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Include)]
        public int Replicas { get; set; }
    }
}
