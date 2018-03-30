using HTTPlease;
using System;
using System.Runtime.Serialization;

namespace KubeClient
{
    using Models;

    /// <summary>
    ///     Exception raised when an error is encountered by the Kubernetes API client.
    /// </summary>
#if NETSTANDARD20
    [Serializable]
#endif // NETSTANDARD20
    public class KubeClientException
        : Exception
    {
        /// <summary>
        ///     The default exception message used when no message was available.
        /// </summary>
        public static readonly string DefaultMessage = "An unexpected error has occurred (error description is not available).";

        /// <summary>
        ///     Create a new <see cref="KubeClientException"/>.
        /// </summary>
        /// <param name="message">
        ///     The exception message.
        /// </param>
        public KubeClientException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Create a new <see cref="KubeClientException"/>, as caused by a previous exception.
        /// </summary>
        /// <param name="message">
        ///     The exception message.
        /// </param>
        /// <param name="innerException">
        ///     The exception that caused the <see cref="KubeClientException"/> to be raised.
        /// </param>
        public KubeClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Create a new <see cref="KubeClientException"/> using the information contained in a Kubernetes status model.
        /// </summary>
        /// <param name="status">
        ///     The Kubernetes <see ref="StatusV1"/> model.
        /// </param>
        public KubeClientException(StatusV1 status)
            : base(GetExceptionMessage(status))
        {
            Status = status;
        }

        /// <summary>
        ///     Create a new <see cref="KubeClientException"/> using the information contained in a Kubernetes status model.
        /// </summary>
        /// <param name="status">
        ///     The Kubernetes <see ref="StatusV1"/> model.
        /// </param>
        /// <param name="innerException">
        ///     The exception that caused the current exception to be raised.
        /// </param>
        public KubeClientException(StatusV1 status, Exception innerException)
            : base(GetExceptionMessage(status), innerException)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));
            
            if (innerException == null)
                throw new ArgumentNullException(nameof(innerException));

            Status = status;
        }

        /// <summary>
        ///     Create a new <see cref="KubeClientException"/> with the specified message.
        /// </summary>
        /// <param name="message">
        ///     The exception message.
        /// </param>
        /// <param name="innerException">
        ///     The exception that caused the current exception to be raised.
        /// </param>
        public KubeClientException(string message, HttpRequestException<StatusV1> innerException)
            : base(message, innerException)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'message'.", nameof(message));
            
            if (innerException == null)
                throw new ArgumentNullException(nameof(innerException));

            Status = innerException.Response;
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
        public StatusV1 Status { get; }

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
