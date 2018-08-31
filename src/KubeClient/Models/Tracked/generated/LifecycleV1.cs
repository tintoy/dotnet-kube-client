using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Lifecycle describes actions that the management system should take in response to container lifecycle events. For the PostStart and PreStop lifecycle handlers, management of the container blocks until the action is complete, unless the container process fails, in which case the handler is aborted.
    /// </summary>
    public partial class LifecycleV1 : Models.LifecycleV1, ITracked
    {
        /// <summary>
        ///     PreStop is called immediately before a container is terminated. The container is terminated after the handler completes. The reason for termination is passed to the handler. Regardless of the outcome of the handler, the container is eventually terminated. Other management of the container blocks until the hook completes. More info: https://kubernetes.io/docs/concepts/containers/container-lifecycle-hooks/#container-hooks
        /// </summary>
        [JsonProperty("preStop")]
        [YamlMember(Alias = "preStop")]
        public override Models.HandlerV1 PreStop
        {
            get
            {
                return base.PreStop;
            }
            set
            {
                base.PreStop = value;

                __ModifiedProperties__.Add("PreStop");
            }
        }


        /// <summary>
        ///     PostStart is called immediately after a container is created. If the handler fails, the container is terminated and restarted according to its restart policy. Other management of the container blocks until the hook completes. More info: https://kubernetes.io/docs/concepts/containers/container-lifecycle-hooks/#container-hooks
        /// </summary>
        [JsonProperty("postStart")]
        [YamlMember(Alias = "postStart")]
        public override Models.HandlerV1 PostStart
        {
            get
            {
                return base.PostStart;
            }
            set
            {
                base.PostStart = value;

                __ModifiedProperties__.Add("PostStart");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
