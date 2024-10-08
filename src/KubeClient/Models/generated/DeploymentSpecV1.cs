using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentSpec is the specification of the desired behavior of the Deployment.
    /// </summary>
    public partial class DeploymentSpecV1
    {
        /// <summary>
        ///     Indicates that the deployment is paused.
        /// </summary>
        [YamlMember(Alias = "paused")]
        [JsonProperty("paused", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Paused { get; set; }

        /// <summary>
        ///     Template describes the pods that will be created. The only allowed template.spec.restartPolicy value is "Always".
        /// </summary>
        [YamlMember(Alias = "template")]
        [JsonProperty("template", NullValueHandling = NullValueHandling.Include)]
        public PodTemplateSpecV1 Template { get; set; }

        /// <summary>
        ///     Label selector for pods. Existing ReplicaSets whose pods are selected by this will be the ones affected by this deployment. It must match the pod template's labels.
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Include)]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     Minimum number of seconds for which a newly created pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready)
        /// </summary>
        [YamlMember(Alias = "minReadySeconds")]
        [JsonProperty("minReadySeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinReadySeconds { get; set; }

        /// <summary>
        ///     The maximum time in seconds for a deployment to make progress before it is considered to be failed. The deployment controller will continue to process failed deployments and a condition with a ProgressDeadlineExceeded reason will be surfaced in the deployment status. Note that progress will not be estimated during the time a deployment is paused. Defaults to 600s.
        /// </summary>
        [YamlMember(Alias = "progressDeadlineSeconds")]
        [JsonProperty("progressDeadlineSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int? ProgressDeadlineSeconds { get; set; }

        /// <summary>
        ///     Number of desired pods. This is a pointer to distinguish between explicit zero and not specified. Defaults to 1.
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? Replicas { get; set; }

        /// <summary>
        ///     The number of old ReplicaSets to retain to allow rollback. This is a pointer to distinguish between explicit zero and not specified. Defaults to 10.
        /// </summary>
        [YamlMember(Alias = "revisionHistoryLimit")]
        [JsonProperty("revisionHistoryLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int? RevisionHistoryLimit { get; set; }

        /// <summary>
        ///     The deployment strategy to use to replace existing pods with new ones.
        /// </summary>
        [RetainKeysStrategy]
        [YamlMember(Alias = "strategy")]
        [JsonProperty("strategy", NullValueHandling = NullValueHandling.Ignore)]
        public DeploymentStrategyV1 Strategy { get; set; }
    }
}
