using System;
using System.Collections.Generic;

namespace KubeClient.Http.Templates
{
	/// <summary>
	///		Represents the evaluation context for a URI template.
	/// </summary>
	interface ITemplateEvaluationContext
	{
		/// <summary>
		///		Determine whether the specified parameter is defined.
		/// </summary>
		/// <param name="parameterName">
		///		The parameter name.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the parameter is defined; otherwise, <c>false</c>.
		/// </returns>
		bool IsParameterDefined(string parameterName);

		/// <summary>
		///		The value of the specified template parameter
		/// </summary>
		/// <param name="parameterName">
		///		The name of the template parameter.
		/// </param>
		/// <param name="isOptional">
		///		Is the parameter optional? If so, return <c>null</c> if it is not present, rather than throwing an exception.
		/// 
		///		Default is <c>true</c>.
		/// </param>
		/// <returns>
		///		The parameter value, or <c>null</c>.
		/// </returns>
		/// <exception cref="ArgumentException">
		///		<paramref name="parameterName"/> is <c>null</c>, empty, or entirely composed of whitespace.
		/// </exception>
		/// <exception cref="KeyNotFoundException">
		///		The parameter is not <paramref name="isOptional"/>, and is not preset.
		/// </exception>
		string this[string parameterName, bool isOptional = false]
		{
			get;
		}
	}
}
