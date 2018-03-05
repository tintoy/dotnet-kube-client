using HTTPlease;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Deployments (v1beta2) API.
    /// </summary>
    public class DeploymentClientV1Beta1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="DeploymentClientV1Beta1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public DeploymentClientV1Beta1(KubeApiClient client)
            : base(client)
        {
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
        ///     The Deployments, as a list of <see cref="DeploymentV1Beta1"/>s.
        /// </returns>
        public async Task<List<DeploymentV1Beta1>> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            DeploymentListV1Beta1 matchingDeployments =
                await Http.GetAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                        LabelSelector = labelSelector
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<DeploymentListV1Beta1, StatusV1>();

            return matchingDeployments.Items;
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
        public IObservable<ResourceEventV1<DeploymentV1Beta1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<DeploymentV1Beta1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                })
            );
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
        ///     A <see cref="DeploymentV1Beta1"/> representing the current state for the Deployment, or <c>null</c> if no Deployment was found with the specified name and namespace.
        /// </returns>
        public async Task<DeploymentV1Beta1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<DeploymentV1Beta1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="DeploymentV1Beta1"/>.
        /// </summary>
        /// <param name="newDeployment">
        ///     A <see cref="DeploymentV1Beta1"/> representing the Deployment to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1Beta1"/> representing the current state for the newly-created Deployment.
        /// </returns>
        public async Task<DeploymentV1Beta1> Create(DeploymentV1Beta1 newDeployment, CancellationToken cancellationToken = default)
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
                .ReadContentAsAsync<DeploymentV1Beta1, StatusV1>();
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="DeploymentV1Beta1"/>.
        /// </summary>
        /// <param name="deployment">
        ///     A <see cref="DeploymentV1Beta1"/> representing the Deployment to update.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1Beta1"/> representing the current state for the newly-created Deployment.
        /// </returns>
        public async Task<DeploymentV1Beta1> Update(DeploymentV1Beta1 deployment, CancellationToken cancellationToken = default)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            if (String.IsNullOrWhiteSpace(deployment.Metadata?.Name))
                throw new ArgumentException("Cannot update Deployment without a value for its Metadata.Name property.", nameof(deployment));

            if (String.IsNullOrWhiteSpace(deployment.Metadata?.Namespace))
                throw new ArgumentException("Cannot update Deployment without a value for its Metadata.Namespace property.", nameof(deployment));
            
            return await Http
                .PatchAsync(
                    Requests.ByName.WithTemplateParameters(new
                    {
                        Name = deployment.Metadata.Name,
                        Namespace = deployment.Metadata.Namespace
                    }),
                    patchBody: deployment,
                    mediaType: PatchMediaType,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<DeploymentV1Beta1, StatusV1>();
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
        ///     A <see cref="DeletePropagationPolicy"/> indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1Beta1"/> representing the deployment's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public async Task<KubeObjectV1> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy propagationPolicy = DeletePropagationPolicy.Background, CancellationToken cancellationToken = default)
        {
            var request = Http.DeleteAsJsonAsync(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                deleteBody: new DeleteOptionsV1
                {
                    PropagationPolicy = propagationPolicy
                },
                cancellationToken: cancellationToken
            );

            if (propagationPolicy == DeletePropagationPolicy.Foreground)
                return await request.ReadContentAsObjectV1Async<DeploymentV1Beta1>(HttpStatusCode.OK);
            
            return await request.ReadContentAsObjectV1Async<StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the Deployment (v1beta2) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Deployment (v1beta2) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("apis/apps/v1beta1/namespaces/{Namespace}/deployments?labelSelector={LabelSelector?}&watch={Watch?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name Deployment (v1beta2) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("apis/apps/v1beta1/namespaces/{Namespace}/deployments/{Name}", SerializerSettings);
        }
    }
}
