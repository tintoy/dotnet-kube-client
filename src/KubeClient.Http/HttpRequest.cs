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
    ///		A template for an HTTP request.
    /// </summary>
    public sealed class HttpRequest
        : HttpRequestBase, IHttpRequest, IHttpRequest<object>
    {
        #region Constants

        /// <summary>
        ///		The <see cref="Object"/> used a context for all untyped HTTP requests.
        /// </summary>
        internal static readonly object DefaultContext = new object();

        /// <summary>
        ///		The base properties for <see cref="HttpRequest"/>s.
        /// </summary>
        static readonly RequestProperties BaseProperties =
            new Dictionary<string, object>
            {
                [nameof(RequestActions)] = ImmutableList<RequestAction<object>>.Empty,
                [nameof(ResponseActions)] = ImmutableList<ResponseAction<object>>.Empty,
                [nameof(TemplateParameters)] = ImmutableDictionary<string, IValueProvider<object, string>>.Empty,
                [nameof(QueryParameters)] = ImmutableDictionary<string, IValueProvider<object, string>>.Empty
            }
            .ToImmutableDictionary();

        /// <summary>
        ///		An empty <see cref="HttpRequest"/>.
        /// </summary>
        public static readonly HttpRequest Empty = new HttpRequest(BaseProperties);

        /// <summary>
        ///		The default factory for <see cref="HttpRequest"/>s.
        /// </summary>
        public static HttpRequestFactory Factory { get; } = new HttpRequestFactory(Empty);

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
            EnsurePropertyType<ImmutableList<RequestAction<object>>>(
                propertyName: nameof(RequestActions)
            );
            EnsurePropertyType<ImmutableDictionary<string, IValueProvider<object, string>>>(
                propertyName: nameof(TemplateParameters)
            );
            EnsurePropertyType<ImmutableDictionary<string, IValueProvider<object, string>>>(
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
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest Create(string requestUri) => Factory.Create(requestUri);

        /// <summary>
        ///		Create a new HTTP request with the specified request URI.
        /// </summary>
        /// <param name="requestUri">
        ///		The request URI (can be relative or absolute).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest Create(Uri requestUri) => Factory.Create(requestUri);

        #endregion // Construction

        #region Properties

        /// <summary>
        ///		Actions (if any) to perform on the outgoing request message.
        /// </summary>
        public ImmutableList<RequestAction<object>> RequestActions => GetProperty<ImmutableList<RequestAction<object>>>();

        /// <summary>
        ///		Actions (if any) to perform on the incoming response message.
        /// </summary>
        public ImmutableList<ResponseAction<object>> ResponseActions => GetProperty<ImmutableList<ResponseAction<object>>>();

        /// <summary>
        ///     The request's URI template parameters (if any).
        /// </summary>
        public ImmutableDictionary<string, IValueProvider<object, string>> TemplateParameters => GetProperty<ImmutableDictionary<string, IValueProvider<object, string>>>();

        /// <summary>
        ///     The request's query parameters (if any).
        /// </summary>
        public ImmutableDictionary<string, IValueProvider<object, string>> QueryParameters => GetProperty<ImmutableDictionary<string, IValueProvider<object, string>>>();

        #endregion // Properties

        #region Invocation

        /// <summary>
        ///		Build the request URI represented by the <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="baseUri">
        ///		An optional base URI to use if the request does not already have an absolute request URI.
        /// </param>
        /// <returns>
        ///		The request URI.
        /// </returns>
        public Uri BuildRequestUri(Uri baseUri = null)
        {
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

                IDictionary<string, string> templateParameterValues = GetTemplateParameterValues();

                requestUri = template.Populate(baseUri, templateParameterValues);
            }

            // Merge in any other query parameters defined directly on the request.
            requestUri = MergeQueryParameters(requestUri);

            return requestUri;
        }

        /// <summary>
        ///		Build and configure a new HTTP request message.
        /// </summary>
        /// <param name="httpMethod">
        ///		The HTTP request method to use.
        /// </param>
        /// <param name="body">
        ///		Optional <see cref="HttpContent"/> representing the request body.
        /// </param>
        /// <param name="baseUri">
        ///		An optional base URI to use if the request does not already have an absolute request URI.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpRequestMessage"/>.
        /// </returns>
        public HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, HttpContent body = null, Uri baseUri = null)
        {
            if (httpMethod == null)
                throw new ArgumentNullException(nameof(httpMethod));

            Uri requestUri = BuildRequestUri(baseUri);

            HttpRequestMessage requestMessage = null;
            try
            {
                requestMessage = new HttpRequestMessage(httpMethod, requestUri);
                SetStandardMessageProperties(requestMessage);

                if (body != null)
                    requestMessage.Content = body;

                List<Exception> configurationActionExceptions = new List<Exception>();
                foreach (RequestAction<object> requestAction in RequestActions)
                {
                    if (requestAction == null)
                        continue;

                    try
                    {
                        requestAction(requestMessage, DefaultContext);
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
        HttpRequestMessage IHttpRequest<object>.BuildRequestMessage(HttpMethod httpMethod, object context, HttpContent body, Uri baseUri)
        {
            return BuildRequestMessage(httpMethod, body, baseUri);
        }

        #endregion // Invocation

        #region IHttpRequestProperties<object>

        /// <summary>
        ///		Actions (if any) to perform on the outgoing request message.
        /// </summary>
        IReadOnlyList<RequestAction<object>> IHttpRequestProperties<object>.RequestActions => RequestActions;

        /// <summary>
        ///		Actions (if any) to perform on the outgoing request message.
        /// </summary>
        IReadOnlyList<ResponseAction<object>> IHttpRequestProperties<object>.ResponseActions => ResponseActions;

        /// <summary>
        ///     The request's URI template parameters (if any).
        /// </summary>
        IReadOnlyDictionary<string, IValueProvider<object, string>> IHttpRequestProperties<object>.TemplateParameters => TemplateParameters;

        /// <summary>
        ///     The request's query parameters (if any).
        /// </summary>
        IReadOnlyDictionary<string, IValueProvider<object, string>> IHttpRequestProperties<object>.QueryParameters => QueryParameters;

        #endregion // IHttpRequestProperties<object>

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
        public new HttpRequest Clone(Action<IDictionary<string, object>> modifications)
        {
            if (modifications == null)
                throw new ArgumentNullException(nameof(modifications));

            return (HttpRequest)base.Clone(modifications);
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
            return new HttpRequest(requestProperties);
        }

        #endregion // Cloning

        #region Helpers

        /// <summary>
        ///		Merge the request's query parameters (if any) into the request URI.
        /// </summary>
        /// <param name="requestUri">
        ///		The request URI.
        /// </param>
        /// <returns>
        ///		The request URI with query parameters merged into it.
        /// </returns>
        Uri MergeQueryParameters(Uri requestUri)
        {
            if (requestUri == null)
                throw new ArgumentNullException(nameof(requestUri));

            if (QueryParameters.Count == 0)
                return requestUri;

            NameValueCollection queryParameters = requestUri.ParseQueryParameters();
            foreach (KeyValuePair<string, IValueProvider<object, string>> queryParameter in QueryParameters)
            {
                string queryParameterValue = queryParameter.Value.Get(DefaultContext);
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
        /// <returns>
        ///		A dictionary of key / value pairs (any parameters whose value-getters return null will be omitted).
        /// </returns>
        IDictionary<string, string> GetTemplateParameterValues()
        {
            return
                TemplateParameters.Select(templateParameter =>
                {
                    Debug.Assert(templateParameter.Value != null);

                    return new
                    {
                        templateParameter.Key,
                        Value = templateParameter.Value.Get(DefaultContext)
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
