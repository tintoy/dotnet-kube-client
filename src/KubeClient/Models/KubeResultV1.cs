using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents the result of a Kubernetes API operation.
    /// </summary>
    public abstract class KubeResultV1
    {
        /// <summary>
        ///     A <see cref="StatusV1"/> representing the operation status.
        /// </summary>
        public StatusV1 Status { get; protected set; }

        /// <summary>
        ///     Does the result represent a successful operation?
        /// </summary>
        public bool IsSuccess => Status?.IsSuccess ?? false;

        /// <summary>
        ///     The type of resource represented by the <see cref="KubeResultV1"/>.
        /// </summary>
        public abstract Type ResourceType { get; }

        /// <summary>
        ///     Create a new <see cref="KubeResultV1"/>.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> that represents the operation result.
        /// </param>
        protected KubeResultV1(StatusV1 status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));

            Status = status;
        }

        /// <summary>
        ///     Implicitly convert a <see cref="KubeResultV1"/> to a <see cref="StatusV1"/>.
        /// </summary>
        /// <param name="result">
        ///     The <see cref="KubeResultV1"/> to convert.
        /// </param>
        /// <returns>
        ///     The result's <see cref="KubeResultV1.Status"/>.
        /// </returns>
        public static implicit operator StatusV1(KubeResultV1 result)
        {
            if (result == null)
                return null;

            return result.Status;
        }
    }
}