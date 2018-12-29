using HTTPlease;
using System;

namespace KubeClient
{
    using Models;

    /// <summary>
    ///     Exception raised when an error result is returned by the Kubernetes API.
    /// </summary>
#if NETSTANDARD20
    [Serializable]
#endif // NETSTANDARD20
    public class KubeApiException
        : KubeClientException
    {
        /// <summary>
        ///     Create a new <see cref="KubeClientException"/> using the information contained in a Kubernetes status model.
        /// </summary>
        /// <param name="status">
        ///     The Kubernetes <see ref="StatusV1"/> model.
        /// </param>
        public KubeApiException(StatusV1 status)
            : base(status)
        {
        }

#if NETSTANDARD2_0   

        /// <summary>
        ///     Deserialisation constructor.
        /// </summary>
        /// <param name="info">
        ///     The serialisation data store.
        /// </param>
        /// <param name="context">
        ///     A <see cref="StreamingContext"/> containing information about the origin of the serialised data.
        /// </param>
        protected KubeClientException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

#endif // NETSTANDARD2_0
    }
}
