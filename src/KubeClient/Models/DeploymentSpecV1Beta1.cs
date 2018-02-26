using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentSpec is the specification of the desired behavior of the Deployment.
    /// </summary>
    [KubeObject("DeploymentSpec", "v1beta1")]
    public class DeploymentSpecV1Beta1
    {
        /// <summary>
        ///     Indicates that the deployment is paused and will not be processed by the deployment controller.
        /// </summary>
        [JsonProperty("paused")]
        public bool Paused { get; set; }

        /// <summary>
        ///     Template describes the pods that will be created.
        /// </summary>
        [JsonProperty("template")]
        public PodTemplateSpecV1 Template { get; set; }

        /// <summary>
        ///     The config this deployment is rolling back to. Will be cleared after rollback is done.
        /// </summary>
        [JsonProperty("rollbackTo")]
        public RollbackConfigV1Beta1 RollbackTo { get; set; }

        /// <summary>
        ///     Label selector for pods. Existing ReplicaSets whose pods are selected by this will be the ones affected by this deployment.
        /// </summary>
        [JsonProperty("selector")]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     Minimum number of seconds for which a newly created pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready)
        /// </summary>
        [JsonProperty("minReadySeconds")]
        public int MinReadySeconds { get; set; }

        /// <summary>
        ///     The maximum time in seconds for a deployment to make progress before it is considered to be failed. The deployment controller will continue to process failed deployments and a condition with a ProgressDeadlineExceeded reason will be surfaced in the deployment status. Once autoRollback is implemented, the deployment controller will automatically rollback failed deployments. Note that progress will not be estimated during the time a deployment is paused. This is not set by default.
        /// </summary>
        [JsonProperty("progressDeadlineSeconds")]
        public int ProgressDeadlineSeconds { get; set; }

        /// <summary>
        ///     Number of desired pods. This is a pointer to distinguish between explicit zero and not specified. Defaults to 1.
        /// </summary>
        [JsonProperty("replicas")]
        public int Replicas { get; set; }

        /// <summary>
        ///     The number of old ReplicaSets to retain to allow rollback. This is a pointer to distinguish between explicit zero and not specified.
        /// </summary>
        [JsonProperty("revisionHistoryLimit")]
        public int RevisionHistoryLimit { get; set; }

        /// <summary>
        ///     The deployment strategy to use to replace existing pods with new ones.
        /// </summary>
        [JsonProperty("strategy")]
        public DeploymentStrategyV1Beta1 Strategy { get; set; }
    }
}
