using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentSpec is the specification of the desired behavior of the Deployment.
    /// </summary>
    public partial class DeploymentSpecV1Beta2
    {
        /// <summary>
        ///     Indicates that the deployment is paused.
        /// </summary>
        [JsonProperty("paused")]
        [YamlMember(Alias = "paused")]
        public bool Paused { get; set; }

        /// <summary>
        ///     Template describes the pods that will be created.
        /// </summary>
        [JsonProperty("template")]
        [YamlMember(Alias = "template")]
        public PodTemplateSpecV1 Template { get; set; }

        /// <summary>
        ///     Label selector for pods. Existing ReplicaSets whose pods are selected by this will be the ones affected by this deployment. It must match the pod template's labels.
        /// </summary>
        [JsonProperty("selector")]
        [YamlMember(Alias = "selector")]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     Minimum number of seconds for which a newly created pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready)
        /// </summary>
        [JsonProperty("minReadySeconds")]
        [YamlMember(Alias = "minReadySeconds")]
        public int MinReadySeconds { get; set; }

        /// <summary>
        ///     The maximum time in seconds for a deployment to make progress before it is considered to be failed. The deployment controller will continue to process failed deployments and a condition with a ProgressDeadlineExceeded reason will be surfaced in the deployment status. Note that progress will not be estimated during the time a deployment is paused. Defaults to 600s.
        /// </summary>
        [JsonProperty("progressDeadlineSeconds")]
        [YamlMember(Alias = "progressDeadlineSeconds")]
        public int ProgressDeadlineSeconds { get; set; }

        /// <summary>
        ///     Number of desired pods. This is a pointer to distinguish between explicit zero and not specified. Defaults to 1.
        /// </summary>
        [JsonProperty("replicas")]
        [YamlMember(Alias = "replicas")]
        public int Replicas { get; set; }

        /// <summary>
        ///     The number of old ReplicaSets to retain to allow rollback. This is a pointer to distinguish between explicit zero and not specified. Defaults to 10.
        /// </summary>
        [JsonProperty("revisionHistoryLimit")]
        [YamlMember(Alias = "revisionHistoryLimit")]
        public int RevisionHistoryLimit { get; set; }

        /// <summary>
        ///     The deployment strategy to use to replace existing pods with new ones.
        /// </summary>
        [JsonProperty("strategy")]
        [YamlMember(Alias = "strategy")]
        public DeploymentStrategyV1Beta2 Strategy { get; set; }
    }
}
