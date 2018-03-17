using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KubeClient.Models
{
    /// <summary>
    ///     The base class for Kubernetes resource lists.
    /// </summary>
    public abstract class KubeResourceListV1
        : KubeObjectV1
    {
        /// <summary>
        ///     Standard list metadata. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
        /// </summary>
        [JsonProperty("metadata")]
        public ListMetaV1 Metadata { get; set; }

        /// <summary>
        ///     The CLR type corresponding to the resources contained in the list.
        /// </summary>
        [JsonIgnore]
        public abstract Type ResourceType { get; }
    }

    /// <summary>
    ///     The base class for Kubernetes resource lists where the resource type is known.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource contained in the list.
    /// </typeparam>
    public abstract class KubeResourceListV1<TResource>
        : KubeResourceListV1, IEnumerable<TResource>
    {
        /// <summary>
        ///     The CLR type corresponding to the resources contained in the list.
        /// </summary>
        [JsonIgnore]
        public override Type ResourceType => typeof(TResource);

        /// <summary>
        ///     The list's resources.
        /// </summary>
        public abstract List<TResource> Items { get; }

        /// <summary>
        ///     Get a typed enumerator for the list's resources.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerator{T}"/>.
        /// </returns>
        public IEnumerator<TResource> GetEnumerator() => Items.GetEnumerator();

        /// <summary>
        ///     Get an untyped enumerator for the list's resources.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
