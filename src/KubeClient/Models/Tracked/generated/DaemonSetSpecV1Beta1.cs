using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DaemonSetSpec is the specification of a daemon set.
    /// </summary>
    public partial class DaemonSetSpecV1Beta1 : Models.DaemonSetSpecV1Beta1
    {
        /// <summary>
        ///     An object that describes the pod that will be created. The DaemonSet will create exactly one copy of this pod on every node that matches the template's node selector (or on every node if no node selector is specified). More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller#pod-template
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
        ///     DEPRECATED. A sequence number representing a specific generation of the template. Populated by the system. It can be set only during the creation.
        /// </summary>
        [JsonProperty("templateGeneration")]
        [YamlMember(Alias = "templateGeneration")]
        public override int TemplateGeneration
        {
            get
            {
                return base.TemplateGeneration;
            }
            set
            {
                base.TemplateGeneration = value;

                __ModifiedProperties__.Add("TemplateGeneration");
            }
        }


        /// <summary>
        ///     A label query over pods that are managed by the daemon set. Must match in order to be controlled. If empty, defaulted to labels on Pod template. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
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
        ///     The minimum number of seconds for which a newly created DaemonSet pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready).
        /// </summary>
        [JsonProperty("minReadySeconds")]
        [YamlMember(Alias = "minReadySeconds")]
        public override int MinReadySeconds
        {
            get
            {
                return base.MinReadySeconds;
            }
            set
            {
                base.MinReadySeconds = value;

                __ModifiedProperties__.Add("MinReadySeconds");
            }
        }


        /// <summary>
        ///     The number of old history to retain to allow rollback. This is a pointer to distinguish between explicit zero and not specified. Defaults to 10.
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
        ///     An update strategy to replace existing DaemonSet pods with new pods.
        /// </summary>
        [JsonProperty("updateStrategy")]
        [YamlMember(Alias = "updateStrategy")]
        public override Models.DaemonSetUpdateStrategyV1Beta1 UpdateStrategy
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
