using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Projection that may be projected along with other supported volume types. Exactly one of these fields must be set.
    /// </summary>
    public partial class VolumeProjectionV1
    {
        /// <summary>
        ///     downwardAPI information about the downwardAPI data to project
        /// </summary>
        [YamlMember(Alias = "downwardAPI")]
        [JsonProperty("downwardAPI", NullValueHandling = NullValueHandling.Ignore)]
        public DownwardAPIProjectionV1 DownwardAPI { get; set; }

        /// <summary>
        ///     ClusterTrustBundle allows a pod to access the `.spec.trustBundle` field of ClusterTrustBundle objects in an auto-updating file.
        ///     
        ///     Alpha, gated by the ClusterTrustBundleProjection feature gate.
        ///     
        ///     ClusterTrustBundle objects can either be selected by name, or by the combination of signer name and a label selector.
        ///     
        ///     Kubelet performs aggressive normalization of the PEM contents written into the pod filesystem.  Esoteric PEM features such as inter-block comments and block headers are stripped.  Certificates are deduplicated. The ordering of certificates within the file is arbitrary, and Kubelet may change the order over time.
        /// </summary>
        [YamlMember(Alias = "clusterTrustBundle")]
        [JsonProperty("clusterTrustBundle", NullValueHandling = NullValueHandling.Ignore)]
        public ClusterTrustBundleProjectionV1 ClusterTrustBundle { get; set; }

        /// <summary>
        ///     serviceAccountToken is information about the serviceAccountToken data to project
        /// </summary>
        [YamlMember(Alias = "serviceAccountToken")]
        [JsonProperty("serviceAccountToken", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceAccountTokenProjectionV1 ServiceAccountToken { get; set; }

        /// <summary>
        ///     configMap information about the configMap data to project
        /// </summary>
        [YamlMember(Alias = "configMap")]
        [JsonProperty("configMap", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigMapProjectionV1 ConfigMap { get; set; }

        /// <summary>
        ///     secret information about the secret data to project
        /// </summary>
        [YamlMember(Alias = "secret")]
        [JsonProperty("secret", NullValueHandling = NullValueHandling.Ignore)]
        public SecretProjectionV1 Secret { get; set; }
    }
}
