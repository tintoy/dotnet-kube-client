using HTTPlease;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using ApiMetadata;
    using Models;

    /// <summary>
    ///     A client for the Kubernetes resource APIs whose types are only known at runtime.
    /// </summary>
    public class DynamicClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Model CLR types, keyed by resource kind and API version.
        /// </summary>
        readonly Dictionary<(string kind, string apiVersion), Type> _modelTypeLookup = ModelMetadata.KubeObject.BuildKindToTypeLookup(
            typeof(KubeObjectV1).GetTypeInfo().Assembly
        );

        /// <summary>
        ///     Create a new <see cref="DynamicClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public DynamicClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Metadata for Kubernetes resource APIs.
        /// </summary>
        KubeApiMetadataCache ApiMetadata { get; } = new KubeApiMetadataCache();

        /// <summary>
        ///     Retrieve a single resource by name.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <param name="name">
        ///     The resource name.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The (optional) name of a Kubernetes namespace containing the resource.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The resource, or <c>null</c> if the resource was not found.
        /// </returns>
        public async Task<KubeResourceV1> Get(string kind, string apiVersion, string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));
            
            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            Type modelType = GetModelType(kind, apiVersion);
            if (!_modelTypeLookup.TryGetValue((kind, apiVersion), out modelType))
                throw new KubeClientException($"Cannot determine the model type that corresponds to {apiVersion}/{kind}.");

            await EnsureMetadata(cancellationToken);

            bool isNamespaced = !String.IsNullOrWhiteSpace(kubeNamespace);
            string apiPath = GetApiPath(kind, apiVersion, isNamespaced);

            HttpRequest request = KubeRequest.Create(apiPath).WithRelativeUri("{Name}")
                .WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace
                });

            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    // Code is slightly ugly for types only known at runtime; see if HTTPlease could be improved here.
                    using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (TextReader responseReader = new StreamReader(responseStream))
                    {
                        JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);

                        return (KubeResourceV1)serializer.Deserialize(responseReader, modelType);
                    }
                }

                // Ensure that HttpStatusCode.NotFound actually refers to the target resource.
                StatusV1 status = await responseMessage.ReadContentAsAsync<StatusV1, StatusV1>(HttpStatusCode.NotFound).ConfigureAwait(false);
                if (status.Reason == "NotFound")
                    return null;

                throw new KubeClientException($"Failed to retrieve {apiVersion}/{kind} resource (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode, response: status)
                );
            }
        }

        /// <summary>
        ///     Ensure that the API metadata cache is populated.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        async Task EnsureMetadata(CancellationToken cancellationToken)
        {
            if (ApiMetadata.IsEmpty)
                await ApiMetadata.Load(KubeClient, cancellationToken);
        }

        /// <summary>
        ///     Get the primary path for the specified Kubernetes resource API.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <param name="isNamespaced"></param>
        /// <returns></returns>
        string GetApiPath(string kind, string apiVersion, bool isNamespaced)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));
            
            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));
            
            KubeApiMetadata apiMetadata = ApiMetadata.Get(kind, apiVersion);
            if (apiMetadata == null)
                throw new KubeClientException($"Cannot find resource API for {apiVersion}/{kind}.");

            KubeApiPathMetadata apiPathMetadata = isNamespaced ? apiMetadata.PrimaryPathMetadata : apiMetadata.PrimaryNamespacedPathMetadata;
            
            if (apiPathMetadata == null)
            {
                if (isNamespaced)
                    throw new KubeClientException($"Resource API for {apiVersion}/{kind} only supports listing resources within a specific namespace.");
                else
                    throw new KubeClientException($"Resource API for {apiVersion}/{kind} does not support listing resources within a specific namespace.");
            }

            return apiPathMetadata.Path;
        }

        Type GetModelType(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));
            
            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            Type modelType;
            if (!_modelTypeLookup.TryGetValue((kind, apiVersion), out modelType))
                throw new KubeClientException($"Cannot determine the model type that corresponds to {apiVersion}/{kind}.");

            return modelType;
        }
    }
}