using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents the result of a Kubernetes API operation that returns a resource.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource returned by the operation.
    /// </typeparam>
    public class KubeResourceResultV1<TResource>
        : KubeResultV1
        where TResource : KubeResourceV1
    {
        /// <summary>
        ///     Create a new <see cref="KubeResourceResultV1{TResource}"/> representing the specified <typeparamref name="TResource"/>.
        /// </summary>
        /// <param name="resource">
        ///     The <typeparamref name="TResource"/> represented by the <see cref="KubeResourceResultV1{TResource}"/>.
        /// </param>
        public KubeResourceResultV1(TResource resource)
            : base(DefaultStatus)
        {
            Resource = resource;
        }

        /// <summary>
        ///     Create a new <see cref="KubeResourceResultV1{TResource}"/> representing a <see cref="StatusV1"/>.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> represented by the <see cref="KubeResourceResultV1{TResource}"/>.
        /// </param>
        public KubeResourceResultV1(StatusV1 status)
            : base(status)
        {
        }

        /// <summary>
        ///     The <typeparamref name="TResource"/> represented by the <see cref="KubeResourceResultV1{TResource}"/>.
        /// </summary>
        public TResource Resource { get; }

        /// <summary>
        ///     The type of resource represented by the <see cref="KubeResourceResultV1{TResource}"/>.
        /// </summary>
        public override Type ResourceType => typeof(TResource);

        /// <summary>
        ///     Implicitly convert a <see cref="KubeResourceResultV1{TResource}"/> to a <typeparamref name="TResource"/>.
        /// </summary>
        /// <param name="result">
        ///     The <see cref="KubeResourceResultV1{TResource}"/> to convert.
        /// </param>
        /// <returns>
        ///     The result's <see cref="KubeResourceResultV1{TResource}.Resource"/>.
        /// </returns>
        /// <exception cref="KubeApiException">
        ///     The result <see cref="KubeResultV1.Status"/> does not represent success.
        /// </exception>
        public static implicit operator TResource(KubeResourceResultV1<TResource> result)
        {
            if (result == null)
                return null;

            if (!result.IsSuccess)
                throw new KubeApiException(result.Status);

            return result.Resource;
        }

        /// <summary>
        ///     Convert a <typeparamref name="TResource"/> to a <see cref="KubeResourceResultV1{TResource}"/>.
        /// </summary>
        /// <param name="resource">
        ///     The <typeparamref name="TResource"/> to convert.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeResourceResultV1{TResource}"/> that wraps the <paramref name="resource"/>.
        /// </returns>
        public static implicit operator KubeResourceResultV1<TResource>(TResource resource) => new KubeResourceResultV1<TResource>(resource);

        /// <summary>
        ///     Convert a <see cref="StatusV1"/> to a <see cref="KubeResourceResultV1{TResource}"/>.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> to convert.
        /// </param>
        /// <returns>
        ///     A <see cref="KubeResourceResultV1{TResource}"/> that wraps the <paramref name="status"/>.
        /// </returns>
        public static implicit operator KubeResourceResultV1<TResource>(StatusV1 status)
        {
            if (status == null)
                return null;

            return new KubeResourceResultV1<TResource>(status);
        }

        /// <summary>
        ///     The default <see cref="StatusV1"/> used when no status is available because an operation returned a status.
        /// </summary>
        protected static StatusV1 DefaultStatus
        {
            get
            {
                (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();

                return StatusV1.Success($"Result contains a {apiVersion}/{kind} resource.");
            }
        }
    }
}
