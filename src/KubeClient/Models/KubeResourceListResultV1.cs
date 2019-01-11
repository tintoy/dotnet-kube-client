using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents the result of a Kubernetes API operation that returns a list of resources.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource returned by the operation.
    /// </typeparam>
    public class KubeResourceListResultV1<TResource>
        : KubeResultV1, IEnumerable<TResource>
        where TResource : KubeResourceV1
    {
        /// <summary>
        ///     Create a new <see cref="KubeResourceListResultV1{TResource}"/> representing the specified <typeparamref name="TResource"/> list.
        /// </summary>
        /// <param name="resources">
        ///     The <typeparamref name="TResource"/> list represented by the <see cref="KubeResourceListResultV1{TResource}"/>.
        /// </param>
        public KubeResourceListResultV1(KubeResourceListV1<TResource> resources)
            : base(DefaultStatus(resources))
        {
            Resources = resources;
        }

        /// <summary>
        ///     Create a new <see cref="KubeResourceListResultV1{TResource}"/> representing a <see cref="StatusV1"/>.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> represented by the <see cref="KubeResourceListResultV1{TResource}"/>.
        /// </param>
        public KubeResourceListResultV1(StatusV1 status)
            : base(status)
        {
        }

        /// <summary>
        ///     The <typeparamref name="TResource"/> represented by the <see cref="KubeResourceListResultV1{TResource}"/>.
        /// </summary>
        public KubeResourceListV1<TResource> Resources { get; }

        /// <summary>
        ///     The type of resource represented by the <see cref="KubeResourceListResultV1{TResource}"/>.
        /// </summary>
        public override Type ResourceType => typeof(TResource);

        /// <summary>
        ///     Implicitly convert a <see cref="KubeResourceListResultV1{TResource}"/> to a <typeparamref name="TResource"/>.
        /// </summary>
        /// <param name="result">
        ///     The <see cref="KubeResourceListResultV1{TResource}"/> to convert.
        /// </param>
        /// <returns>
        ///     The result's <see cref="KubeResourceListResultV1{TResource}.Resources"/>.
        /// </returns>
        /// <exception cref="KubeApiException">
        ///     The result <see cref="KubeResultV1.Status"/> does not represent success.
        /// </exception>
        public static implicit operator KubeResourceListV1<TResource>(KubeResourceListResultV1<TResource> result)
        {
            if (result == null)
                return null;

            if (!result.IsSuccess)
                throw new KubeApiException(result.Status);

            return result.Resources;
        }

        /// <summary>
        ///     Convert a <typeparamref name="TResource"/> to a <see cref="KubeResourceListResultV1{TResource}"/>.
        /// </summary>
        /// <param name="resources">
        ///     The <typeparamref name="TResource"/> to convert.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeResourceListResultV1{TResource}"/> that wraps the <paramref name="resources"/>.
        /// </returns>
        public static implicit operator KubeResourceListResultV1<TResource>(KubeResourceListV1<TResource> resources) => new KubeResourceListResultV1<TResource>(resources);

        /// <summary>
        ///     Convert a <see cref="StatusV1"/> to a <see cref="KubeResourceListResultV1{TResource}"/>.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> to convert.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeResourceListResultV1{TResource}"/> that wraps the <paramref name="status"/>.
        /// </returns>
        public static implicit operator KubeResourceListResultV1<TResource>(StatusV1 status)
        {
            if (status == null)
                return null;

            return new KubeResourceListResultV1<TResource>(status);
        }

        /// <summary>
        ///     The default <see cref="StatusV1"/> used when no status is available because an operation returned a list of resources.
        /// </summary>
        protected static StatusV1 DefaultStatus(KubeResourceListV1<TResource> resources)
        {
            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
            int resourceCount = resources != null ? resources.Items.Count : 0;

            return StatusV1.Success($"Result contains {resourceCount} {apiVersion}/{kind} resources.");
        }

        /// <summary>
        ///     Get a typed enumerator for the resources returned with the result.
        /// </summary>
        /// <returns>
        ///     The enumerator.
        /// </returns>
        /// <exception cref="KubeApiException">
        ///     The result <see cref="KubeResultV1.Status"/> does not represent success.
        /// </exception>
        public IEnumerator<TResource> GetEnumerator()
        {
            if (!IsSuccess)
                throw new KubeApiException(Status);

            if (Resources == null)
                return Enumerable.Empty<TResource>().GetEnumerator();

            return Resources.GetEnumerator();
        }

        /// <summary>
        ///     Get an untyped enumerator for the resources returned with the result.
        /// </summary>
        /// <returns>
        ///     The enumerator.
        /// </returns>
        /// <exception cref="KubeApiException">
        ///     The result <see cref="KubeResultV1.Status"/> does not represent success.
        /// </exception>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
