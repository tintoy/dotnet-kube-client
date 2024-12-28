using System;
using System.Collections.Generic;

namespace KubeClient.Http.Templates
{
	/// <summary>
	///		The default evaluation context for a URI template.
	/// </summary>
	sealed class TemplateEvaluationContext
		: ITemplateEvaluationContext
	{
		/// <summary>
		///		The template parameters.
		/// </summary>
		readonly Dictionary<string, string> _templateParameters = new Dictionary<string, string>();

		/// <summary>
		///		Create a new template evaluation context.
		/// </summary>
		public TemplateEvaluationContext()
		{
		}

		/// <summary>
		///		Create a new template evaluation context.
		/// </summary>
		/// <param name="templateParameters">
		///		A dictionary of template parameters (and their values) used to populate the evaluation context.
		/// </param>
		public TemplateEvaluationContext(IDictionary<string, string> templateParameters)
		{
			if (templateParameters == null)
				throw new ArgumentNullException(nameof(templateParameters));

			foreach (KeyValuePair<string, string> templateParameter in templateParameters)
				_templateParameters[templateParameter.Key] = templateParameter.Value;
		}

		/// <summary>
		///		The template parameters.
		/// </summary>
		public Dictionary<string, string> TemplateParameters
		{
			get
			{
				return _templateParameters;
			}
		}

		/// <summary>
		///		Determine whether the specified parameter is defined.
		/// </summary>
		/// <param name="parameterName">
		///		The parameter name.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the parameter is defined; otherwise, <c>false</c>.
		/// </returns>
		public bool IsParameterDefined(string parameterName)
		{
			if (String.IsNullOrWhiteSpace(parameterName))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'parameterName'.", nameof(parameterName));

			return _templateParameters.ContainsKey(parameterName);
		}

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
		public string this[string parameterName, bool isOptional]
		{
			get
			{
				if (String.IsNullOrWhiteSpace(parameterName))
					throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'parameterName'.", nameof(parameterName));

				string parameterValue;
				if (!_templateParameters.TryGetValue(parameterName, out parameterValue))
				{
					if (!isOptional)
						throw new UriTemplateException($"Required template parameter '{parameterName}' is not defined.");
				}

				return parameterValue;
			}
		}
	}
}
