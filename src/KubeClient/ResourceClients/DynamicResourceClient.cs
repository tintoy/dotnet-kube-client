using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using ApiMetadata;
    using Http;
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
            IReadOnlyList<Assembly> modelTypeAssemblies = KubeClient.GetClientOptions().ModelTypeAssemblies;

            var assemblyModelTypeLookup = ModelMetadata.KubeObject.BuildKindToTypeLookup(modelTypeAssemblies);
            foreach (var key in assemblyModelTypeLookup.Keys)
                _modelTypeLookup[key] = assemblyModelTypeLookup[key];

            var assemblyListModelTypeLookup = ModelMetadata.KubeObject.BuildKindToListTypeLookup(modelTypeAssemblies);
            foreach (var key in assemblyListModelTypeLookup.Keys)
                _modelTypeLookup[key] = assemblyListModelTypeLookup[key];
        }

        /// <summary>
        ///     Metadata for Kubernetes resource APIs.
        /// </summary>
        public KubeApiMetadataCache ApiMetadata { get; } = new KubeApiMetadataCache();

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
        ///     Create a Kubernetes resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of resource to create.
        /// </typeparam>
        /// <param name="resource">
        ///     A <typeparamref name="TResource"/> representing the resource to create.
        /// </param>
        /// <param name="isNamespaced">
        ///     Is the resource type commonly namespaced?
        ///     
        ///     In other words, does the resource's API path contain a "{namespace}" segment?
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the newly-created resource.
        /// </returns>
        public async Task<TResource> Create<TResource>(TResource resource, bool isNamespaced = true, CancellationToken cancellationToken = default)
            where TResource : KubeResourceV1
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            await EnsureApiMetadata(cancellationToken);

            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
            string apiPath = GetApiPath(kind, apiVersion, isNamespaced);

            HttpRequest request = KubeRequest.Create(apiPath);
            if (isNamespaced)
            {
                request = request.WithTemplateParameters(new
                {
                    @namespace = resource.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                });
            }

            return await Http
                .PostAsJsonAsync(request,
                    postBody: resource,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<TResource>(
                    operationDescription: isNamespaced ?
                        $"create {apiVersion}/{kind} resource in namespace {resource?.Metadata?.Namespace ?? KubeClient.DefaultNamespace}"
                        :
                        $"create {apiVersion}/{kind} resource"
                );
        }

        /// <summary>
        ///     Update a Kubernetes resource using a server-side apply operation.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of resource to update.
        /// </typeparam>
        /// <param name="resource">
        ///     A <typeparamref name="TResource"/> representing the resource to update.
        /// </param>
        /// <param name="fieldManager">
        ///     The name of the field manager to use when performing the server-side apply.
        /// </param>
        /// <param name="force">
        ///     Allow the field manager to take ownership of fields if required?
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the updated resource.
        /// </returns>
        public async Task<TResource> Apply<TResource>(TResource resource, string fieldManager, bool force = false, CancellationToken cancellationToken = default)
            where TResource : KubeResourceV1
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (String.IsNullOrWhiteSpace(fieldManager))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(fieldManager)}.", nameof(fieldManager));

            await EnsureApiMetadata(cancellationToken);

            string resourceYaml;
            using (StringWriter yamlWriter = new StringWriter())
            {
                Yaml.Serialize(resource, yamlWriter);

                resourceYaml = yamlWriter.ToString();
            }

            KubeResourceV1 appliedResource = await ApplyYaml(
                resource.Metadata.Name,
                resource.Kind,
                resource.ApiVersion,
                resourceYaml,
                fieldManager,
                force,
                kubeNamespace: resource.Metadata.Namespace,
                cancellationToken
            );

            return (TResource)appliedResource;
        }

        /// <summary>
        ///     Update a Kubernetes resource using a server-side apply operation with the specified YAML.
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
        /// <param name="yaml">
        ///     A string containing the resource YAML.
        /// </param>
        /// <param name="fieldManager">
        ///     The name of the field manager to use when performing the server-side apply.
        /// </param>
        /// <param name="force">
        ///     Allow the field manager to take ownership of fields if required?
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
        public async Task<KubeResourceV1> ApplyYaml(string name, string kind, string apiVersion, string yaml, string fieldManager, bool force = false, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            if (String.IsNullOrWhiteSpace(yaml))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(yaml)}.", nameof(yaml));

            bool isNamespaced = !String.IsNullOrWhiteSpace(kubeNamespace);

            await EnsureApiMetadata(cancellationToken);
            string apiPath = GetApiPath(kind, apiVersion, isNamespaced);

            Type modelType = GetModelType(kind, apiVersion);

            HttpRequest request = KubeRequest.Create(apiPath).WithRelativeUri("{name}?fieldManager={fieldManager?}&force={force?}")
                .WithTemplateParameters(new
                {
                    name,
                    @namespace = kubeNamespace,
                    fieldManager,
                    force = force ? "true" : null
                });

            using (StringContent patchBody = new StringContent(yaml, Encoding.UTF8, ApplyPatchYamlMediaType))
            using (HttpResponseMessage responseMessage = await Http.PatchAsync(request, patchBody, mediaType: ApplyPatchYamlMediaType, cancellationToken: cancellationToken).ConfigureAwait(false))
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
                        $"Unable to patch {apiVersion}/{kind} resource '{name}' in namespace '{kubeNamespace}' using server-side apply (resource not found)."
                        :
                        $"Unable to patch {apiVersion}/{kind} resource '{name}' using server-side apply (resource not found).";

                    throw new KubeClientException(errorMessage);
                }

                throw new KubeClientException($"Unable to patch {apiVersion}/{kind} resource (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode, status)
                );
            }
        }

        /// <summary>
        ///     Request deletion of the specified resource.
        /// </summary>
        /// <param name="name">
        ///     The name of the resource to delete.
        /// </param>
        /// <param name="kind">
        ///     The kind of resource to delete.
        /// </param>
        /// <param name="apiVersion">
        ///     The API version of the resource to delete.
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
        ///     A <see cref="KubeResourceV1"/> representing the resource's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/> indicating the operation result.
        /// </returns>
        public async Task<KubeResourceResultV1<KubeResourceV1>> Delete(string name, string kind, string apiVersion, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
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

            string operationDescription = $"delete {apiVersion}/{kind} resource '{name}' in namespace '{kubeNamespace}'";

            return await Http
                .DeleteAsJsonAsync(
                    request: KubeRequest.Create(apiPath).WithRelativeUri("{name}").WithTemplateParameters(new
                    {
                        name,
                        @namespace = kubeNamespace
                    }),
                    deleteBody: new DeleteOptionsV1
                    {
                        PropagationPolicy = propagationPolicy
                    }
                )
                .ReadContentAsResourceOrStatusV1(modelType, operationDescription, HttpStatusCode.OK, HttpStatusCode.NotFound);
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
        ///     Metadata for Kubernetes resource APIs.
        /// </summary>
        KubeApiMetadataCache ApiMetadata { get; }

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
        ///     Create a Kubernetes resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of resource to create.
        /// </typeparam>
        /// <param name="resource">
        ///     A <typeparamref name="TResource"/> representing the resource to create.
        /// </param>
        /// <param name="isNamespaced">
        ///     Is the resource namespaced?
        ///     
        ///     In other words, does the resource's API path contain a "{namespace}" segment?
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the updated resource.
        /// </returns>
        Task<TResource> Create<TResource>(TResource resource, bool isNamespaced = true, CancellationToken cancellationToken = default)
            where TResource : KubeResourceV1;

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

        /// <summary>
        ///     Update a Kubernetes resource using a server-side apply operation.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of resource to update.
        /// </typeparam>
        /// <param name="resource">
        ///     A <typeparamref name="TResource"/> representing the resource to update.
        /// </param>
        /// <param name="fieldManager">
        ///     The name of the field manager to use when performing the server-side apply.
        /// </param>
        /// <param name="force">
        ///     Allow the field manager to take ownership of fields if required?
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the updated resource.
        /// </returns>
        Task<TResource> Apply<TResource>(TResource resource, string fieldManager, bool force = false, CancellationToken cancellationToken = default)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Update a Kubernetes resource using a server-side apply operation with the specified YAML.
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
        /// <param name="yaml">
        ///     A string containing the resource YAML.
        /// </param>
        /// <param name="fieldManager">
        ///     The name of the field manager to use when performing the server-side apply.
        /// </param>
        /// <param name="force">
        ///     Allow the field manager to take ownership of fields if required?
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
        Task<KubeResourceV1> ApplyYaml(string name, string kind, string apiVersion, string yaml, string fieldManager, bool force = false, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified resource.
        /// </summary>
        /// <param name="name">
        ///     The name of the resource to delete.
        /// </param>
        /// <param name="kind">
        ///     The kind of resource to delete.
        /// </param>
        /// <param name="apiVersion">
        ///     The API version of the resource to delete.
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
        ///     A <see cref="KubeResourceV1"/> representing the resource's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/> indicating the operation result.
        /// </returns>
        Task<KubeResourceResultV1<KubeResourceV1>> Delete(string name, string kind, string apiVersion, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
