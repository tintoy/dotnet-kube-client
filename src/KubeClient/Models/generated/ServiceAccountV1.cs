using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceAccount binds together: * a name, understood by users, and perhaps by peripheral systems, for an identity * a principal that can be authenticated and authorized * a set of secrets
    /// </summary>
    [KubeObject("ServiceAccount", "v1")]
    public partial class ServiceAccountV1 : KubeResourceV1
    {
        /// <summary>
        ///     AutomountServiceAccountToken indicates whether pods running as this service account should have an API token automatically mounted. Can be overridden at the pod level.
        /// </summary>
        [JsonProperty("automountServiceAccountToken")]
        [YamlMember(Alias = "automountServiceAccountToken")]
        public bool AutomountServiceAccountToken { get; set; }

        /// <summary>
        ///     ImagePullSecrets is a list of references to secrets in the same namespace to use for pulling any images in pods that reference this ServiceAccount. ImagePullSecrets are distinct from Secrets because Secrets can be mounted in the pod, but ImagePullSecrets are only accessed by the kubelet. More info: https://kubernetes.io/docs/concepts/containers/images/#specifying-imagepullsecrets-on-a-pod
        /// </summary>
        [YamlMember(Alias = "imagePullSecrets")]
        [JsonProperty("imagePullSecrets", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocalObjectReferenceV1> ImagePullSecrets { get; set; } = new List<LocalObjectReferenceV1>();

        /// <summary>
        ///     Secrets is the list of secrets allowed to be used by pods running using this ServiceAccount. More info: https://kubernetes.io/docs/concepts/configuration/secret
        /// </summary>
        [YamlMember(Alias = "secrets")]
        [StrategicPatchMerge(Key = "name")]
        [JsonProperty("secrets", NullValueHandling = NullValueHandling.Ignore)]
        public List<ObjectReferenceV1> Secrets { get; set; } = new List<ObjectReferenceV1>();
    }
}
