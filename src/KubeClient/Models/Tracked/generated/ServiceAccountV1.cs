using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ServiceAccount binds together: * a name, understood by users, and perhaps by peripheral systems, for an identity * a principal that can be authenticated and authorized * a set of secrets
    /// </summary>
    [KubeObject("ServiceAccount", "v1")]
    public partial class ServiceAccountV1 : Models.ServiceAccountV1, ITracked
    {
        /// <summary>
        ///     AutomountServiceAccountToken indicates whether pods running as this service account should have an API token automatically mounted. Can be overridden at the pod level.
        /// </summary>
        [JsonProperty("automountServiceAccountToken")]
        [YamlMember(Alias = "automountServiceAccountToken")]
        public override bool AutomountServiceAccountToken
        {
            get
            {
                return base.AutomountServiceAccountToken;
            }
            set
            {
                base.AutomountServiceAccountToken = value;

                __ModifiedProperties__.Add("AutomountServiceAccountToken");
            }
        }


        /// <summary>
        ///     ImagePullSecrets is a list of references to secrets in the same namespace to use for pulling any images in pods that reference this ServiceAccount. ImagePullSecrets are distinct from Secrets because Secrets can be mounted in the pod, but ImagePullSecrets are only accessed by the kubelet. More info: https://kubernetes.io/docs/concepts/containers/images/#specifying-imagepullsecrets-on-a-pod
        /// </summary>
        [YamlMember(Alias = "imagePullSecrets")]
        [JsonProperty("imagePullSecrets", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.LocalObjectReferenceV1> ImagePullSecrets { get; set; } = new List<Models.LocalObjectReferenceV1>();

        /// <summary>
        ///     Secrets is the list of secrets allowed to be used by pods running using this ServiceAccount. More info: https://kubernetes.io/docs/concepts/configuration/secret
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "secrets")]
        [JsonProperty("secrets", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.ObjectReferenceV1> Secrets { get; set; } = new List<Models.ObjectReferenceV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
