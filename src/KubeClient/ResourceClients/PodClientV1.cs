using HTTPlease;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Pods (v1) API.
    /// </summary>
    public class PodClientV1
        : KubeResourceClient, IPodClientV1
    {
        /// <summary>
        ///     Create a new <see cref="PodClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public PodClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Pod with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Pod to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PodV1"/> representing the current state for the Pod, or <c>null</c> if no Pod was found with the specified name and namespace.
        /// </returns>
        public async Task<PodV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<PodV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Pods in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Pods.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PodListV1"/> containing the Pods.
        /// </returns>
        public async Task<PodListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<PodListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to Pods.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Pods.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<PodV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<PodV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/Pods with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Get the combined logs for the Pod with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Pod.
        /// </param>
        /// <param name="containerName">
        ///     The name of the container.
        /// 
        ///     Not required if the pod only has a single container.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="limitBytes">
        ///     Limit the number of bytes returned (optional).
        /// </param>
        /// <param name="tailLines">
        ///     The number of lines from the end of the log to show (optional).
        /// 
        ///     If not specified, logs are since from the creation of the container.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A string containing the logs.
        /// </returns>
        public async Task<string> Logs(string name, string containerName = null, string kubeNamespace = null, int? limitBytes = null, int? tailLines = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            HttpResponseMessage responseMessage = await Http.GetAsync(
                Requests.Logs.WithTemplateParameters(new
                {
                    Name = name,
                    ContainerName = containerName,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LimitBytes = limitBytes,
                    TailLines = tailLines
                }),
                cancellationToken
            );
            using (responseMessage)
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadAsStringAsync();

                throw new KubeClientException($"Unable to retrieve logs for container '{containerName ?? "<default>"}' of v1/Pod '{name}' in namespace '{kubeNamespace ?? KubeClient.DefaultNamespace}'.",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode,
                    response: await responseMessage.ReadContentAsStatusV1Async(responseMessage.StatusCode).ConfigureAwait(false)
                ));
            }
        }

        /// <summary>
        ///     Stream the combined logs for the Pod with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Pod.
        /// </param>
        /// <param name="containerName">
        ///     The name of the container.
        /// 
        ///     Not required if the pod only has a single container.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="limitBytes">
        ///     Limit the number of bytes returned (optional).
        /// </param>
        /// <param name="tailLines">
        ///     The number of lines from the end of the log to show (optional).
        /// 
        ///     If not specified, logs are since from the creation of the container.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> sequence of lines from the log.
        /// </returns>
        public IObservable<string> StreamLogs(string name, string containerName = null, string kubeNamespace = null, int? limitBytes = null, int? tailLines = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return ObserveLines(
                Requests.Logs.WithTemplateParameters(new
                {
                    Name = name,
                    ContainerName = containerName,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LimitBytes = limitBytes,
                    TailLines = tailLines,
                    Follow = "true"
                }),
                operationDescription: $"stream logs for v1/Pod '{name}' (container '{containerName ?? "<default>"}') in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="PodV1"/>.
        /// </summary>
        /// <param name="newPod">
        ///     A <see cref="PodV1"/> representing the Pod to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PodV1"/> representing the current state for the newly-created Pod.
        /// </returns>
        public async Task<PodV1> Create(PodV1 newPod, CancellationToken cancellationToken = default)
        {
            if (newPod == null)
                throw new ArgumentNullException(nameof(newPod));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newPod?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newPod,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<PodV1>(
                    operationDescription: $"create v1/Pod resource in namespace {newPod?.Metadata?.Namespace ?? KubeClient.DefaultNamespace}"
                );
        }

        /// <summary>
        ///     Request deletion of the specified Pod.
        /// </summary>
        /// <param name="name">
        ///     The name of the Pod to delete.
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
        ///     A <see cref="PodV1"/> representing the pod's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/> indicating the operation result.
        /// </returns>
        public Task<KubeResourceResultV1<PodV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<PodV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the Pods (v1) API.
        /// </summary>
        public static class Requests
        {
            /// <summary>
            ///     A collection-level Pod (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("api/v1/namespaces/{Namespace}/pods?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name Pod (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("api/v1/namespaces/{Namespace}/pods/{Name}");

            /// <summary>
            ///     A get-logs Pod (v1) request.
            /// </summary>
            public static readonly HttpRequest Logs         = ByName.WithRelativeUri("log?container={ContainerName?}&follow={Follow?}&limitBytes={LimitBytes?}&tailLines={TailLines?}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes Pods (v1) API.
    /// </summary>
    public interface IPodClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the Pod with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Pod to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PodV1"/> representing the current state for the Pod, or <c>null</c> if no Pod was found with the specified name and namespace.
        /// </returns>
        Task<PodV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all Pods in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Pods.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PodListV1"/> containing the Pods.
        /// </returns>
        Task<PodListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to Pods.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Pods.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<PodV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Get the combined logs for the Pod with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Pod.
        /// </param>
        /// <param name="containerName">
        ///     The name of the container.
        /// 
        ///     Not required if the pod only has a single container.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="limitBytes">
        ///     Limit the number of bytes returned (optional).
        /// </param>
        /// <param name="tailLines">
        ///     The number of lines from the end of the log to show (optional).
        /// 
        ///     If not specified, logs are since from the creation of the container.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A string containing the logs.
        /// </returns>
        Task<string> Logs(string name, string containerName = null, string kubeNamespace = null, int? limitBytes = null, int? tailLines = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Stream the combined logs for the Pod with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Pod.
        /// </param>
        /// <param name="containerName">
        ///     The name of the container.
        /// 
        ///     Not required if the pod only has a single container.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="limitBytes">
        ///     Limit the number of bytes returned (optional).
        /// </param>
        /// <param name="tailLines">
        ///     The number of lines from the end of the log to show (optional).
        /// 
        ///     If not specified, logs are since from the creation of the container.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> sequence of lines from the log.
        /// </returns>
        IObservable<string> StreamLogs(string name, string containerName = null, string kubeNamespace = null, int? limitBytes = null, int? tailLines = null);

        /// <summary>
        ///     Request creation of a <see cref="PodV1"/>.
        /// </summary>
        /// <param name="newPod">
        ///     A <see cref="PodV1"/> representing the Pod to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PodV1"/> representing the current state for the newly-created Pod.
        /// </returns>
        Task<PodV1> Create(PodV1 newPod, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified Pod.
        /// </summary>
        /// <param name="name">
        ///     The name of the Pod to delete.
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
        ///     A <see cref="PodV1"/> representing the pod's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/> indicating the operation result.
        /// </returns>
        Task<KubeResourceResultV1<PodV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
