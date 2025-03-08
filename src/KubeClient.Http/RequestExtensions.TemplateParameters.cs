using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KubeClient.Http
{
    using ValueProviders;

    /// <summary>
    ///		<see cref="HttpRequest"/> / <see cref="IHttpRequest"/> extension methods for template parameters.
    /// </summary>
    public static partial class RequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request builder with the specified request URI template parameter.
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
        public static HttpRequest WithTemplateParameter<TParameter>(this HttpRequest request, string name, TParameter value)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            return request.WithTemplateParameterFromProvider(name,
                ValueProvider<object>.FromConstantValue(value?.ToString())
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI template parameter.
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
        public static HttpRequest WithTemplateParameter<TParameter>(this HttpRequest request, string name, Func<TParameter> getValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithTemplateParameterFromProvider(name,
                ValueProvider<object>.FromFunction(getValue).Convert().ValueToString()
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI template parameter.
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
        /// <param name="valueProvider">
        ///		A <see cref="IValueProvider{TSource, TValue}">value provider</see> that, given the current context, returns the parameter value.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithTemplateParameterFromProvider<TParameter>(this HttpRequest request, string name, IValueProvider<object, TParameter> valueProvider)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            if (valueProvider == null)
                throw new ArgumentNullException(nameof(valueProvider));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.TemplateParameters)] = request.TemplateParameters.SetItem(
                    key: name,
                    value: valueProvider.Convert().ValueToString()
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request, but with template parameters from the specified object's properties.
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
        public static HttpRequest WithTemplateParameters<TParameters>(this HttpRequest request, TParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return request.WithTemplateParametersFromProviders(
                CreateDeferredParameters<object, TParameters>(parameters)
            );
        }

        /// <summary>
        ///		Create a copy of the request builder with the specified request URI template parameters.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="templateParameters">
        ///		A sequence of 0 or more key / value pairs representing the template parameters (values cannot be <c>null</c>).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithTemplateParametersFromProviders(this HttpRequest request, IEnumerable<KeyValuePair<string, IValueProvider<object, string>>> templateParameters)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (templateParameters == null)
                throw new ArgumentNullException(nameof(templateParameters));

            bool modified = false;
            ImmutableDictionary<string, IValueProvider<object, string>>.Builder templateParametersBuilder = request.TemplateParameters.ToBuilder();
            foreach (KeyValuePair<string, IValueProvider<object, string>> templateParameter in templateParameters)
            {
                if (templateParameter.Value == null)
                {
                    throw new ArgumentException(
                        String.Format(
                            "Template parameter '{0}' has a null getter; this is not supported.",
                            templateParameter.Key
                        ),
                        nameof(templateParameters)
                    );
                }

                templateParametersBuilder[templateParameter.Key] = templateParameter.Value;
                modified = true;
            }

            if (!modified)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.TemplateParameters)] = templateParametersBuilder.ToImmutable();
            });
        }

        /// <summary>
        ///		Create a copy of the request builder without the specified request URI template parameter.
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
        public static HttpRequest WithoutTemplateParameter(this HttpRequest request, string name)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

            if (!request.TemplateParameters.ContainsKey(name))
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.TemplateParameters)] = request.TemplateParameters.Remove(name);
            });
        }

        /// <summary>
        ///		Create a copy of the request builder without the specified request URI template parameters.
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
        public static HttpRequest WithoutTemplateParameters(this HttpRequest request, IEnumerable<string> names)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (names == null)
                throw new ArgumentNullException(nameof(names));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.TemplateParameters)] = request.TemplateParameters.RemoveRange(names);
            });
        }
    }
}
