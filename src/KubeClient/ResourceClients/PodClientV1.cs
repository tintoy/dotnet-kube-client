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
    ///     A client for the Kubernetes Pods (v1) API.
    /// </summary>
    public class PodClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="PodClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public PodClientV1(KubeApiClient client)
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
        public IObservable<ResourceEventV1<PodV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<PodV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                })
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
        ///     Limit the number of bytes returned.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A string containing the logs.
        /// </returns>
        public async Task<string> Logs(string name, string containerName = null, string kubeNamespace = null, int? limitBytes = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            HttpResponseMessage responseMessage = await Http.GetAsync(
                Requests.Logs.WithTemplateParameters(new
                {
                    Name = name,
                    ContainerName = containerName,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LimitBytes = limitBytes
                }),
                cancellationToken
            );
            using (responseMessage)
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadAsStringAsync();

                throw new HttpRequestException<StatusV1>(responseMessage.StatusCode,
                    response: await responseMessage.ReadContentAsAsync<StatusV1, StatusV1>()
                );
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
        ///     Limit the number of bytes returned.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> sequence of lines from the log.
        /// </returns>
        public IObservable<string> StreamLogs(string name, string containerName = null, string kubeNamespace = null, int? limitBytes = null)
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
                    Follow = "true"
                })
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
                .ReadContentAsAsync<PodV1, StatusV1>();
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
                .ReadContentAsAsync<StatusV1, StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
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
            public static readonly HttpRequest Logs         = ByName.WithRelativeUri("log?limitBytes={LimitBytes?}&container={ContainerName?}&follow={Follow?}");
        }
    }
}
