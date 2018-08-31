using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     A StatefulSetSpec is the specification of a StatefulSet.
    /// </summary>
    public partial class StatefulSetSpecV1Beta1 : Models.StatefulSetSpecV1Beta1, ITracked
    {
        /// <summary>
        ///     serviceName is the name of the service that governs this StatefulSet. This service must exist before the StatefulSet, and is responsible for the network identity of the set. Pods get DNS/hostnames that follow the pattern: pod-specific-string.serviceName.default.svc.cluster.local where "pod-specific-string" is managed by the StatefulSet controller.
        /// </summary>
        [JsonProperty("serviceName")]
        [YamlMember(Alias = "serviceName")]
        public override string ServiceName
        {
            get
            {
                return base.ServiceName;
            }
            set
            {
                base.ServiceName = value;

                __ModifiedProperties__.Add("ServiceName");
            }
        }


        /// <summary>
        ///     template is the object that describes the pod that will be created if insufficient replicas are detected. Each pod stamped out by the StatefulSet will fulfill this Template, but have a unique identity from the rest of the StatefulSet.
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
        ///     selector is a label query over pods that should match the replica count. If empty, defaulted to labels on the pod template. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
        /// </summary>
        [JsonProperty("selector")]
        [YamlMember(Alias = "selector")]
        public override Models.LabelSelectorV1 Selector
        {
            get
            {
                return base.Selector;
            }
            set
            {
                base.Selector = value;

                __ModifiedProperties__.Add("Selector");
            }
        }


        /// <summary>
        ///     replicas is the desired number of replicas of the given Template. These are replicas in the sense that they are instantiations of the same Template, but individual replicas also have a consistent identity. If unspecified, defaults to 1.
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
        ///     volumeClaimTemplates is a list of claims that pods are allowed to reference. The StatefulSet controller is responsible for mapping network identities to claims in a way that maintains the identity of a pod. Every claim in this list must have at least one matching (by name) volumeMount in one container in the template. A claim in this list takes precedence over any volumes in the template, with the same name.
        /// </summary>
        [YamlMember(Alias = "volumeClaimTemplates")]
        [JsonProperty("volumeClaimTemplates", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.PersistentVolumeClaimV1> VolumeClaimTemplates { get; set; } = new List<Models.PersistentVolumeClaimV1>();

        /// <summary>
        ///     revisionHistoryLimit is the maximum number of revisions that will be maintained in the StatefulSet's revision history. The revision history consists of all revisions not represented by a currently applied StatefulSetSpec version. The default value is 10.
        /// </summary>
        [JsonProperty("revisionHistoryLimit")]
        [YamlMember(Alias = "revisionHistoryLimit")]
        public override int RevisionHistoryLimit
        {
            get
            {
                return base.RevisionHistoryLimit;
            }
            set
            {
                base.RevisionHistoryLimit = value;

                __ModifiedProperties__.Add("RevisionHistoryLimit");
            }
        }


        /// <summary>
        ///     podManagementPolicy controls how pods are created during initial scale up, when replacing pods on nodes, or when scaling down. The default policy is `OrderedReady`, where pods are created in increasing order (pod-0, then pod-1, etc) and the controller will wait until each pod is ready before continuing. When scaling down, the pods are removed in the opposite order. The alternative policy is `Parallel` which will create pods in parallel to match the desired scale without waiting, and on scale down will delete all pods at once.
        /// </summary>
        [JsonProperty("podManagementPolicy")]
        [YamlMember(Alias = "podManagementPolicy")]
        public override string PodManagementPolicy
        {
            get
            {
                return base.PodManagementPolicy;
            }
            set
            {
                base.PodManagementPolicy = value;

                __ModifiedProperties__.Add("PodManagementPolicy");
            }
        }


        /// <summary>
        ///     updateStrategy indicates the StatefulSetUpdateStrategy that will be employed to update Pods in the StatefulSet when a revision is made to Template.
        /// </summary>
        [JsonProperty("updateStrategy")]
        [YamlMember(Alias = "updateStrategy")]
        public override Models.StatefulSetUpdateStrategyV1Beta1 UpdateStrategy
        {
            get
            {
                return base.UpdateStrategy;
            }
            set
            {
                base.UpdateStrategy = value;

                __ModifiedProperties__.Add("UpdateStrategy");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
