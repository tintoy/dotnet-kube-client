using System;
using System.Collections.Generic;

namespace KubeClient.ApiMetadata
{
    /// <summary>
    ///     Metadata for a specific path in a Kubernetes resource API.
    /// </summary>
    public class KubeApiPathMetadata
    {
        /// <summary>
        ///     Create new Kubernetes resource API path metadata.
        /// </summary>
        /// <param name="path">
        ///     The API path.
        /// </param>
        /// <param name="isNamespaced">
        ///     Is the path namespaced?
        /// </param>
        /// <param name="verbs">
        ///     The verbs supported on the path.
        /// </param>
        public KubeApiPathMetadata(string path, bool isNamespaced, IReadOnlyCollection<string> verbs)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'path'.", nameof(path));

            if (verbs == null)
                throw new ArgumentNullException(nameof(verbs));

            Path = path;
            Verbs = verbs;
            IsNamespaced = isNamespaced;
        }

        /// <summary>
        ///     The API path.
        /// </summary>
        public string Path { get; }

        /// <summary>
        ///     Is the path namespaced?
        /// </summary>
        public bool IsNamespaced { get; }

        /// <summary>
        ///     The verbs supported on the path.
        /// </summary>
        public IReadOnlyCollection<string> Verbs { get; }
    }
}
