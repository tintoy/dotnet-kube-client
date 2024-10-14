using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Defines a set of pods (namely those matching the labelSelector relative to the given namespace(s)) that this pod should be co-located (affinity) or not co-located (anti-affinity) with, where co-located is defined as running on a node whose value of the label with key &lt;topologyKey&gt; matches that of any node on which a pod of the set of pods is running
    /// </summary>
    public partial class PodAffinityTermV1
    {
        /// <summary>
        ///     A label query over a set of resources, in this case pods. If it's null, this PodAffinityTerm matches with no Pods.
        /// </summary>
        [YamlMember(Alias = "labelSelector")]
        [JsonProperty("labelSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 LabelSelector { get; set; }

        /// <summary>
        ///     A label query over the set of namespaces that the term applies to. The term is applied to the union of the namespaces selected by this field and the ones listed in the namespaces field. null selector and null or empty namespaces list means "this pod's namespace". An empty selector ({}) matches all namespaces.
        /// </summary>
        [YamlMember(Alias = "namespaceSelector")]
        [JsonProperty("namespaceSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 NamespaceSelector { get; set; }

        /// <summary>
        ///     MatchLabelKeys is a set of pod label keys to select which pods will be taken into consideration. The keys are used to lookup values from the incoming pod labels, those key-value labels are merged with `labelSelector` as `key in (value)` to select the group of existing pods which pods will be taken into consideration for the incoming pod's pod (anti) affinity. Keys that don't exist in the incoming pod labels will be ignored. The default value is empty. The same key is forbidden to exist in both matchLabelKeys and labelSelector. Also, matchLabelKeys cannot be set when labelSelector isn't set. This is a beta field and requires enabling MatchLabelKeysInPodAffinity feature gate (enabled by default).
        /// </summary>
        [YamlMember(Alias = "matchLabelKeys")]
        [JsonProperty("matchLabelKeys", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> MatchLabelKeys { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="MatchLabelKeys"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMatchLabelKeys() => MatchLabelKeys.Count > 0;

        /// <summary>
        ///     MismatchLabelKeys is a set of pod label keys to select which pods will be taken into consideration. The keys are used to lookup values from the incoming pod labels, those key-value labels are merged with `labelSelector` as `key notin (value)` to select the group of existing pods which pods will be taken into consideration for the incoming pod's pod (anti) affinity. Keys that don't exist in the incoming pod labels will be ignored. The default value is empty. The same key is forbidden to exist in both mismatchLabelKeys and labelSelector. Also, mismatchLabelKeys cannot be set when labelSelector isn't set. This is a beta field and requires enabling MatchLabelKeysInPodAffinity feature gate (enabled by default).
        /// </summary>
        [YamlMember(Alias = "mismatchLabelKeys")]
        [JsonProperty("mismatchLabelKeys", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> MismatchLabelKeys { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="MismatchLabelKeys"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMismatchLabelKeys() => MismatchLabelKeys.Count > 0;

        /// <summary>
        ///     namespaces specifies a static list of namespace names that the term applies to. The term is applied to the union of the namespaces listed in this field and the ones selected by namespaceSelector. null or empty namespaces list and null namespaceSelector means "this pod's namespace".
        /// </summary>
        [YamlMember(Alias = "namespaces")]
        [JsonProperty("namespaces", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Namespaces { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Namespaces"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNamespaces() => Namespaces.Count > 0;

        /// <summary>
        ///     This pod should be co-located (affinity) or not co-located (anti-affinity) with the pods matching the labelSelector in the specified namespaces, where co-located is defined as running on a node whose value of the label with key topologyKey matches that of any node on which any of the selected pods is running. Empty topologyKey is not allowed.
        /// </summary>
        [YamlMember(Alias = "topologyKey")]
        [JsonProperty("topologyKey", NullValueHandling = NullValueHandling.Include)]
        public string TopologyKey { get; set; }
    }
}
