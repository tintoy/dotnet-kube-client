using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace KubeClient.Http
{
	using ValueProviders;

	/// <summary>
	///		Represents common properties of templates for building HTTP requests.
	/// </summary>
	public interface IHttpRequestProperties
    {
		/// <summary>
		///		The request URI.
		/// </summary>
		Uri Uri
		{
			get;
		}

		/// <summary>
		///		Is the request URI a template?
		/// </summary>
		bool IsUriTemplate
		{
			get;
		}

		/// <summary>
		///		Additional properties for the request.
		/// </summary>
		ImmutableDictionary<string, object> Properties
		{
			get;
		}
    }

	/// <summary>
	///		Represents common properties of templates for building HTTP requests.
	/// </summary>
	/// <typeparam name="TContext">
	///		The type of object used as a context for resolving deferred template parameters.
	/// </typeparam>
	public interface IHttpRequestProperties<TContext>
		: IHttpRequestProperties
	{
		/// <summary>
		///		Actions (if any) to perform on the outgoing request message.
		/// </summary>
		IReadOnlyList<RequestAction<TContext>> RequestActions { get; }

		/// <summary>
		///		Actions (if any) to perform on the outgoing request message.
		/// </summary>
		IReadOnlyList<ResponseAction<TContext>> ResponseActions { get; }

		/// <summary>
		///     The request's URI template parameters (if any).
		/// </summary>
		IReadOnlyDictionary<string, IValueProvider<TContext, string>> TemplateParameters { get; }

		/// <summary>
		///     The request's query parameters (if any).
		/// </summary>
		IReadOnlyDictionary<string, IValueProvider<TContext, string>> QueryParameters { get; }
	}
}
