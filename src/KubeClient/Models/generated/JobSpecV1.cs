using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JobSpec describes how the job execution will look like.
    /// </summary>
    public partial class JobSpecV1
    {
        /// <summary>
        ///     Describes the pod that will be created when executing a job. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [YamlMember(Alias = "template")]
        [JsonProperty("template", NullValueHandling = NullValueHandling.Include)]
        public PodTemplateSpecV1 Template { get; set; }

        /// <summary>
        ///     Specifies the maximum desired number of pods the job should run at any given time. The actual number of pods running in steady state will be less than this number when ((.spec.completions - .status.successful) &lt; .spec.parallelism), i.e. when the work left to do is less than max parallelism. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [YamlMember(Alias = "parallelism")]
        [JsonProperty("parallelism", NullValueHandling = NullValueHandling.Ignore)]
        public int? Parallelism { get; set; }

        /// <summary>
        ///     manualSelector controls generation of pod labels and pod selectors. Leave `manualSelector` unset unless you are certain what you are doing. When false or unset, the system pick labels unique to this job and appends those labels to the pod template.  When true, the user is responsible for picking unique labels and specifying the selector.  Failure to pick a unique label may cause this and other jobs to not function correctly.  However, You may see `manualSelector=true` in jobs that were created with the old `extensions/v1beta1` API. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/#specifying-your-own-pod-selector
        /// </summary>
        [YamlMember(Alias = "manualSelector")]
        [JsonProperty("manualSelector", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ManualSelector { get; set; }

        /// <summary>
        ///     A label query over pods that should match the pod count. Normally, the system sets this field for you. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/#label-selectors
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     Specifies the duration in seconds relative to the startTime that the job may be active before the system tries to terminate it; value must be positive integer
        /// </summary>
        [YamlMember(Alias = "activeDeadlineSeconds")]
        [JsonProperty("activeDeadlineSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? ActiveDeadlineSeconds { get; set; }

        /// <summary>
        ///     Specifies the desired number of successfully finished pods the job should be run with.  Setting to nil means that the success of any pod signals the success of all pods, and allows parallelism to have any positive value.  Setting to 1 means that parallelism is limited to 1 and the success of that pod signals the success of the job. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [YamlMember(Alias = "completions")]
        [JsonProperty("completions", NullValueHandling = NullValueHandling.Ignore)]
        public int? Completions { get; set; }

        /// <summary>
        ///     Specifies the number of retries before marking this job failed. Defaults to 6
        /// </summary>
        [YamlMember(Alias = "backoffLimit")]
        [JsonProperty("backoffLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int? BackoffLimit { get; set; }
    }
}
