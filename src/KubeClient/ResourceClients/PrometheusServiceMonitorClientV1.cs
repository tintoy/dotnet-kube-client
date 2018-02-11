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
    ///     A client for the Prometheus ServiceMonitor (v1) API.
    /// </summary>
    public class PrometheusServiceMonitorClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="PrometheusServiceMonitorClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public PrometheusServiceMonitorClientV1(KubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get all ServiceMonitors in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ServiceMonitors.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The ServiceMonitors, as a list of <see cref="PrometheusServiceMonitorV1"/>es.
        /// </returns>
        public async Task<List<PrometheusServiceMonitorV1>> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            PrometheusServiceMonitorListV1 matchingServiceMonitors =
                await Http.GetAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = kubeNamespace ?? Client.DefaultNamespace,
                        LabelSelector = labelSelector
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<PrometheusServiceMonitorListV1, StatusV1>();

            return matchingServiceMonitors.Items;
        }

        /// <summary>
        ///     Get the ServiceMonitor with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceMonitor to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PrometheusServiceMonitorV1"/> representing the current state for the ServiceMonitor, or <c>null</c> if no ServiceMonitor was found with the specified name and namespace.
        /// </returns>
        public async Task<PrometheusServiceMonitorV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<PrometheusServiceMonitorV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? Client.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Request creation of a Prometheus ServiceMonitor.
        /// </summary>
        /// <param name="newServiceMonitor">
        ///     A <see cref="PrometheusServiceMonitorV1"/> representing the ServiceMonitor to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PrometheusServiceMonitorV1"/> representing the current state for the newly-created ServiceMonitor.
        /// </returns>
        public async Task<PrometheusServiceMonitorV1> Create(PrometheusServiceMonitorV1 newServiceMonitor, CancellationToken cancellationToken = default)
        {
            if (newServiceMonitor == null)
                throw new ArgumentNullException(nameof(newServiceMonitor));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newServiceMonitor?.Metadata?.Namespace ?? Client.DefaultNamespace
                    }),
                    postBody: newServiceMonitor,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<PrometheusServiceMonitorV1, StatusV1>();
        }

        /// <summary>
        ///     Request deletion of the specified ServiceMonitor.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceMonitor to delete.
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
                        Namespace = kubeNamespace ?? Client.DefaultNamespace
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<StatusV1, StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the ServiceMonitors (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level CoreOS Monitoring (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("apis/monitoring.coreos.com/v1/namespaces/{Namespace}/servicemonitors?labelSelector={LabelSelector?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name CoreOS Monitoring (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("apis/monitoring.coreos.com/v1/namespaces/{Namespace}/servicemonitors/{Name}", SerializerSettings);
        }
    }
}
