using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterTrustBundle is a cluster-scoped container for X.509 trust anchors (root certificates).
    ///     
    ///     ClusterTrustBundle objects are considered to be readable by any authenticated user in the cluster, because they can be mounted by pods using the `clusterTrustBundle` projection.  All service accounts have read access to ClusterTrustBundles by default.  Users who only have namespace-level access to a cluster can read ClusterTrustBundles by impersonating a serviceaccount that they have access to.
    ///     
    ///     It can be optionally associated with a particular assigner, in which case it contains one valid set of trust anchors for that signer. Signers may have multiple associated ClusterTrustBundles; each is an independent set of trust anchors for that signer. Admission control is used to enforce that only users with permissions on the signer can create or modify the corresponding bundle.
    /// </summary>
    [KubeObject("ClusterTrustBundle", "certificates.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/certificates.k8s.io/v1alpha1/clustertrustbundles")]
    [KubeApi(KubeAction.Create, "apis/certificates.k8s.io/v1alpha1/clustertrustbundles")]
    [KubeApi(KubeAction.Get, "apis/certificates.k8s.io/v1alpha1/clustertrustbundles/{name}")]
    [KubeApi(KubeAction.Patch, "apis/certificates.k8s.io/v1alpha1/clustertrustbundles/{name}")]
    [KubeApi(KubeAction.Delete, "apis/certificates.k8s.io/v1alpha1/clustertrustbundles/{name}")]
    [KubeApi(KubeAction.Update, "apis/certificates.k8s.io/v1alpha1/clustertrustbundles/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/certificates.k8s.io/v1alpha1/watch/clustertrustbundles")]
    [KubeApi(KubeAction.DeleteCollection, "apis/certificates.k8s.io/v1alpha1/clustertrustbundles")]
    [KubeApi(KubeAction.Watch, "apis/certificates.k8s.io/v1alpha1/watch/clustertrustbundles/{name}")]
    public partial class ClusterTrustBundleV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     spec contains the signer (if any) and trust anchors.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public ClusterTrustBundleSpecV1Alpha1 Spec { get; set; }
    }
}
