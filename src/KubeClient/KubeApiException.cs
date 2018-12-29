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
        ///     Create a new <see cref="KubeApiException"/> using the information contained in a Kubernetes status model.
        /// </summary>
        /// <param name="status">
        ///     The Kubernetes <see ref="StatusV1"/> model.
        /// </param>
        public KubeApiException(StatusV1 status)
            : base(GetExceptionMessage(status))
        {
            Status = status;
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiException"/> using the information contained in a Kubernetes status model.
        /// </summary>
        /// <param name="message">
        ///     The exception message.
        /// </param>
        /// <param name="status">
        ///     The Kubernetes <see ref="StatusV1"/> model.
        /// </param>
        public KubeApiException(string message, StatusV1 status)
            : base(message + Environment.NewLine + GetExceptionMessage(status))
        {
            Status = status;
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiException"/> using the information contained in a Kubernetes status model.
        /// </summary>
        /// <param name="status">
        ///     The Kubernetes <see ref="StatusV1"/> model.
        /// </param>
        /// <param name="innerException">
        ///     The exception that caused the current exception to be raised.
        /// </param>
        public KubeApiException(StatusV1 status, Exception innerException)
            : base(GetExceptionMessage(status), innerException)
        {
            if (innerException == null)
                throw new ArgumentNullException(nameof(innerException));

            Status = status;
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiException"/> with the specified message.
        /// </summary>
        /// <param name="message">
        ///     The exception message.
        /// </param>
        /// <param name="innerException">
        ///     The exception that caused the current exception to be raised.
        /// </param>
        public KubeApiException(string message, HttpRequestException<StatusV1> innerException)
            : base(message + Environment.NewLine + GetExceptionMessage(innerException?.Response), innerException)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'message'.", nameof(message));
            
            if (innerException == null)
                throw new ArgumentNullException(nameof(innerException));

            Status = innerException.Response;
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiException"/> from an <see cref="HttpRequestException{TResponse}"/>.
        /// </summary>
        /// <param name="requestException">
        ///     The exception that caused the current exception to be raised.
        /// </param>
        public KubeApiException(HttpRequestException<StatusV1> requestException)
            : base(GetExceptionMessage(requestException?.Response), requestException)
        {
            if (requestException == null)
                throw new ArgumentNullException(nameof(requestException));

            Status = requestException.Response;
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

        /// <summary>
        ///     A Kubernetes <see cref="StatusV1"/> model that (if present) contains more information about the error.
        /// </summary>
        public StatusV1 Status { get; protected set; }

        /// <summary>
        ///     Does the exception have a <see cref="Status"/> model available?
        /// </summary>
        public bool HasStatus => Status != null;

        /// <summary>
        ///     The Kubernetes reason code (if available from <see cref="Status"/>).
        /// </summary>
        public string StatusReason => Status?.Reason;

        /// <summary>
        ///     The Kubernetes error message (if available from <see cref="Status"/>).
        /// </summary>
        public string StatusMessage => Status?.Message;

        /// <summary>
        ///     Generate an exception message from a Kubernetes status model.
        /// </summary>
        /// <param name="status">
        ///     The Kubernetes <see cref="StatusV1"/> model.
        /// </param>
        /// <returns>
        ///     The exception message.
        /// </returns>
        protected static string GetExceptionMessage(StatusV1 status)
        {
            if (status == null)
                return DefaultMessage;

            if (!String.IsNullOrWhiteSpace(status.Reason))
                return $"{status.Reason}: {status.Message}";

            if (!String.IsNullOrWhiteSpace(status.Message))
                return status.Message;

            return DefaultMessage;
        }
    }
}
