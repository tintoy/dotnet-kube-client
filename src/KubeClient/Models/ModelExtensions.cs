using System;
using System.Collections.Generic;
using System.Linq;

namespace KubeClient.Models
{
    /// <summary>
    ///     Extension methods for Kubernetes API models.
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        ///     Get the host name and port corresponding to the Service.
        /// </summary>
        /// <param name="service">
        ///     The Kubernetes <see cref="ServiceV1"/>.
        /// </param>
        /// <param name="portName">
        ///     The name of the port to use.
        /// </param>
        /// <returns>
        ///     The host name and port.
        /// </returns>
        public static (string hostName, int? port) GetHostAndPort(this ServiceV1 service, string portName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (String.IsNullOrWhiteSpace(portName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'portName'.", nameof(portName));

            string hostName = $"{service.Metadata.Name}.{service.Metadata.Namespace}.svc.cluster.local";
            
            ServicePortV1 targetPort = service.Spec.Ports.FirstOrDefault(
                servicePort => servicePort.Name == portName
            );

            int? port = null;
            if (targetPort != null)
            {
                if (service.Spec.Type == "NodePort")
                    port = targetPort.NodePort;
                else
                    port = targetPort.Port;
            }
            
            return (hostName, port);
        }

        /// <summary>
        ///     Determine whether a Deployment owns a ReplicaSet.
        /// </summary>
        /// <param name="replicaSet">
        ///     The ReplicaSet to examine.
        /// </param>
        /// <param name="deployment">
        ///     The Deployment to examine.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the ReplicaSet has an owner-reference to the Deployment; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOwnedBy(this ReplicaSetV1 replicaSet, DeploymentV1 deployment)
        {
            if (replicaSet == null)
                throw new ArgumentNullException(nameof(replicaSet));

            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            if (replicaSet.Metadata == null)
                throw new ArgumentException("Cannot evaluate ownership of the supplied ReplicaSet because its Metadata is null.", nameof(replicaSet));

            if (deployment.Metadata == null)
                throw new ArgumentException("Cannot evaluate ownership of the supplied ReplicaSet because the supplied Deployment's Metadata is null.", nameof(replicaSet));
            
            bool isOwnedBy = replicaSet.Metadata.OwnerReferences.Any(ownerReference =>
                ownerReference.Kind == deployment.Kind
                &&
                ownerReference.ApiVersion == deployment.ApiVersion
                &&
                ownerReference.Name == deployment.Metadata.Name
            );
            
            return isOwnedBy;
        }

        /// <summary>
        ///     Determine the revision (represented by the "deployment.kubernetes.io/revision" annotation) of the Deployment.
        /// </summary>
        /// <param name="deployment">
        ///     The <see cref="DeploymentV1"/> model.
        /// </param>
        /// <returns>
        ///     The revision, if present; <c>null</c>, if the annotation is absent or not a number.
        /// </returns>
        public static int? GetRevision(this DeploymentV1 deployment)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            if (deployment.Metadata == null)
                return null;
            
            string rawRevision;
            if (!deployment.Metadata.Annotations.TryGetValue(K8sAnnotations.Deployment.Revision, out rawRevision))
                return null;

            int revision;
            if (!Int32.TryParse(rawRevision, out revision))
                return null;

            return revision;
        }

        /// <summary>
        ///     Determine the revision (represented by the "deployment.kubernetes.io/revision" annotation) of the Deployment represented by the ReplicaSet.
        /// </summary>
        /// <param name="deployment">
        ///     The <see cref="ReplicaSetV1"/> model.
        /// </param>
        /// <returns>
        ///     The revision, if present; <c>null</c>, if the annotation is absent or not a number.
        /// </returns>
        public static int? GetRevision(this ReplicaSetV1 deployment)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            if (deployment.Metadata == null)
                return null;
            
            string rawRevision;
            if (!deployment.Metadata.Annotations.TryGetValue(K8sAnnotations.Deployment.Revision, out rawRevision))
                return null;

            int revision;
            if (!Int32.TryParse(rawRevision, out revision))
                return null;

            return revision;
        }

        /// <summary>
        ///     Get the composite label selector (if any) associated with the specified Deployment.
        /// </summary>
        /// <param name="deployment">
        ///     A <see cref="DeploymentV1"/> representing the Deployment's current state.
        /// </param>
        /// <returns>
        ///     The composite label selector (e.g. "key1=value1,key2=value2"), or <c>null</c> if the Deployment doesn't specify any label selectors.
        /// </returns>
        public static string GetLabelSelector(this DeploymentV1 deployment)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));
            
            return deployment.Spec?.Selector?.GetLabelSelector();
        }

        /// <summary>
        ///     Get the composite label selector (if any) associated with the specified Deployment.
        /// </summary>
        /// <param name="labelSelector">
        ///     A <see cref="DeploymentV1"/> representing the Deployment's current state.
        /// </param>
        /// <returns>
        ///     The composite label selector (e.g. "key1=value1,key2=value2"), or <c>null</c> if <paramref name="labelSelector" />'s <see cref="LabelSelectorV1.MatchLabels"/> is empty.
        /// </returns>
        public static string GetLabelSelector(this LabelSelectorV1 labelSelector)
        {
            if (labelSelector == null)
                throw new ArgumentNullException(nameof(labelSelector));

            if (labelSelector.MatchLabels.Count == 0)
                return null;

            return String.Join(",", labelSelector.MatchLabels.Select(
                selector => $"{selector.Key}={selector.Value}"
            ));
        }
    }
}
