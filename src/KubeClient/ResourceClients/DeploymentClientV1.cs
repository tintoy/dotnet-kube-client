using HTTPlease;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Deployments (v1) API.
    /// </summary>
    public class DeploymentClientV1
        : KubeResourceClient, IDeploymentClientV1
    {
        /// <summary>
        ///     Create a new <see cref="DeploymentClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public DeploymentClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Deployment with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Deployment to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the current state for the Deployment, or <c>null</c> if no Deployment was found with the specified name and namespace.
        /// </returns>
        public async Task<DeploymentV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<DeploymentV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Deployments in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Deployments.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentListV1"/> containing the Deployments.
        /// </returns>
        public async Task<DeploymentListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<DeploymentListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to Deployments.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Deployments.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<DeploymentV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<DeploymentV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/Deployments with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="DeploymentV1"/>.
        /// </summary>
        /// <param name="newDeployment">
        ///     A <see cref="DeploymentV1"/> representing the Deployment to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the current state for the newly-created Deployment.
        /// </returns>
        public async Task<DeploymentV1> Create(DeploymentV1 newDeployment, CancellationToken cancellationToken = default)
        {
            if (newDeployment == null)
                throw new ArgumentNullException(nameof(newDeployment));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newDeployment?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newDeployment,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<DeploymentV1>("create v1/Deployment resource");
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="DeploymentV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Deployment.
        /// </param>
        /// <param name="patchAction">
        ///     A delegate that customises the patch operation.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the current state for the updated Deployment.
        /// </returns>
        public async Task<DeploymentV1> Update(string name, Action<JsonPatchDocument<DeploymentV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (patchAction == null)
                throw new ArgumentNullException(nameof(patchAction));
            
            return await PatchResource(patchAction,
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken
            );
        }

        /// <summary>
        ///     Request deletion of the specified Deployment.
        /// </summary>
        /// <param name="name">
        ///     The name of the Deployment to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the deployment's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<DeploymentV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<DeploymentV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the Deployments (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Deployment (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("apis/apps/v1/namespaces/{Namespace}/deployments?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name Deployment (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("apis/apps/v1/namespaces/{Namespace}/deployments/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes Deployments (v1) API.
    /// </summary>
    public interface IDeploymentClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the Deployment with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Deployment to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the current state for the Deployment, or <c>null</c> if no Deployment was found with the specified name and namespace.
        /// </returns>
        Task<DeploymentV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all Deployments in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Deployments.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentListV1"/> containing the Deployments.
        /// </returns>
        Task<DeploymentListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to Deployments.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Deployments.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<DeploymentV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="DeploymentV1"/>.
        /// </summary>
        /// <param name="newDeployment">
        ///     A <see cref="DeploymentV1"/> representing the Deployment to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the current state for the newly-created Deployment.
        /// </returns>
        Task<DeploymentV1> Create(DeploymentV1 newDeployment, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="DeploymentV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Deployment.
        /// </param>
        /// <param name="patchAction">
        ///     A delegate that customises the patch operation.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the current state for the updated Deployment.
        /// </returns>
        Task<DeploymentV1> Update(string name, Action<JsonPatchDocument<DeploymentV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified Deployment.
        /// </summary>
        /// <param name="name">
        ///     The name of the Deployment to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the deployment's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<DeploymentV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
