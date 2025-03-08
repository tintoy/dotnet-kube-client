using Newtonsoft.Json;
using System;

namespace KubeClient.Http
{
	using Formatters.Json;

	/// <summary>
	///		Formatter-related extension methods for <see cref="HttpRequest"/> / <see cref="HttpRequest{TContext}"/>.
	/// </summary>
	public static class NewtonsoftJsonFormatterRequestExtensions
	{
		/// <summary>
		///		Create a copy of the <see cref="HttpRequest"/>, configuring it to only use the JSON formatters.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="serializerSettings">
		///		<see cref="JsonSerializerSettings"/> used to configure the formatter's behaviour.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest"/>.
		/// </returns>
		public static HttpRequest UseJson(this HttpRequest request, JsonSerializerSettings serializerSettings = null)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			// TODO: Clear all existing formatters, first.

			return request.WithFormatter(new NewtonsoftJsonFormatter
			{
				SerializerSettings = serializerSettings
			});
		}

		/// <summary>
		///		Create a copy of the <see cref="HttpRequest{TContext}"/>, configuring it to only use the JSON formatters.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="serializerSettings">
		///		<see cref="JsonSerializerSettings"/> used to configure the formatter's behaviour.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest"/>.
		/// </returns>
		public static HttpRequest<TContext> UseJson<TContext>(this HttpRequest<TContext> request, JsonSerializerSettings serializerSettings = null)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			// TODO: Clear all existing formatters, first.

			return request.WithFormatter(new NewtonsoftJsonFormatter
			{
				SerializerSettings = serializerSettings
			});
		}
	}
}
