using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
        ///     Populate the cache.
        /// </summary>
        /// <param name="kubeClient">
        ///     The <see cref="KubeClient"/> used to retrieve API metadata.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task Load(IKubeApiClient kubeClient, CancellationToken cancellationToken = default)
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
                    List<string> groupVersions = new List<string>();
                    if (apiGroupPrefix == "api")
                    {
                        groupVersions.Add("v1");
                    }
                    else
                    {
                        groupVersions.AddRange(
                            apiGroup.Versions.Select(
                                version => version.GroupVersion
                            )
                        );
                    }

                    foreach (string groupVersion in groupVersions)
                    {
                        APIResourceListV1 apis = await kubeClient.APIResourcesV1().List(apiGroupPrefix, groupVersion, cancellationToken);
                        foreach (var apisForKind in apis.Resources.GroupBy(api => api.Kind))
                        {
                            string kind = apisForKind.Key;
                            string singularName = null;
                            IReadOnlyCollection<string> shortNames = new string[0];

                            var apiPaths = new List<KubeApiPathMetadata>();

                            bool isPreferredVersion = (groupVersion == apiGroup.PreferredVersion.GroupVersion);

                            foreach (APIResourceV1 api in apisForKind)
                            {
                                string apiPath = $"{apiGroupPrefix}/{apiGroup.PreferredVersion.GroupVersion}/{api.Name}";

                                apiPaths.Add(
                                    new KubeApiPathMetadata(apiPath,
                                        isNamespaced: false,
                                        verbs: api.Verbs.ToArray()
                                    )
                                );

                                if (api.Namespaced)
                                {
                                    string namespacedApiPath = $"{apiGroupPrefix}/{apiGroup.PreferredVersion.GroupVersion}/namespaces/{{Namespace}}/{api.Name}";

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

                            loadedMetadata.Add(
                                new KubeApiMetadata(kind, groupVersion, singularName, shortNames, isPreferredVersion, apiPaths)
                            );
                        }
                    }
                }
            }

            cancellationToken.ThrowIfCancellationRequested();

            lock (_stateLock)
            {
                Clear();

                foreach (KubeApiMetadata apiMetadata in loadedMetadata)
                {
                    string cacheKey = CreateCacheKey(apiMetadata.Kind, apiMetadata.ApiVersion);
                    _metadata[cacheKey] = apiMetadata;

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
