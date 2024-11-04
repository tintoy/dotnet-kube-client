using System.Collections.Generic;
using System.Collections.Immutable;

namespace KubeClient.Extensions.CustomResources.Schema
{
    /// <summary>
    ///     Well-known Kubernetes resource-schema constants.
    /// </summary>
    static class SchemaConstants
    {
        /// <summary>
        ///     The names of well-known value types in Kubernetes resource schemas.
        /// </summary>
        public static IReadOnlySet<string> ValueTypeNames = ImmutableHashSet.CreateRange([
            "bool",
            "int",
            "long",
            "double",
            "DateTime",
        ]);

        /// <summary>
        ///     The names of data types to ignore when parsing Kubernetes resource schemas.
        /// </summary>
        public static IReadOnlySet<string> IgnoreDataTypes = ImmutableHashSet.CreateRange([
            "io.k8s.apimachinery.pkg.apis.meta.v1.DeleteOptions",
            "io.k8s.apimachinery.pkg.apis.meta.v1.Time",
            "io.k8s.apimachinery.pkg.apis.meta.v1.MicroTime",

            "io.k8s.apimachinery.pkg.api.resource.Quantity",
            "io.k8s.apimachinery.pkg.util.intstr.IntOrString",

            // Present in both regular and and "extensions" groups:
            "io.k8s.api.extensions.v1beta1.Deployment",
            "io.k8s.api.extensions.v1beta1.DeploymentList",
            "io.k8s.api.extensions.v1beta1.DeploymentRollback",
            "io.k8s.api.extensions.v1beta1.NetworkPolicy",
            "io.k8s.api.extensions.v1beta1.NetworkPolicyList",
            "io.k8s.api.extensions.v1beta1.PodSecurityPolicy",
            "io.k8s.api.extensions.v1beta1.PodSecurityPolicyList",
            "io.k8s.api.extensions.v1beta1.ReplicaSet",
            "io.k8s.api.extensions.v1beta1.ReplicaSetList",
            "io.k8s.api.extensions.v1.Deployment",
            "io.k8s.api.extensions.v1.DeploymentList",
            "io.k8s.api.extensions.v1.DeploymentRollback",
            "io.k8s.api.extensions.v1.NetworkPolicy",
            "io.k8s.api.extensions.v1.NetworkPolicyList",
            "io.k8s.api.extensions.v1.PodSecurityPolicy",
            "io.k8s.api.extensions.v1.PodSecurityPolicyList",
            "io.k8s.api.extensions.v1.ReplicaSet",
            "io.k8s.api.extensions.v1.ReplicaSetList",
            "io.k8s.kubernetes.pkg.apis.apps.v1beta1.ControllerRevision",
            "io.k8s.kubernetes.pkg.apis.apps.v1beta1.ControllerRevisionList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DaemonSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DaemonSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.Deployment",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DeploymentList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.DeploymentRollback",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.NetworkPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.NetworkPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.PodSecurityPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.PodSecurityPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ReplicaSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ReplicaSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.Scale",
            "io.k8s.kubernetes.pkg.apis.apps.v1.ControllerRevision",
            "io.k8s.kubernetes.pkg.apis.apps.v1.ControllerRevisionList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DaemonSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DaemonSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.Deployment",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DeploymentList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.DeploymentRollback",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.NetworkPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.NetworkPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.PodSecurityPolicy",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.PodSecurityPolicyList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.ReplicaSet",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.ReplicaSetList",
            "io.k8s.kubernetes.pkg.apis.extensions.v1.Scale",

            // Special case for EventV1
            "io.k8s.api.events.v1.Event",
            "io.k8s.api.events.v1.EventList",

            // Hand-coded:
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ThirdPartyResource",
            "io.k8s.kubernetes.pkg.apis.extensions.v1beta1.ThirdPartyResourceList",
        ]);
    }
}