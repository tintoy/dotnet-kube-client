using System;

namespace KubeClient
{
    using ResourceClients;

    /// <summary>
    ///     Extension methods for creating Kubernetes resource clients for a <see cref="KubeApiClient"/>.
    /// </summary>
    public static class ClientFactoryExtensions
    {
        /// <summary>
        ///     Get the Kubernetes Namespaces (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static INamespaceClientV1 NamespacesV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<INamespaceClientV1>(
                client => new NamespaceClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes ConfigMaps (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IConfigMapClientV1 ConfigMapsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IConfigMapClientV1>(
                client => new ConfigMapClientV1(client)
            );
        }
        /// <summary>
        ///     Get the Kubernetes DaemonSets (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IDaemonSetClientV1 DaemonSetsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IDaemonSetClientV1>(
                client => new DaemonSetClientV1(client)
            );
        }
        /// <summary>
        ///     Get the Kubernetes Deployments (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IDeploymentClientV1 DeploymentsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IDeploymentClientV1>(
                client => new DeploymentClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Deployments (v1beta1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IDeploymentClientV1Beta1 DeploymentsV1Beta1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IDeploymentClientV1Beta1>(
                client => new DeploymentClientV1Beta1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Jobs (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IJobClientV1 JobsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IJobClientV1>(
                client => new JobClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Nodes (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static INodeClientV1 NodesV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<INodeClientV1>(
                client => new NodeClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Events (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IEventClientV1 EventsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IEventClientV1>(
                client => new EventClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Ingresses (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IIngressClientV1Beta1 IngressesV1Beta1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IIngressClientV1Beta1>(
                client => new IngressClientV1Beta1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes PersistentVolumes (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IPersistentVolumeClientV1 PersistentVolumesV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IPersistentVolumeClientV1>(
                client => new PersistentVolumeClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes PersistentVolumeClaims (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IPersistentVolumeClaimClientV1 PersistentVolumeClaimsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IPersistentVolumeClaimClientV1>(
                client => new PersistentVolumeClaimClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Pods (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IPodClientV1 PodsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IPodClientV1>(
                client => new PodClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Roles (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IRoleClientV1 RolesV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IRoleClientV1>(
                client => new RoleClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Secrets (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static ISecretClientV1 SecretsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<ISecretClientV1>(
                client => new SecretClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes Services (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IServiceClientV1 ServicesV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IServiceClientV1>(
                client => new ServiceClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes ReplicationControllers (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IReplicationControllerClientV1 ReplicationControllersV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IReplicationControllerClientV1>(
                client => new ReplicationControllerClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes ReplicaSets (v1beta1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IReplicaSetClientV1 ReplicaSetsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IReplicaSetClientV1>(
                client => new ReplicaSetClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes APIGroups (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IAPIGroupClientV1 APIGroupsV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IAPIGroupClientV1>(
                client => new APIGroupClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes APIResources (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IAPIResourceClientV1 APIResourcesV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IAPIResourceClientV1>(
                client => new APIResourceClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes NetworkPolicy (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static INetworkPolicyClientV1 NetworkPolicyV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<INetworkPolicyClientV1>(
                client => new NetworkPolicyClientV1(client)
            );
        }

        /// <summary>
        ///     Get the Kubernetes StatefulSets (v1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IStatefulSetClientV1 StatefulSetV1(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IStatefulSetClientV1>(
                client => new StatefulSetClientV1(client)
            );
        }

        /// <summary>
        ///     Get a client for dynamic access to Kubernetes resource APIs.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static IDynamicResourceClient Dynamic(this IKubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            return kubeClient.ResourceClient<IDynamicResourceClient>(
                client => new DynamicResourceClient(client)
            );
        }
    }
}
