using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A StatefulSetSpec is the specification of a StatefulSet.
    /// </summary>
    public partial class StatefulSetSpecV1
    {
        /// <summary>
        ///     serviceName is the name of the service that governs this StatefulSet. This service must exist before the StatefulSet, and is responsible for the network identity of the set. Pods get DNS/hostnames that follow the pattern: pod-specific-string.serviceName.default.svc.cluster.local where "pod-specific-string" is managed by the StatefulSet controller.
        /// </summary>
        [YamlMember(Alias = "serviceName")]
        [JsonProperty("serviceName", NullValueHandling = NullValueHandling.Include)]
        public string ServiceName { get; set; }

        /// <summary>
        ///     template is the object that describes the pod that will be created if insufficient replicas are detected. Each pod stamped out by the StatefulSet will fulfill this Template, but have a unique identity from the rest of the StatefulSet. Each pod will be named with the format &lt;statefulsetname&gt;-&lt;podindex&gt;. For example, a pod in a StatefulSet named "web" with index number "3" would be named "web-3". The only allowed template.spec.restartPolicy value is "Always".
        /// </summary>
        [YamlMember(Alias = "template")]
        [JsonProperty("template", NullValueHandling = NullValueHandling.Include)]
        public PodTemplateSpecV1 Template { get; set; }

        /// <summary>
        ///     selector is a label query over pods that should match the replica count. It must match the pod template's labels. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Include)]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     Minimum number of seconds for which a newly created pod should be ready without any of its container crashing for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready)
        /// </summary>
        [YamlMember(Alias = "minReadySeconds")]
        [JsonProperty("minReadySeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinReadySeconds { get; set; }

        /// <summary>
        ///     ordinals controls the numbering of replica indices in a StatefulSet. The default ordinals behavior assigns a "0" index to the first replica and increments the index by one for each additional replica requested.
        /// </summary>
        [YamlMember(Alias = "ordinals")]
        [JsonProperty("ordinals", NullValueHandling = NullValueHandling.Ignore)]
        public StatefulSetOrdinalsV1 Ordinals { get; set; }

        /// <summary>
        ///     replicas is the desired number of replicas of the given Template. These are replicas in the sense that they are instantiations of the same Template, but individual replicas also have a consistent identity. If unspecified, defaults to 1.
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? Replicas { get; set; }

        /// <summary>
        ///     volumeClaimTemplates is a list of claims that pods are allowed to reference. The StatefulSet controller is responsible for mapping network identities to claims in a way that maintains the identity of a pod. Every claim in this list must have at least one matching (by name) volumeMount in one container in the template. A claim in this list takes precedence over any volumes in the template, with the same name.
        /// </summary>
        [YamlMember(Alias = "volumeClaimTemplates")]
        [JsonProperty("volumeClaimTemplates", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PersistentVolumeClaimV1> VolumeClaimTemplates { get; } = new List<PersistentVolumeClaimV1>();

        /// <summary>
        ///     Determine whether the <see cref="VolumeClaimTemplates"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumeClaimTemplates() => VolumeClaimTemplates.Count > 0;

        /// <summary>
        ///     revisionHistoryLimit is the maximum number of revisions that will be maintained in the StatefulSet's revision history. The revision history consists of all revisions not represented by a currently applied StatefulSetSpec version. The default value is 10.
        /// </summary>
        [YamlMember(Alias = "revisionHistoryLimit")]
        [JsonProperty("revisionHistoryLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int? RevisionHistoryLimit { get; set; }

        /// <summary>
        ///     persistentVolumeClaimRetentionPolicy describes the lifecycle of persistent volume claims created from volumeClaimTemplates. By default, all persistent volume claims are created as needed and retained until manually deleted. This policy allows the lifecycle to be altered, for example by deleting persistent volume claims when their stateful set is deleted, or when their pod is scaled down. This requires the StatefulSetAutoDeletePVC feature gate to be enabled, which is beta.
        /// </summary>
        [YamlMember(Alias = "persistentVolumeClaimRetentionPolicy")]
        [JsonProperty("persistentVolumeClaimRetentionPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public StatefulSetPersistentVolumeClaimRetentionPolicyV1 PersistentVolumeClaimRetentionPolicy { get; set; }

        /// <summary>
        ///     podManagementPolicy controls how pods are created during initial scale up, when replacing pods on nodes, or when scaling down. The default policy is `OrderedReady`, where pods are created in increasing order (pod-0, then pod-1, etc) and the controller will wait until each pod is ready before continuing. When scaling down, the pods are removed in the opposite order. The alternative policy is `Parallel` which will create pods in parallel to match the desired scale without waiting, and on scale down will delete all pods at once.
        /// </summary>
        [YamlMember(Alias = "podManagementPolicy")]
        [JsonProperty("podManagementPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string PodManagementPolicy { get; set; }

        /// <summary>
        ///     updateStrategy indicates the StatefulSetUpdateStrategy that will be employed to update Pods in the StatefulSet when a revision is made to Template.
        /// </summary>
        [YamlMember(Alias = "updateStrategy")]
        [JsonProperty("updateStrategy", NullValueHandling = NullValueHandling.Ignore)]
        public StatefulSetUpdateStrategyV1 UpdateStrategy { get; set; }
    }
}
