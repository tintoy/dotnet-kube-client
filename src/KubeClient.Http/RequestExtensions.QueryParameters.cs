using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KubeClient.Http
{
    using ValueProviders;

    /// <summary>
    ///		<see cref="HttpRequest"/> / <see cref="IHttpRequest"/> extension methods for query parameters.
    /// </summary>
    public static partial class RequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameter.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <typeparam name="TParameter">
        ///		The parameter data-type.
        /// </typeparam>
        /// <param name="name">
        ///		The parameter name.
        /// </param>
        /// <param name="value">
        ///		The parameter value.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithQueryParameter<TParameter>(this HttpRequest request, string name, TParameter value)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            return request.WithQueryParameterFromProvider(name,
                ValueProvider<object>.FromConstantValue(value?.ToString())
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameter.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <typeparam name="TParameter">
        ///		The parameter data-type.
        /// </typeparam>
        /// <param name="name">
        ///		The parameter name.
        /// </param>
        /// <param name="getValue">
        ///		Delegate that returns the parameter value (cannot be <c>null</c>).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithQueryParameter<TParameter>(this HttpRequest request, string name, Func<TParameter> getValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithQueryParameterFromProvider(
                name,
                ValueProvider<object>.FromFunction(getValue)
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameter.
        /// </summary>
        /// <typeparam name="TParameter">
        ///		The parameter data-type.
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
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithQueryParameterFromProvider<TParameter>(this HttpRequest request, string name, IValueProvider<object, TParameter> valueProvider)
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
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithQueryParameters<TParameters>(this HttpRequest request, TParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return request.WithQueryParametersFromProviders(
                CreateDeferredParameters<object, TParameters>(parameters)
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI query parameters.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="queryParameters">
        ///		A sequence of 0 or more key / value pairs representing the query parameters (values cannot be <c>null</c>).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithQueryParametersFromProviders(this HttpRequest request, IEnumerable<KeyValuePair<string, IValueProvider<object, string>>> queryParameters)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (queryParameters == null)
                throw new ArgumentNullException(nameof(queryParameters));

            bool modified = false;
            ImmutableDictionary<string, IValueProvider<object, string>>.Builder queryParametersBuilder = request.QueryParameters.ToBuilder();
            foreach (KeyValuePair<string, IValueProvider<object, string>> queryParameter in queryParameters)
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
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="name">
        ///		The parameter name.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithoutQueryParameter(this HttpRequest request, string name)
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
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithoutQueryParameters(this HttpRequest request, IEnumerable<string> names)
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
