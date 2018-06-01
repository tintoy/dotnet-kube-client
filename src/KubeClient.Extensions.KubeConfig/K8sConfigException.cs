using System;

namespace KubeClient
{
    /// <summary>
    ///     Exception raised when invalid Kubernetes client configuration is encountered.
    /// </summary>
    public class K8sConfigException
        : KubeClientException
    {
        /// <summary>
        ///     Create a new <see cref="K8sConfigException"/>.
        /// </summary>
        /// <param name="message">
        ///     The exception message.
        /// </param>
        public K8sConfigException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Create a new <see cref="K8sConfigException"/>, as caused by a previous exception.
        /// </summary>
        /// <param name="message">
        ///     The exception message.
        /// </param>
        /// <param name="innerException">
        ///     The exception that caused the <see cref="K8sConfigException"/> to be raised.
        /// </param>
        public K8sConfigException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
