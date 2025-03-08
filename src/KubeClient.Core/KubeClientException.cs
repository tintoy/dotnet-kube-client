using System;

namespace KubeClient
{
    /// <summary>
    ///     Exception raised when an error is encountered by the Kubernetes API client.
    /// </summary>
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
    }
}
