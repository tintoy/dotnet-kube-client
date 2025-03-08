using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KubeClient.Http
{
    using ValueProviders;

    /// <summary>
    ///		<see cref="HttpRequest{TContext}"/> / <see cref="IHttpRequest{TContext}"/> extension methods for query parameters.
    /// </summary>
    public static partial class TypedRequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameter.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///		The parameter data-type.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="name">
        ///		The parameter name.
        /// </param>
        /// <param name="value">
        ///		The parameter value.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithQueryParameter<TContext, TValue>(this HttpRequest<TContext> request, string name, TValue value)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            return request.WithQueryParameterFromProvider(name,
                ValueProvider<TContext>.FromConstantValue(value?.ToString())
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameter.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="name">
        ///		The parameter name.
        /// </param>
        /// <param name="getValue">
        ///		Delegate that returns the parameter value (cannot be <c>null</c>).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithQueryParameter<TContext>(this HttpRequest<TContext> request, string name, Func<object> getValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithQueryParameterFromProvider(
                name,
                ValueProvider<TContext>.FromFunction(getValue)
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameter.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="name">
        ///		The parameter name.
        /// </param>
        /// <param name="valueProvider">
        ///		Delegate that, given the current context, returns the parameter value (cannot be <c>null</c>).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithQueryParameterFromProvider<TContext>(this HttpRequest<TContext> request, string name, IValueProvider<TContext, object> valueProvider)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            if (valueProvider == null)
                throw new ArgumentNullException(nameof(valueProvider));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.QueryParameters)] = request.QueryParameters.SetItem(
                    key: name,
                    value: valueProvider.Convert().ValueToString()
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request, but with query parameters from the specified object's properties.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <typeparam name="TParameters">
        ///		The type of object whose properties will form the parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="parameters">
        ///		The object whose properties will form the parameters.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithQueryParametersFrom<TContext, TParameters>(HttpRequest<TContext> request, TParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return request.WithQueryParametersFromProviders(
                CreateDeferredParameters<TContext, TParameters>(parameters)
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameters.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="queryParameters">
        ///		A sequence of 0 or more key / value pairs representing the query parameters (values cannot be <c>null</c>).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithQueryParametersFromProviders<TContext>(this HttpRequest<TContext> request, IEnumerable<KeyValuePair<string, IValueProvider<TContext, string>>> queryParameters)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (queryParameters == null)
                throw new ArgumentNullException(nameof(queryParameters));

            bool modified = false;
            ImmutableDictionary<string, IValueProvider<TContext, string>>.Builder queryParametersBuilder = request.QueryParameters.ToBuilder();
            foreach (KeyValuePair<string, IValueProvider<TContext, string>> queryParameter in queryParameters)
            {
                if (queryParameter.Value == null)
                {
                    throw new ArgumentException(
                        String.Format(
                            "Query parameter '{0}' has a null getter; this is not supported.",
                            queryParameter.Key
                        ),
                        nameof(queryParameters)
                    );
                }

                queryParametersBuilder[queryParameter.Key] = queryParameter.Value;
                modified = true;
            }

            if (!modified)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.QueryParameters)] = queryParametersBuilder.ToImmutable();
            });
        }

        /// <summary>
        ///		Create a copy of the request builder without the specified request URI query parameter.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="name">
        ///		The parameter name.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithoutQueryParameter<TContext>(this HttpRequest<TContext> request, string name)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            if (!request.QueryParameters.ContainsKey(name))
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.QueryParameters)] = request.QueryParameters.Remove(name);
            });
        }

        /// <summary>
        ///		Create a copy of the request builder without the specified request URI query parameters.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="names">
        ///		The parameter names.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithoutQueryParameters<TContext>(this HttpRequest<TContext> request, IEnumerable<string> names)
        {
            if (names == null)
                throw new ArgumentNullException(nameof(names));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.QueryParameters)] = request.QueryParameters.RemoveRange(names);
            });
        }
    }
}
