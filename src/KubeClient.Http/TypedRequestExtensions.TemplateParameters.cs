using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KubeClient.Http
{
	using ValueProviders;

	/// <summary>
	///		<see cref="HttpRequest{TContext}"/> / <see cref="IHttpRequest{TContext}"/> extension methods for template parameters.
	/// </summary>
	public static partial class TypedRequestExtensions
    {
		/// <summary>
		///		Create a copy of the request builder with the specified request URI template parameter.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
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
		public static HttpRequest<TContext> WithTemplateParameter<TContext, TValue>(this HttpRequest<TContext> request, string name, TValue value)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (String.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

			return request.WithTemplateParameterFromProvider(name,
				ValueProvider<TContext>.FromConstantValue(value?.ToString())
			);
		}

		/// <summary>
		///		Create a copy of the request builder with the specified request URI template parameter.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <typeparam name="TParameter">
		///		The parameter data-type.
		/// </typeparam>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="name">
		///		The parameter name.
		/// </param>
		/// <param name="getValue">
		///		Delegate that returns the parameter value.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> WithTemplateParameter<TContext, TParameter>(this HttpRequest<TContext> request, string name, Func<TParameter> getValue)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (String.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

			if (getValue == null)
				throw new ArgumentNullException(nameof(getValue));

			return request.WithTemplateParameterFromProvider(name,
				ValueProvider<TContext>.FromFunction(getValue).Convert().ValueToString()
			);
		}

		/// <summary>
		///		Create a copy of the request builder with the specified request URI template parameter.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <typeparam name="TParameter">
		///		The parameter data-type.
		/// </typeparam>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="name">
		///		The parameter name.
		/// </param>
		/// <param name="getValue">
		///		Delegate that returns the parameter value.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> WithTemplateParameter<TContext, TParameter>(this HttpRequest<TContext> request, string name, Func<TContext, TParameter> getValue)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (String.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(name));

			if (getValue == null)
				throw new ArgumentNullException(nameof(getValue));

			return request.WithTemplateParameterFromProvider(name,
				ValueProvider<TContext>.FromSelector(getValue).Convert().ValueToString()
			);
		}

		/// <summary>
		///		Create a copy of the request builder with the specified request URI template parameter.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
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
		///		A <see cref="IValueProvider{TSource, TValue}">value provider</see> that, given the current context, returns the parameter value.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> WithTemplateParameterFromProvider<TContext, TParameter>(this HttpRequest<TContext> request, string name, IValueProvider<TContext, TParameter> valueProvider)
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
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
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
		public static HttpRequest<TContext> WithTemplateParameters<TContext, TParameters>(HttpRequest<TContext> request, TParameters parameters)
		{
			if (ReferenceEquals(parameters, null))
				throw new ArgumentNullException(nameof(parameters));

			if (parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			return request.WithTemplateParametersFromProviders(
				CreateDeferredParameters<TContext, TParameters>(parameters)
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
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> WithTemplateParametersFromProviders<TContext>(this HttpRequest<TContext> request, IEnumerable<KeyValuePair<string, IValueProvider<TContext, string>>> templateParameters)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (templateParameters == null)
				throw new ArgumentNullException(nameof(templateParameters));

			bool modified = false;
			ImmutableDictionary<string, IValueProvider<TContext, string>>.Builder templateParametersBuilder = request.TemplateParameters.ToBuilder();
			foreach (KeyValuePair<string, IValueProvider<TContext, string>> templateParameter in templateParameters)
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
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> WithoutTemplateParameter<TContext>(this HttpRequest<TContext> request, string name)
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
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public static HttpRequest<TContext> WithoutTemplateParameters<TContext>(this HttpRequest<TContext> request, IEnumerable<string> names)
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
