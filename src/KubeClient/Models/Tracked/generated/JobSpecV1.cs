using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JobSpec describes how the job execution will look like.
    /// </summary>
    public partial class JobSpecV1 : Models.JobSpecV1
    {
        /// <summary>
        ///     Describes the pod that will be created when executing a job. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
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
        ///     Specifies the maximum desired number of pods the job should run at any given time. The actual number of pods running in steady state will be less than this number when ((.spec.completions - .status.successful) &lt; .spec.parallelism), i.e. when the work left to do is less than max parallelism. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [JsonProperty("parallelism")]
        [YamlMember(Alias = "parallelism")]
        public override int Parallelism
        {
            get
            {
                return base.Parallelism;
            }
            set
            {
                base.Parallelism = value;

                __ModifiedProperties__.Add("Parallelism");
            }
        }


        /// <summary>
        ///     manualSelector controls generation of pod labels and pod selectors. Leave `manualSelector` unset unless you are certain what you are doing. When false or unset, the system pick labels unique to this job and appends those labels to the pod template.  When true, the user is responsible for picking unique labels and specifying the selector.  Failure to pick a unique label may cause this and other jobs to not function correctly.  However, You may see `manualSelector=true` in jobs that were created with the old `extensions/v1beta1` API. More info: https://git.k8s.io/community/contributors/design-proposals/selector-generation.md
        /// </summary>
        [JsonProperty("manualSelector")]
        [YamlMember(Alias = "manualSelector")]
        public override bool ManualSelector
        {
            get
            {
                return base.ManualSelector;
            }
            set
            {
                base.ManualSelector = value;

                __ModifiedProperties__.Add("ManualSelector");
            }
        }


        /// <summary>
        ///     A label query over pods that should match the pod count. Normally, the system sets this field for you. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
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
        ///     Optional duration in seconds relative to the startTime that the job may be active before the system tries to terminate it; value must be positive integer
        /// </summary>
        [JsonProperty("activeDeadlineSeconds")]
        [YamlMember(Alias = "activeDeadlineSeconds")]
        public override int? ActiveDeadlineSeconds
        {
            get
            {
                return base.ActiveDeadlineSeconds;
            }
            set
            {
                base.ActiveDeadlineSeconds = value;

                __ModifiedProperties__.Add("ActiveDeadlineSeconds");
            }
        }


        /// <summary>
        ///     Specifies the desired number of successfully finished pods the job should be run with.  Setting to nil means that the success of any pod signals the success of all pods, and allows parallelism to have any positive value.  Setting to 1 means that parallelism is limited to 1 and the success of that pod signals the success of the job. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [JsonProperty("completions")]
        [YamlMember(Alias = "completions")]
        public override int Completions
        {
            get
            {
                return base.Completions;
            }
            set
            {
                base.Completions = value;

                __ModifiedProperties__.Add("Completions");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
