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
    [KubeApi("api/v1/serviceaccounts", KubeAction.List)]
    [KubeApi("api/v1/watch/serviceaccounts", KubeAction.WatchList)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/serviceaccounts", KubeAction.WatchList)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/serviceaccounts/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/namespaces/{namespace}/serviceaccounts", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("api/v1/namespaces/{namespace}/serviceaccounts/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
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
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "secrets")]
        [JsonProperty("secrets", NullValueHandling = NullValueHandling.Ignore)]
        public List<ObjectReferenceV1> Secrets { get; set; } = new List<ObjectReferenceV1>();
    }
}
