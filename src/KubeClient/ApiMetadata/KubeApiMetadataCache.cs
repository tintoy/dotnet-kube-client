using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ApiMetadata
{
    using Models;

    /// <summary>
    ///     A cache for Kubernetes resource API metadata.
    /// </summary>
    public sealed class KubeApiMetadataCache
    {
        /// <summary>
        ///     Well-known group prefixes.
        /// </summary>
        public static readonly IReadOnlyCollection<string> ApiGroupPrefixes = new string[] { "api", "apis" };

        /// <summary>
        ///     An object used to synchronise access to cache state.
        /// </summary>
        readonly object _stateLock = new object();

        /// <summary>
        ///     Model metadata, keyed by apiVersion/Kind, singular name, and short names.
        /// </summary>
        readonly Dictionary<string, KubeApiMetadata> _metadata = new Dictionary<string, KubeApiMetadata>();

        /// <summary>
        ///     Create a new Kubernetes resource metadata cache.
        /// </summary>
        public KubeApiMetadataCache()
        {
        }

        /// <summary>
        ///     Is the cache currently empty?
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                lock (_stateLock)
                {
                    return _metadata.Count == 0;
                }
            }
        }

        /// <summary>
        ///     Retrieve metadata for a Kubernetes resource API.
        /// </summary>
        /// <typeparam name="TModel">
        ///     The type of model that represents the resource.
        /// </typeparam>
        /// <returns>
        ///     The API metadata, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public KubeApiMetadata Get<TModel>()
            where TModel : KubeObjectV1
        {
            return Get(
                typeof(TModel)
            );
        }

        /// <summary>
        ///     Retrieve metadata for a Kubernetes resource API.
        /// </summary>
        /// <param name="modelType">
        ///     The CLR <see cref="Type"/> of the model that represents the resource.
        /// </param>
        /// <returns>
        ///     The API metadata, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public KubeApiMetadata Get(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind(modelType);
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException($"Model type {modelType.FullName} has not been decorated with KubeResourceAttribute or KubeResourceListAttribute.", nameof(modelType));

            return Get(kind, apiVersion);
        }

        /// <summary>
        ///     Retrieve metadata for a Kubernetes resource API.
        /// </summary>
        /// <param name="name">
        ///     The resource singular-name or short-name.
        /// </param>
        /// <returns>
        ///     The API metadata, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public KubeApiMetadata Get(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (_metadata.TryGetValue(name, out KubeApiMetadata apiMetadata))
                return apiMetadata;

            return null;
        }

        /// <summary>
        ///     Retrieve metadata for a Kubernetes resource API.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <returns>
        ///     The API metadata, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public KubeApiMetadata Get(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            lock (_stateLock)
            {
                string cacheKey = CreateCacheKey(kind, apiVersion);
                if (_metadata.TryGetValue(cacheKey, out KubeApiMetadata metadata))
                    return metadata;
            }

            return null;
        }

        /// <summary>
        ///     Retrieve the primary path of a Kubernetes resource API.
        /// </summary>
        /// <typeparam name="TModel">
        ///     The type of model that represents the resource.
        /// </typeparam>
        /// <returns>
        ///     The API's primary (i.e. first) path, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public string GetPrimaryPath<TModel>()
            where TModel : KubeObjectV1
        {
            return GetPrimaryPath(
                typeof(TModel)
            );
        }

        /// <summary>
        ///     Retrieve the primary path of a Kubernetes resource API.
        /// </summary>
        /// <param name="modelType">
        ///     The CLR <see cref="Type"/> of the model that represents the resource.
        /// </param>
        /// <returns>
        ///     The API's primary (i.e. first) path, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public string GetPrimaryPath(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind(modelType);
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException($"Model type {modelType.FullName} has not been decorated with KubeResourceAttribute or KubeResourceListAttribute.", nameof(modelType));

            return GetPrimaryPath(kind, apiVersion);
        }

        /// <summary>
        ///     Retrieve the primary path of a Kubernetes resource API.
        /// </summary>
        /// <param name="name">
        ///     The resource singular-name or short-name.
        /// </param>
        /// <returns>
        ///     The API metadata, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public string GetPrimaryPath(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return Get(name)?.PrimaryPath;
        }

        /// <summary>
        ///     Retrieve the primary path of a Kubernetes resource API.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <returns>
        ///     The API's primary (i.e. first) path, or <c>null</c> if no metadata was found for the API.
        /// </returns>
        public string GetPrimaryPath(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            lock (_stateLock)
            {
                KubeApiMetadata metadata = Get(kind, apiVersion);
                if (metadata == null)
                    throw new KeyNotFoundException($"No API metadata found for '{kind}/{apiVersion}'");

                return metadata.PrimaryPath;
            }
        }

        /// <summary>
        ///     Clear the cache.
        /// </summary>
        public void Clear()
        {
            lock (_stateLock)
            {
                _metadata.Clear();
            }
        }

        /// <summary>
        ///     Populate the cache from model metadata.
        /// </summary>
        /// <param name="assembly">
        ///     The assembly containing model types to process.
        /// </param>
        /// <param name="clearExisting">
        ///     Remove existing metadata from the cache?
        /// </param>
        public void LoadFromMetadata(Assembly assembly, bool clearExisting = false)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            Dictionary<(string kind, string apiVersion), Type> modelMetadata = ModelMetadata.KubeObject.BuildKindToTypeLookup(assembly);

            var loadedMetadata = new List<KubeApiMetadata>();

            foreach (var kindAndApiVersion in modelMetadata.Keys)
            {
                Type modelType = modelMetadata[kindAndApiVersion];

                // TODO: Add SingularName and ShortNames to model metadata (as custom attributes), but where do we get them from? They appear to only be available at runtime (via the API).

                var apiPaths = new List<KubeApiPathMetadata>();

                KubeApiAttribute[] apiAttributes = modelType.GetTypeInfo().GetCustomAttributes<KubeApiAttribute>().ToArray();

                string[] listApiPaths =
                    apiAttributes.Where(
                        api => api.Action == KubeAction.List
                    )
                    .SelectMany(api => api.Paths)
                    .ToArray();
                string namespacedPath = listApiPaths.FirstOrDefault(
                    path => path.Contains("namespaces/{namespace}") && !path.EndsWith("/{name}")
                );
                if (!String.IsNullOrWhiteSpace(namespacedPath))
                {
                    apiPaths.Add(new KubeApiPathMetadata(
                        path: namespacedPath,
                        isNamespaced: true,
                        verbs: new string[] { "list", "get", "put", "patch" }
                    ));
                }
                string allNamespacesPath = listApiPaths.FirstOrDefault(
                    path => !path.Contains("namespaces/{namespace}") && !path.EndsWith("/{name}")
                );
                if (!String.IsNullOrWhiteSpace(namespacedPath))
                {
                    apiPaths.Add(new KubeApiPathMetadata(
                        path: namespacedPath,
                        isNamespaced: false,
                        verbs: new string[] { "list", "post" }
                    ));
                }

                if (apiPaths.Count == 0)
                    continue;

                loadedMetadata.Add(new KubeApiMetadata(
                    kindAndApiVersion.kind,
                    kindAndApiVersion.apiVersion,
                    singularName: null,
                    shortNames: new string[0],
                    isPreferredVersion: true,
                    paths: apiPaths
                ));
            }

            Cache(loadedMetadata, clearExisting);
        }

        /// <summary>
        ///     Populate the cache from the Kubernetes API.
        /// </summary>
        /// <param name="kubeClient">
        ///     The <see cref="KubeClient"/> used to retrieve API metadata.
        /// </param>
        /// <param name="clearExisting">
        ///     Remove existing metadata from the cache?
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task Load(IKubeApiClient kubeClient, bool clearExisting = false, CancellationToken cancellationToken = default)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));

            var loadedMetadata = new List<KubeApiMetadata>();

            foreach (string apiGroupPrefix in ApiGroupPrefixes)
            {
                APIGroupListV1 apiGroups = await kubeClient.APIGroupsV1().List(apiGroupPrefix, cancellationToken);
                if (apiGroupPrefix == "api")
                {
                    // Special case for old-style ("api/v1") APIs.
                    apiGroups.Groups.Add(new APIGroupV1
                    {
                        Name = "Core",
                        PreferredVersion = new GroupVersionForDiscoveryV1
                        {
                            GroupVersion = "v1"
                        }
                    });
                }

                foreach (APIGroupV1 apiGroup in apiGroups.Groups)
                {
                    List<GroupVersionForDiscoveryV1> groupVersions;
                    if (apiGroupPrefix == "api")
                    {
                        groupVersions = new List<GroupVersionForDiscoveryV1>
                        {
                            new GroupVersionForDiscoveryV1
                            {
                                GroupVersion = "v1"
                            }
                        };
                    }
                    else
                        groupVersions = apiGroup.Versions;

                    var metadataLoaders = new List<Task<List<KubeApiMetadata>>>();

                    foreach (GroupVersionForDiscoveryV1 groupVersion in groupVersions)
                    {
                        metadataLoaders.Add(
                            LoadGroupApis(kubeClient, apiGroupPrefix, apiGroup, groupVersion, cancellationToken)
                        );
                    }

                    List<KubeApiMetadata>[] completedLoads = await Task.WhenAll(metadataLoaders);
                    foreach (List<KubeApiMetadata> completedLoad in completedLoads)
                        loadedMetadata.AddRange(completedLoad);
                }
            }

            cancellationToken.ThrowIfCancellationRequested();

            Cache(loadedMetadata, clearExisting);
        }

        /// <summary>
        ///     Load metadata for the specified group of resource APIs.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client used to load API metadata.
        /// </param>
        /// <param name="apiGroupPrefix">
        ///     The API group prefix (usually "api" or "apis").
        /// </param>
        /// <param name="apiGroup">
        ///     The API group.
        /// </param>
        /// <param name="groupVersion">
        ///     The current API group version to examine.
        /// </param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A list of <see cref="KubeApiMetadata"/> representing the group's APIs.
        /// </returns>
        async Task<List<KubeApiMetadata>> LoadGroupApis(IKubeApiClient kubeClient, string apiGroupPrefix, APIGroupV1 apiGroup, GroupVersionForDiscoveryV1 groupVersion, CancellationToken cancellationToken)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));
            
            if (String.IsNullOrWhiteSpace(apiGroupPrefix))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiGroupPrefix'.", nameof(apiGroupPrefix));
            
            if (apiGroup == null)
                throw new ArgumentNullException(nameof(apiGroup));
            
            if (groupVersion == null)
                throw new ArgumentNullException(nameof(groupVersion));

            var apiMetadata = new List<KubeApiMetadata>();

            APIResourceListV1 apis = await kubeClient.APIResourcesV1().List(apiGroupPrefix, groupVersion.GroupVersion, cancellationToken);
            foreach (var apisForKind in apis.Resources.GroupBy(api => api.Kind))
            {
                string kind = apisForKind.Key;
                string singularName = null;
                IReadOnlyCollection<string> shortNames = new string[0];

                var apiPaths = new List<KubeApiPathMetadata>();

                bool isPreferredVersion = (groupVersion.GroupVersion == apiGroup.PreferredVersion.GroupVersion);

                foreach (APIResourceV1 api in apisForKind)
                {
                    // TODO: Parse and examine verbs to improve path resolution.

                    string apiPath = $"{apiGroupPrefix}/{groupVersion.GroupVersion}/{api.Name}";

                    apiPaths.Add(
                        new KubeApiPathMetadata(apiPath,
                            isNamespaced: false,
                            verbs: api.Verbs.ToArray()
                        )
                    );

                    if (api.Namespaced)
                    {
                        string namespacedApiPath = $"{apiGroupPrefix}/{groupVersion.GroupVersion}/namespaces/{{namespace}}/{api.Name}";

                        apiPaths.Add(
                            new KubeApiPathMetadata(namespacedApiPath,
                                isNamespaced: true,
                                verbs: api.Verbs.ToArray()
                            )
                        );
                    }

                    // Only use aliases from preferred API version.
                    if (isPreferredVersion)
                    {
                        if (singularName == null)
                            singularName = api.SingularName;

                        if (shortNames.Count == 0)
                            shortNames = api.ShortNames.ToArray();
                    }
                }

                apiMetadata.Add(
                    new KubeApiMetadata(kind, groupVersion.Version ?? groupVersion.GroupVersion, singularName, shortNames, isPreferredVersion, apiPaths)
                );
            }

            return apiMetadata;
        }

        /// <summary>
        ///     Retrieve all keys for which the cache contains metadata.
        /// </summary>
        /// <returns>
        ///     An array of strings representing the cache keys.
        /// </returns>
        public string[] GetCacheKeys()
        {
            lock (_stateLock)
            {
                return _metadata.Keys.ToArray();
            }
        }

        /// <summary>
        ///     Retrieve all resource kinds for which the cache contains metadata.
        /// </summary>
        /// <returns>
        ///     An array of (kind, apiVersion) tuples.
        /// </returns>
        public (string kind, string apiVersion)[] GetKnownResourceKinds()
        {
            lock (_stateLock)
            {
                return _metadata.Keys
                    .Where(
                        key => key.IndexOf('/') != -1
                    )
                    .Select(
                        key => key.Split('/')
                    )
                    .Select(
                        keyParts => (kind: keyParts[0], apiVersion: keyParts[1])
                    )
                    .ToArray();
            }
        }

        /// <summary>
        ///     Populate the cache using the specified metadata.
        /// </summary>
        /// <param name="loadedMetadata">
        ///     API metadata to be added to the cache.
        /// </param>
        /// <param name="clearExisting">
        ///     Remove existing metadata from the cache?
        /// </param>
        void Cache(IEnumerable<KubeApiMetadata> loadedMetadata, bool clearExisting)
        {
            if (loadedMetadata == null)
                throw new ArgumentNullException(nameof(loadedMetadata));

            lock (_stateLock)
            {
                if (clearExisting)
                    Clear();

                foreach (KubeApiMetadata apiMetadata in loadedMetadata)
                {
                    string cacheKey = CreateCacheKey(apiMetadata.Kind, apiMetadata.ApiVersion);
                    _metadata[cacheKey] = apiMetadata;

                    // Special-case: pluralise the resource kind.
                    string suffix = String.Empty;
                    if (apiMetadata.Kind.EndsWith("y"))
                        suffix = "ies";
                    else if (!apiMetadata.Kind.EndsWith("s"))
                        suffix = "s";

                    cacheKey = $"{apiMetadata.Kind}{suffix}";
                    if (!_metadata.ContainsKey(cacheKey))
                        _metadata.Add(cacheKey, apiMetadata);

                    // Only cache aliases from preferred API version.
                    if (apiMetadata.IsPreferredVersion)
                    {
                        if (apiMetadata.SingularName != null)
                            _metadata[apiMetadata.SingularName] = apiMetadata;

                        foreach (string shortName in apiMetadata.ShortNames)
                            _metadata[shortName] = apiMetadata;
                    }
                }
            }
        }

        /// <summary>
        ///     Create a cache key based on the specified resource kind and API version.
        /// </summary>
        /// <param name="kind">
        ///     The Kubernetes resource kind (e.g. "Pod").
        /// </param>
        /// <param name="apiVersion">
        ///     The Kubernetes resource API version (e.g. "v1").
        /// </param>
        /// <returns>
        ///     The cache key.
        /// </returns>
        static string CreateCacheKey(string kind, string apiVersion)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));

            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));

            return $"{apiVersion}/{kind}";
        }
    }
}
