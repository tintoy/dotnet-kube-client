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
    ///     A client for the Kubernetes Ingress (v1beta1) API.
    /// </summary>
    public class IngressClientV1Beta1
        : KubeResourceClient, IIngressClientV1Beta1
    {
        /// <summary>
        ///     Create a new <see cref="IngressClientV1Beta1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public IngressClientV1Beta1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Ingress with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Ingress to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="IngressV1Beta1"/> representing the current state for the Ingress, or <c>null</c> if no Ingress was found with the specified name and namespace.
        /// </returns>
        public async Task<IngressV1Beta1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<IngressV1Beta1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Ingresses in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Ingresses.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="IngressListV1Beta1"/> containing the Ingresses.
        /// </returns>
        public async Task<IngressListV1Beta1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<IngressListV1Beta1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific Ingress.
        /// </summary>
        /// <param name="name">
        ///     The name of the Ingress to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<IngressV1Beta1>> Watch(string name, string kubeNamespace = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<IngressV1Beta1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                operationDescription: $"watch v1/Ingress '{name}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Watch for events relating to Ingresses.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Ingresses.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<IngressV1Beta1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<IngressV1Beta1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/Ingresses with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of an <see cref="IngressV1Beta1"/>.
        /// </summary>
        /// <param name="newIngress">
        ///     A <see cref="IngressV1Beta1"/> representing the Ingress to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="IngressV1Beta1"/> representing the current state for the newly-created Ingress.
        /// </returns>
        public async Task<IngressV1Beta1> Create(IngressV1Beta1 newIngress, CancellationToken cancellationToken = default)
        {
            if (newIngress == null)
                throw new ArgumentNullException(nameof(newIngress));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newIngress?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newIngress,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<IngressV1Beta1>(
                    operationDescription: "create v1/Ingress resource"
                );
        }

        /// <summary>
        ///     Request update (PATCH) of an <see cref="IngressV1Beta1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Ingress.
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
        ///     A <see cref="IngressV1Beta1"/> representing the current state for the updated Ingress.
        /// </returns>
        public async Task<IngressV1Beta1> Update(string name, Action<JsonPatchDocument<IngressV1Beta1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified Ingress.
        /// </summary>
        /// <param name="name">
        ///     The name of the Ingress to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     An <see cref="StatusV1"/> indicating the result of the request.
        /// </returns>
        public async Task<StatusV1> Delete(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await Http
                .DeleteAsync(
                    Requests.ByName.WithTemplateParameters(new
                    {
                        Name = name,
                        Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<StatusV1>("delete v1beta1/Ingress resource", HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the Ingress (v1beta1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Ingress (v1beta1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("apis/extensions/v1beta1/namespaces/{Namespace}/ingresses?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name Ingress (v1beta1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("apis/extensions/v1beta1/namespaces/{Namespace}/ingresses/{Name}");

            /// <summary>
            ///     A watch-by-name Ingress (v1beta1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName  = KubeRequest.Create("apis/extensions/v1beta1/watch/namespaces/{Namespace}/ingresses/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes Ingress (v1beta1) API.
    /// </summary>
    public interface IIngressClientV1Beta1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the Ingress with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Ingress to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="IngressV1Beta1"/> representing the current state for the Ingress, or <c>null</c> if no Ingress was found with the specified name and namespace.
        /// </returns>
        Task<IngressV1Beta1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all Ingresses in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Ingresses.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="IngressListV1Beta1"/> containing the Ingresses.
        /// </returns>
        Task<IngressListV1Beta1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific Ingress.
        /// </summary>
        /// <param name="name">
        ///     The name of the Ingress to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<IngressV1Beta1>> Watch(string name, string kubeNamespace = null);

        /// <summary>
        ///     Watch for events relating to Ingresses.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Ingresses.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<IngressV1Beta1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of an <see cref="IngressV1Beta1"/>.
        /// </summary>
        /// <param name="newIngress">
        ///     A <see cref="IngressV1Beta1"/> representing the Ingress to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="IngressV1Beta1"/> representing the current state for the newly-created Ingress.
        /// </returns>
        Task<IngressV1Beta1> Create(IngressV1Beta1 newIngress, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of an <see cref="IngressV1Beta1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Ingress.
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
        ///     A <see cref="IngressV1Beta1"/> representing the current state for the updated Ingress.
        /// </returns>
        Task<IngressV1Beta1> Update(string name, Action<JsonPatchDocument<IngressV1Beta1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified Ingress.
        /// </summary>
        /// <param name="name">
        ///     The name of the Ingress to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     An <see cref="StatusV1"/> indicating the result of the request.
        /// </returns>
        Task<StatusV1> Delete(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);
    }
}
