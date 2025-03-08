using System;

namespace KubeClient.Http
{
    /// <summary>
	///		Exception raised when a <see cref="UriTemplate"/> is invalid or is missing required information.
	/// </summary>
	public class UriTemplateException
        : KubeClientException
    {
        /// <summary>
        ///		Create a new <see cref="UriTemplateException"/>.
        /// </summary>
        /// <param name="message">
        ///		The exception message.
        /// </param>
        public UriTemplateException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///		Create a new <see cref="UriTemplateException"/>.
        /// </summary>
        /// <param name="innerException">
        ///		The exception that caused this exception to be raised.
        /// </param>
        /// <param name="message">
        ///		The exception message.
        /// </param>
        public UriTemplateException(Exception innerException, string message)
            : base(message, innerException)
        {
        }
    }
}
