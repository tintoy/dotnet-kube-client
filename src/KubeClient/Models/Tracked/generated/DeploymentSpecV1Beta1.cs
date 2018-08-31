using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DeploymentSpec is the specification of the desired behavior of the Deployment.
    /// </summary>
    public partial class DeploymentSpecV1Beta1 : Models.DeploymentSpecV1Beta1, ITracked
    {
        /// <summary>
        ///     Indicates that the deployment is paused and will not be processed by the deployment controller.
        /// </summary>
        [JsonProperty("paused")]
        [YamlMember(Alias = "paused")]
        public override bool Paused
        {
            get
            {
                return base.Paused;
            }
            set
            {
                base.Paused = value;

                __ModifiedProperties__.Add("Paused");
            }
        }


        /// <summary>
        ///     Template describes the pods that will be created.
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
        ///     The config this deployment is rolling back to. Will be cleared after rollback is done.
        /// </summary>
        [JsonProperty("rollbackTo")]
        [YamlMember(Alias = "rollbackTo")]
        public override Models.RollbackConfigV1Beta1 RollbackTo
        {
            get
            {
                return base.RollbackTo;
            }
            set
            {
                base.RollbackTo = value;

                __ModifiedProperties__.Add("RollbackTo");
            }
        }


        /// <summary>
        ///     Label selector for pods. Existing ReplicaSets whose pods are selected by this will be the ones affected by this deployment.
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
        ///     Minimum number of seconds for which a newly created pod should be ready without any of its container crashing, for it to be considered available. Defaults to 0 (pod will be considered available as soon as it is ready)
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
        ///     The maximum time in seconds for a deployment to make progress before it is considered to be failed. The deployment controller will continue to process failed deployments and a condition with a ProgressDeadlineExceeded reason will be surfaced in the deployment status. Once autoRollback is implemented, the deployment controller will automatically rollback failed deployments. Note that progress will not be estimated during the time a deployment is paused. This is not set by default.
        /// </summary>
        [JsonProperty("progressDeadlineSeconds")]
        [YamlMember(Alias = "progressDeadlineSeconds")]
        public override int ProgressDeadlineSeconds
        {
            get
            {
                return base.ProgressDeadlineSeconds;
            }
            set
            {
                base.ProgressDeadlineSeconds = value;

                __ModifiedProperties__.Add("ProgressDeadlineSeconds");
            }
        }


        /// <summary>
        ///     Number of desired pods. This is a pointer to distinguish between explicit zero and not specified. Defaults to 1.
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
        ///     The number of old ReplicaSets to retain to allow rollback. This is a pointer to distinguish between explicit zero and not specified.
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
        ///     The deployment strategy to use to replace existing pods with new ones.
        /// </summary>
        [JsonProperty("strategy")]
        [YamlMember(Alias = "strategy")]
        public override Models.DeploymentStrategyV1Beta1 Strategy
        {
            get
            {
                return base.Strategy;
            }
            set
            {
                base.Strategy = value;

                __ModifiedProperties__.Add("Strategy");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
