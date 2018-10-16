using System;
using System.Collections.Generic;
using System.Linq;

namespace KubeClient.ApiMetadata
{
    /// <summary>
    ///     Metadata for a Kubernetes resource API.
    /// </summary>
    public class KubeApiMetadata
    {
        /// <summary>
        ///     Create new Kubernetes resource API metadata.
        /// </summary>
        /// <param name="kind">
        ///     The resource kind.
        /// </param>
        /// <param name="apiVersion">
        ///     The resource API version.
        /// </param>
        /// <param name="singularName">
        ///     The singular name (if any) for the resource.
        /// </param>
        /// <param name="shortNames">
        ///     Short names (if any) for the resource.
        /// </param>
        /// <param name="isPreferredVersion">
        ///     Is this the currently-preferred version of the API?
        /// </param>
        /// <param name="paths">
        ///     The metadata for the API's paths.
        /// 
        ///     At least 1 path must be supplied.
        /// </param>
        public KubeApiMetadata(string kind, string apiVersion, string singularName, IReadOnlyCollection<string> shortNames, bool isPreferredVersion, IReadOnlyList<KubeApiPathMetadata> paths)
        {
            if (String.IsNullOrWhiteSpace(kind))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'kind'.", nameof(kind));
            
            if (String.IsNullOrWhiteSpace(apiVersion))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiVersion'.", nameof(apiVersion));
            
            if (String.IsNullOrWhiteSpace(singularName))
                singularName = null;

            if (shortNames == null)
                throw new ArgumentNullException(nameof(shortNames));
            
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));

            if (paths.Count == 0)
                throw new ArgumentException("Metadata for a Kubernetes resource API must have at least one path.", nameof(paths));

            Kind = kind;
            ApiVersion = apiVersion;
            SingularName = singularName;
            ShortNames = shortNames;
            IsPreferredVersion = isPreferredVersion;
            PathMetadata = paths;
        }

        /// <summary>
        ///     The resource kind.
        /// </summary>
        public string Kind { get; }

        /// <summary>
        ///     The resource API version.
        /// </summary>
        public string ApiVersion { get; }

        /// <summary>
        ///     The singular name (if any) for the resource.
        /// </summary>
        public string SingularName { get; }

        /// <summary>
        ///     Short names (if any) for the resource.
        /// </summary>
        public IReadOnlyCollection<string> ShortNames { get; }

        /// <summary>
        ///     Is this the currently-preferred version of the API?
        /// </summary>
        public bool IsPreferredVersion { get; }

        /// <summary>
        ///     The metadata for the API's primary (non-namespaced) path.
        /// </summary>
        public string PrimaryPath => PrimaryPathMetadata?.Path;

        /// <summary>
        ///     The metadata for the API's primary namespaced path.
        /// </summary>
        public string PrimaryNamespacedPath => PrimaryNamespacedPathMetadata?.Path;

        /// <summary>
        ///     The metadata for the API's primary (non-namespaced) path.
        /// </summary>
        public KubeApiPathMetadata PrimaryPathMetadata => PathMetadata.FirstOrDefault(path => !path.IsNamespaced);

        /// <summary>
        ///     The metadata for the API's primary namespaced path.
        /// </summary>
        public KubeApiPathMetadata PrimaryNamespacedPathMetadata => PathMetadata.FirstOrDefault(path => path.IsNamespaced);

        /// <summary>
        ///     The metadata for the API's path(s).
        /// </summary>
        public IReadOnlyList<KubeApiPathMetadata> PathMetadata { get; }
    }
}
