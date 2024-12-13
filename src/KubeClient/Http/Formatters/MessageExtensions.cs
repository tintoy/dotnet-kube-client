using System;
using System.Net.Http;

namespace KubeClient.Http
{
	using Formatters;

    // TODO: Consider migrating to HttpRequestOptions (requires net9.0 or newer).

    /// <summary>
    ///		Formatter-related extension methods for <see cref="HttpRequestMessage"/> / <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class FormatterMessageExtensions
    {
		/// <summary>
		///		Get the message's <see cref="IFormatterCollection"/> (if any).
		/// </summary>
		/// <param name="message">
		///		The HTTP request message.
		/// </param>
		/// <returns>
		///		The content formatters, or <c>null</c> if the message does not have any associated formatters.
		/// </returns>
		public static IFormatterCollection GetFormatters(this HttpRequestMessage message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

#pragma warning disable CS0618 // Type or member is obsolete (HttpRequestMessage.Properties is obsolete in net9.0, replaced by HttpRequestMessage.Options)
            object contentFormatters;
			message.Properties.TryGetValue(MessageProperties.ContentFormatters, out contentFormatters);
#pragma warning restore CS0618 // Type or member is obsolete (HttpRequestMessage.Properties is obsolete in net9.0, replaced by HttpRequestMessage.Options)

            return (IFormatterCollection)contentFormatters;
		}

		/// <summary>
		///		Get the message's <see cref="IFormatterCollection"/> (if any).
		/// </summary>
		/// <param name="message">
		///		The HTTP request message.
		/// </param>
		/// <returns>
		///		The content formatters, or <c>null</c> if the message does not have any associated formatters.
		/// </returns>
		/// <remarks>
		///		Can only be called on an <see cref="HttpResponseMessage"/> whose <see cref="HttpResponseMessage.RequestMessage"/> contains a valid <see cref="HttpRequestMessage"/>.
		/// </remarks>
		public static IFormatterCollection GetFormatters(this HttpResponseMessage message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			HttpRequestMessage requestMessage = message.RequestMessage;
			if (requestMessage == null)
				throw new InvalidOperationException("This operation is only valid on a response message produced by invoking an HttpRequest (the response message does not have an associated request message).");

#pragma warning disable CS0618 // Type or member is obsolete (HttpRequestMessage.Properties is obsolete in net9.0, replaced by HttpRequestMessage.Options)
            object contentFormatters;
            message.RequestMessage.Properties.TryGetValue(MessageProperties.ContentFormatters, out contentFormatters);
#pragma warning restore CS0618 // Type or member is obsolete (HttpRequestMessage.Properties is obsolete in net9.0, replaced by HttpRequestMessage.Options)

            return (IFormatterCollection)contentFormatters;
		}

		/// <summary>
		///		Set the message's <see cref="IFormatterCollection"/>.
		/// </summary>
		/// <param name="message">
		///		The HTTP request message.
		/// </param>
		/// <param name="contentFormatters">
		///		The content formatters (if any).
		/// </param>
		public static void SetFormatters(this HttpRequestMessage message, IFormatterCollection contentFormatters)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

#pragma warning disable CS0618 // Type or member is obsolete (HttpRequestMessage.Properties is obsolete in net9.0, replaced by HttpRequestMessage.Options)
            message.Properties[MessageProperties.ContentFormatters] = contentFormatters;
#pragma warning restore CS0618 // Type or member is obsolete (HttpRequestMessage.Properties is obsolete in net9.0, replaced by HttpRequestMessage.Options)
        }
    }
}
