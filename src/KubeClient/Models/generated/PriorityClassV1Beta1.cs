using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityClass defines mapping from a priority class name to the priority integer value. The value can be any valid integer.
    /// </summary>
    [KubeObject("PriorityClass", "scheduling.k8s.io/v1beta1")]
    [KubeApi(KubeAction.List, "apis/scheduling.k8s.io/v1beta1/priorityclasses")]
    [KubeApi(KubeAction.Create, "apis/scheduling.k8s.io/v1beta1/priorityclasses")]
    [KubeApi(KubeAction.Get, "apis/scheduling.k8s.io/v1beta1/priorityclasses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/scheduling.k8s.io/v1beta1/priorityclasses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/scheduling.k8s.io/v1beta1/priorityclasses/{name}")]
    [KubeApi(KubeAction.Update, "apis/scheduling.k8s.io/v1beta1/priorityclasses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/scheduling.k8s.io/v1beta1/watch/priorityclasses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/scheduling.k8s.io/v1beta1/priorityclasses")]
    [KubeApi(KubeAction.Watch, "apis/scheduling.k8s.io/v1beta1/watch/priorityclasses/{name}")]
    public partial class PriorityClassV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     The value of this priority class. This is the actual priority that pods receive when they have the name of this class in their pod spec.
        /// </summary>
        [YamlMember(Alias = "value")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Include)]
        public int Value { get; set; }

        /// <summary>
        ///     description is an arbitrary string that usually provides guidelines on when this priority class should be used.
        /// </summary>
        [YamlMember(Alias = "description")]
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        ///     globalDefault specifies whether this PriorityClass should be considered as the default priority for pods that do not have any priority class. Only one PriorityClass can be marked as `globalDefault`. However, if more than one PriorityClasses exists with their `globalDefault` field set to true, the smallest value of such global default PriorityClasses will be used as the default priority.
        /// </summary>
        [YamlMember(Alias = "globalDefault")]
        [JsonProperty("globalDefault", NullValueHandling = NullValueHandling.Ignore)]
        public bool? GlobalDefault { get; set; }
    }
}
