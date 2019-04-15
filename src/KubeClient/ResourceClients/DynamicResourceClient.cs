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

    // TODO: Always prefer namespaced API paths (except for List operations) and use client's default namespace.

    /// <summary>
    ///     A client for dynamic access to Kubernetes resource APIs.
    /// </summary>
    public sealed class DynamicResourceClient
        : KubeResourceClient, IDynamicResourceClient
    {
        /// <summary>
        ///     Model CLR types, keyed by resource kind and API version.
        /// </summary>
        readonly Dictionary<(string kind, string apiVersion), Type> _modelTypeLookup = ModelMetadata.KubeObject.BuildKindToTypeLookup(
            typeof(KubeObjectV1).GetTypeInfo().Assembly
        );

        /// <summary>
        ///     Model CLR types, keyed by resource kind and API version.
        /// </summary>
        readonly Dictionary<(string kind, string apiVersion), Type> _listModelTypeLookup = ModelMetadata.KubeObject.BuildKindToListTypeLookup(
            typeof(KubeObjectV1).GetTypeInfo().Assembly
        );

        /// <summary>
        ///     Create a new <see cref="DynamicResourceClient"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public DynamicResourceClient(IKubeApiClient client)
            : base(client)
        {
            // Register metadata for additional model types (if any).
            foreach (Assembly modelTypeAssembly in KubeClient.GetClientOptions().ModelTypeAssemblies)
            {
                var assemblyModelTypeLookup = ModelMetadata.KubeObject.BuildKindToTypeLookup(modelTypeAssembly);
                foreach (var key in assemblyModelTypeLookup.Keys)
                    _modelTypeLookup[key] = assemblyModelTypeLookup[key];

                var assemblyListModelTypeLookup = ModelMetadata.KubeObject.BuildKindToListTypeLookup(modelTypeAssembly);
                foreach (var key in assemblyListModelTypeLookup.Keys)
                    _modelTypeLookup[key] = assemblyListModelTypeLookup[key];
            }
        }

        /// <summary>
        ///     Metadata for Kubernetes resource APIs.
        /// </summary>
        KubeApiMetadataCache ApiMetadata { get; } = new KubeApiMetadataCache();

        /// <summary>
        ///     Retrieve a single resource by name.
        /// </summary>
        /// <param name="name">
        ///     The resource name.
        /// </param>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
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
        public async Task<KubeResourceV1> Get(string name, string kind, string apiVersion, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            bool isNamespaced = !String.IsNullOrWhiteSpace(kubeNamespace);

            await EnsureApiMetadata(cancellationToken);
            string apiPath = GetApiPath(kind, apiVersion, isNamespaced);

            Type modelType = GetModelType(kind, apiVersion);

            HttpRequest request = KubeRequest.Create(apiPath).WithRelativeUri("{name}")
                .WithTemplateParameters(new
                {
                    name = name,
                    @namespace = kubeNamespace
                });

            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    // Code is slightly ugly for types only known at runtime; see if HTTPlease could be improved here.
                    using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (TextReader responseReader = new StreamReader(responseStream))
                    {
                        JsonSerializer serializer = responseMessage.GetJsonSerializer();

                        return (KubeResourceV1)serializer.Deserialize(responseReader, modelType);
                    }
                }

                // Ensure that HttpStatusCode.NotFound actually refers to the target resource.
                StatusV1 status = await responseMessage.ReadContentAsStatusV1Async(HttpStatusCode.NotFound).ConfigureAwait(false);
                if (status.Reason == "NotFound")
                    return null;

                throw new KubeClientException($"Unable to retrieve {apiVersion}/{kind} resource (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode, status)
                );
            }
        }

        /// <summary>
        ///     List resources.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The (optional) name of a Kubernetes namespace containing the resources.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The resource list (can be cast to <see cref="KubeResourceListV1{TResource}"/> for access to individual resources).
        /// </returns>
        public async Task<KubeResourceListV1> List(string kind, string apiVersion, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            bool isNamespaced = !String.IsNullOrWhiteSpace(kubeNamespace);

            await EnsureApiMetadata(cancellationToken);
            string apiPath = GetApiPath(kind, apiVersion, isNamespaced);

            Type listModelType = GetListModelType(kind, apiVersion);

            HttpRequest request = KubeRequest.Create(apiPath)
                .WithTemplateParameter("namespace", kubeNamespace);

            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    // Code is slightly ugly for types only known at runtime; see if HTTPlease could be improved here.
                    using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (TextReader responseReader = new StreamReader(responseStream))
                    {
                        JsonSerializer serializer = responseMessage.GetJsonSerializer();

                        return (KubeResourceListV1)serializer.Deserialize(responseReader, listModelType);
                    }
                }

                StatusV1 status = await responseMessage.ReadContentAsStatusV1Async(HttpStatusCode.NotFound).ConfigureAwait(false);

                throw new KubeClientException($"Unable to list {apiVersion}/{kind} resources (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode, status)
                );
            }
        }

        /// <summary>
        ///     Perform a JSON patch operation on a Kubernetes resource.
        /// </summary>
        /// <param name="name">
        ///     The resource name.
        /// </param>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <param name="patch">
        ///     A <see cref="JsonPatchDocument"/> representing the patch operation(s) to perform.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The (optional) name of a Kubernetes namespace containing the resources.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeResourceV1"/> representing the updated resource.
        /// </returns>
        public async Task<KubeResourceV1> Patch(string name, string kind, string apiVersion, JsonPatchDocument patch, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            if (patch == null)
                throw new ArgumentNullException(nameof(patch));

            bool isNamespaced = !String.IsNullOrWhiteSpace(kubeNamespace);

            await EnsureApiMetadata(cancellationToken);
            string apiPath = GetApiPath(kind, apiVersion, isNamespaced);

            Type modelType = GetModelType(kind, apiVersion);

            HttpRequest request = KubeRequest.Create(apiPath).WithRelativeUri("{name}")
                .WithTemplateParameters(new
                {
                    name = name,
                    @namespace = kubeNamespace
                });

            using (HttpResponseMessage responseMessage = await Http.PatchAsync(request, patchBody: patch, mediaType: PatchMediaType, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    // Code is slightly ugly for types only known at runtime; see if HTTPlease could be improved here.
                    using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (TextReader responseReader = new StreamReader(responseStream))
                    {
                        JsonSerializer serializer = responseMessage.GetJsonSerializer();

                        return (KubeResourceV1)serializer.Deserialize(responseReader, modelType);
                    }
                }

                // Ensure that HttpStatusCode.NotFound actually refers to the target resource.
                StatusV1 status = await responseMessage.ReadContentAsStatusV1Async(HttpStatusCode.NotFound).ConfigureAwait(false);
                if (status.Reason == "NotFound")
                {
                    string errorMessage = isNamespaced ?
                        $"Unable to patch {apiVersion}/{kind} resource '{name}' in namespace '{kubeNamespace}' (resource not found)."
                        :
                        $"Unable to patch {apiVersion}/{kind} resource '{name}' (resource not found).";

                    throw new KubeClientException(errorMessage);
                }

                throw new KubeClientException($"Unable to patch {apiVersion}/{kind} resource (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode, status)
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
        async Task EnsureApiMetadata(CancellationToken cancellationToken)
        {
            await Task.Yield();
            
            if (ApiMetadata.IsEmpty)
            {
                ApiMetadata.LoadFromMetadata(
                    typeof(KubeObjectV1).GetTypeInfo().Assembly
                );

                foreach (Assembly modelTypeAssembly in KubeClient.GetClientOptions().ModelTypeAssemblies)
                    ApiMetadata.LoadFromMetadata(modelTypeAssembly);

                if (ApiMetadata.IsEmpty) // Never happens (consider async preload *as a configurable option* and otherwise implement as a read-through cache)
                    await ApiMetadata.Load(KubeClient, cancellationToken: cancellationToken);
            }
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
        /// <param name="namespaced">
        ///     Require a path with Kubernetes namespace support?
        /// </param>
        /// <returns>
        ///     The resource API path.
        /// </returns>
        string GetApiPath(string kind, string apiVersion, bool namespaced)
        {
            // TODO: Add KubeAction parameter to improve path resolution.

            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            KubeApiMetadata apiMetadata = ApiMetadata.Get(kind, apiVersion);
            if (apiMetadata == null)
                throw new KubeClientException($"Cannot find resource API for kind '{kind}', apiVersion '{apiVersion}'.");

            KubeApiPathMetadata apiPathMetadata = namespaced ? apiMetadata.PrimaryNamespacedPathMetadata : apiMetadata.PrimaryPathMetadata;

            if (apiPathMetadata == null)
            {
                if (namespaced)
                    throw new KubeClientException($"Resource API for {apiVersion}/{kind} only supports listing resources within a specific namespace.");
                else
                    throw new KubeClientException($"Resource API for {apiVersion}/{kind} does not support listing resources within a specific namespace.");
            }

            return apiPathMetadata.Path;
        }

        /// <summary>
        ///     Get the CLR <see cref="Type"/> of the model that corresponds to the specified Kubernetes resource type.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <returns>
        ///     The model <see cref="Type"/>.
        /// </returns>
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

        /// <summary>
        ///     Get the CLR <see cref="Type"/> of the list model that corresponds to the specified Kubernetes resource type.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <returns>
        ///     The model <see cref="Type"/>.
        /// </returns>
        Type GetListModelType(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            Type listModelType;
            if (!_listModelTypeLookup.TryGetValue((kind, apiVersion), out listModelType))
                throw new KubeClientException($"Cannot determine the list model type that corresponds to {apiVersion}/{kind}.");

            return listModelType;
        }
    }

    /// <summary>
    ///     Represents a client for dynamic access to Kubernetes resource APIs.
    /// </summary>
    public interface IDynamicResourceClient
        : IKubeResourceClient
    {
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
        Task<KubeResourceV1> Get(string name, string kind, string apiVersion, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     List resources.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The (optional) name of a Kubernetes namespace containing the resources.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The resource list (can be cast to <see cref="KubeResourceListV1{TResource}"/> for access to individual resources).
        /// </returns>
        Task<KubeResourceListV1> List(string kind, string apiVersion, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Perform a JSON patch operation on a Kubernetes resource.
        /// </summary>
        /// <param name="name">
        ///     The resource name.
        /// </param>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <param name="patch">
        ///     A <see cref="JsonPatchDocument"/> representing the patch operation(s) to perform.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The (optional) name of a Kubernetes namespace containing the resources.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeResourceV1"/> representing the updated resource.
        /// </returns>
        Task<KubeResourceV1> Patch(string name, string kind, string apiVersion, JsonPatchDocument patch, string kubeNamespace = null, CancellationToken cancellationToken = default);
    }
}
