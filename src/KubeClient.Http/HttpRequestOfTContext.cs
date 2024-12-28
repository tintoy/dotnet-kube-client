using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace KubeClient.Http
{
	using Utilities;
	using ValueProviders;

	using RequestProperties = ImmutableDictionary<string, object>;

	/// <summary>
	///		A template for an HTTP request that resolves deferred values from an instance of <typeparamref name="TContext"/>.
	/// </summary>
	/// <typeparam name="TContext">
	///		The type of object used as a context for resolving deferred values.
	/// </typeparam>
	public class HttpRequest<TContext>
		: HttpRequestBase, IHttpRequest<TContext>
	{
		#region Constants

		/// <summary>
		///		The base properties for <see cref="HttpRequest"/>s.
		/// </summary>
		static readonly RequestProperties BaseProperties =
			new Dictionary<string, object>
			{
				[nameof(RequestActions)] = ImmutableList<RequestAction<TContext>>.Empty,
				[nameof(ResponseActions)] = ImmutableList<ResponseAction<TContext>>.Empty,
				[nameof(TemplateParameters)] = ImmutableDictionary<string, IValueProvider<TContext, string>>.Empty,
				[nameof(QueryParameters)] = ImmutableDictionary<string, IValueProvider<TContext, string>>.Empty
			}
			.ToImmutableDictionary();

		/// <summary>
		///		An empty <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		public static HttpRequest<TContext> Empty = new HttpRequest<TContext>(BaseProperties);

		/// <summary>
		///		The default factory for <see cref="HttpRequest{TContext}"/>s.
		/// </summary>
		public static HttpRequestFactory<TContext> Factory { get; } = new HttpRequestFactory<TContext>(Empty);

		#endregion // Constants

		#region Construction

		/// <summary>
		///		Create a new HTTP request.
		/// </summary>
		/// <param name="properties">
		///		The request properties.
		/// </param>
		HttpRequest(ImmutableDictionary<string, object> properties)
			: base(properties)
		{
			EnsurePropertyType<ImmutableList<RequestAction<TContext>>>(
				propertyName: nameof(RequestActions)
			);
			EnsurePropertyType<ImmutableDictionary<string, IValueProvider<TContext, string>>>(
				propertyName: nameof(TemplateParameters)
			);
			EnsurePropertyType<ImmutableDictionary<string, IValueProvider<TContext, string>>>(
				propertyName: nameof(QueryParameters)
			);
		}

		/// <summary>
		///		Create a new HTTP request with the specified request URI.
		/// </summary>
		/// <param name="requestUri">
		///		The request URI (can be relative or absolute).
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> Create(string requestUri) => Factory.Create(requestUri);

		/// <summary>
		///		Create a new HTTP request with the specified request URI.
		/// </summary>
		/// <param name="requestUri">
		///		The request URI (can be relative or absolute).
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> Create(Uri requestUri) => Factory.Create(requestUri);

		#endregion // Construction

		#region Properties

		/// <summary>
		///		Actions (if any) to perform on the outgoing request message.
		/// </summary>
		public ImmutableList<RequestAction<TContext>> RequestActions => GetProperty<ImmutableList<RequestAction<TContext>>>();

		/// <summary>
		///		Actions (if any) to perform on the incoming response message.
		/// </summary>
		public ImmutableList<ResponseAction<TContext>> ResponseActions => GetProperty<ImmutableList<ResponseAction<TContext>>>();

		/// <summary>
		///     The request's URI template parameters (if any).
		/// </summary>
		public ImmutableDictionary<string, IValueProvider<TContext, string>> TemplateParameters => GetProperty<ImmutableDictionary<string, IValueProvider<TContext, string>>>();

		/// <summary>
		///     The request's query parameters (if any).
		/// </summary>
		public ImmutableDictionary<string, IValueProvider<TContext, string>> QueryParameters => GetProperty<ImmutableDictionary<string, IValueProvider<TContext, string>>>();

		#endregion // Properties

		#region Invocation

		/// <summary>
		///     Build and configure a new HTTP request message.
		/// </summary>
		/// <param name="httpMethod">
		///     The HTTP request method to use.
		/// </param>
		/// <param name="context">
		///		The object used as a context for resolving deferred template values.
		/// </param>
		/// <param name="body">
		///     Optional <see cref="HttpContent" /> representing the request body.
		/// </param>
		/// <param name="baseUri">
		///     An optional base URI to use if the request does not already have an absolute request URI.
		/// </param>
		/// <returns>
		///     The configured <see cref="HttpRequestMessage" />.
		/// </returns>
		public HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, TContext context, HttpContent body = null, Uri baseUri = null)
		{
			if (httpMethod == null)
				throw new ArgumentNullException(nameof(httpMethod));

			// Ensure we have an absolute URI.
			Uri requestUri = Uri;
			if (requestUri == null)
				throw new InvalidOperationException("Cannot build a request message; the request does not have a URI.");

			if (!requestUri.IsAbsoluteUri)
			{
				if (baseUri == null)
					throw new InvalidOperationException("Cannot build a request message; the request does not have an absolute request URI, and no base URI was supplied.");

				// Make relative to base URI.
				requestUri = baseUri.AppendRelativeUri(requestUri);
			}
			else
			{
				// Extract base URI to which request URI is already (by definition) relative.
				baseUri = new Uri(
					requestUri.GetComponents(
						UriComponents.Scheme | UriComponents.StrongAuthority,
						UriFormat.UriEscaped
					)
				);
			}

			if (IsUriTemplate)
			{
				UriTemplate template = new UriTemplate(
					requestUri.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped)
				);

				IDictionary<string, string> templateParameterValues = GetTemplateParameterValues(context);

				requestUri = template.Populate(baseUri, templateParameterValues);
			}

			// Merge in any other query parameters defined directly on the request.
			requestUri = MergeQueryParameters(requestUri, context);

			HttpRequestMessage requestMessage = null;
			try
			{
				requestMessage = new HttpRequestMessage(httpMethod, requestUri);
				SetStandardMessageProperties(requestMessage);

				if (body != null)
					requestMessage.Content = body;

				List<Exception> configurationActionExceptions = new List<Exception>();
				foreach (RequestAction<TContext> requestAction in RequestActions)
				{
					if (requestAction == null)
						continue;

					try
					{
						requestAction(requestMessage, context);
					}
					catch (Exception eConfigurationAction)
					{
						configurationActionExceptions.Add(eConfigurationAction);
					}
				}

				if (configurationActionExceptions.Count > 0)
				{
					throw new AggregateException(
						"One or more unhandled exceptions were encountered while configuring the outgoing request message.",
						configurationActionExceptions
					);
				}
			}
			catch
			{
				using (requestMessage)
				{
					throw;
				}
			}

			return requestMessage;
		}

		#endregion // Invocation

		#region IHttpRequest

		/// <summary>
		///		Actions (if any) to perform on the outgoing request message.
		/// </summary>
		IReadOnlyList<RequestAction<TContext>> IHttpRequestProperties<TContext>.RequestActions => RequestActions;

		/// <summary>
		///		Actions (if any) to perform on the outgoing request message.
		/// </summary>
		IReadOnlyList<ResponseAction<TContext>> IHttpRequestProperties<TContext>.ResponseActions => ResponseActions;

		/// <summary>
		///     The request's URI template parameters (if any).
		/// </summary>
		IReadOnlyDictionary<string, IValueProvider<TContext, string>> IHttpRequestProperties<TContext>.TemplateParameters => TemplateParameters;

		/// <summary>
		///     The request's query parameters (if any).
		/// </summary>
		IReadOnlyDictionary<string, IValueProvider<TContext, string>> IHttpRequestProperties<TContext>.QueryParameters => QueryParameters;

		#endregion // IHttpRequest

		#region Cloning

		/// <summary>
		///		Clone the request.
		/// </summary>
		/// <param name="modifications">
		///		A delegate that performs modifications to the request properties.
		/// </param>
		/// <returns>
		///		The cloned request.
		/// </returns>
		public new HttpRequest<TContext> Clone(Action<IDictionary<string, object>> modifications)
		{
			if (modifications == null)
				throw new ArgumentNullException(nameof(modifications));

			return (HttpRequest<TContext>)base.Clone(modifications);
		}

		/// <summary>
		///		Create a new instance of the HTTP request using the specified properties.
		/// </summary>
		/// <param name="requestProperties">
		///		The request properties.
		/// </param>
		/// <returns>
		///		The new HTTP request instance.
		/// </returns>
		protected override HttpRequestBase CreateInstance(ImmutableDictionary<string, object> requestProperties)
		{
			return new HttpRequest<TContext>(requestProperties);
		}

		#endregion // Cloning

		#region Helpers

		/// <summary>
		///		Merge the request's query parameters (if any) into the request URI.
		/// </summary>
		/// <param name="requestUri">
		///		The request URI.
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> from which parameter values will be resolved.
		/// </param>
		/// <returns>
		///		The request URI with query parameters merged into it.
		/// </returns>
		Uri MergeQueryParameters(Uri requestUri, TContext context)
		{
			if (requestUri == null)
				throw new ArgumentNullException(nameof(requestUri));

			if (QueryParameters.Count == 0)
				return requestUri;

			NameValueCollection queryParameters = requestUri.ParseQueryParameters();
			foreach (KeyValuePair<string, IValueProvider<TContext, string>> queryParameter in QueryParameters)
			{
				string queryParameterValue = queryParameter.Value.Get(context);
				if (queryParameterValue != null)
					queryParameters[queryParameter.Key] = queryParameterValue;
				else
					queryParameters.Remove(queryParameter.Key);
			}

			return requestUri.WithQueryParameters(queryParameters);
		}

		/// <summary>
		///		Get a dictionary mapping template parameters (if any) to their current values.
		/// </summary>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> from which parameter values will be resolved.
		/// </param>
		/// <returns>
		///		A dictionary of key / value pairs (any parameters whose value-getters return null will be omitted).
		/// </returns>
		IDictionary<string, string> GetTemplateParameterValues(TContext context)
		{
			return
				TemplateParameters.Select(templateParameter =>
				{
					Debug.Assert(templateParameter.Value != null);

					return new
					{
						templateParameter.Key,
						Value = templateParameter.Value.Get(context)
					};
				})
				.Where(
					templateParameter => templateParameter.Value != null
				)
				.ToDictionary(
					templateParameter => templateParameter.Key,
					templateParameter => templateParameter.Value
				);
		}

		/// <summary>
		///		Configure standard properties for the specified <see cref="HttpRequestMessage"/>.
		/// </summary>
		/// <param name="requestMessage">
		///		The <see cref="HttpRequestMessage"/>.
		/// </param>
		void SetStandardMessageProperties(HttpRequestMessage requestMessage)
		{
			if (requestMessage == null)
				throw new ArgumentNullException(nameof(requestMessage));

            // TODO: Switch to HttpRequestOptions once we drop netstandard2.1 support.
#pragma warning disable CS0618 // Type or member is obsolete
            requestMessage.Properties[MessageProperties.Request] = this;
#pragma warning restore CS0618 // Type or member is obsolete
        }

		#endregion // Helpers
	}
}
