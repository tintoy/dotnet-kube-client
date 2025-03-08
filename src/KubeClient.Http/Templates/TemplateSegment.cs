using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KubeClient.Http.Templates
{
	/// <summary>
	///		The base class for the segments that comprise a URI template.
	/// </summary>
	abstract class TemplateSegment
	{
		/// <summary>
		///		The regular expression used to match variables.
		/// </summary>
		static readonly Regex VariableRegex = new Regex(
			@"\{(?<VariableName>\w+)(?<VariableIsOptional>\?)?\}\/?",
			RegexOptions.Compiled | RegexOptions.Singleline
		);

		/// <summary>
		///		Create a new URI template segment.
		/// </summary>
		protected TemplateSegment()
		{
		}

		/// <summary>
		///		Does the segment have a parameterised (non-constant) value?
		/// </summary>
		public virtual bool IsParameterized => true;

		/// <summary>
		///		Get the value of the segment (if any).
		/// </summary>
		/// <param name="evaluationContext">
		///		The current template evaluation context.
		/// </param>
		/// <returns>
		///		The segment value, or <c>null</c> if the segment has no value.
		/// </returns>
		public abstract string GetValue(ITemplateEvaluationContext evaluationContext);

		/// <summary>
		///		Parse the specified URI into template segments.
		/// </summary>
		/// <param name="template">
		///		The URI to parse.
		/// </param>
		/// <returns>
		///		The template segments.
		/// </returns>
		public static IReadOnlyList<TemplateSegment> Parse(string template)
		{
			if (template == null)
				throw new ArgumentNullException(nameof(template));

			List<TemplateSegment> segments = new List<TemplateSegment>();

			try
			{
				Uri templateUri = new Uri(
					new Uri("http://localhost/"),
					template.Replace("?}", "%3F}") // Special case for '?' because it messes with Uri's parser.
				);
				segments.AddRange(
					ParsePathSegments(templateUri)
				);
				segments.AddRange(
					ParseQuerySegments(templateUri)
				);
			}
			catch (Exception eParse)
			{
				throw new UriTemplateException(eParse, $"'{template}' is not a valid URI template.");
			}
			
			return segments;
		}

		/// <summary>
		///		Parse URI segments from the specified template.
		/// </summary>
		/// <param name="template">
		///		The URI template.
		/// </param>
		/// <returns>
		///		A sequence of 0 or more URI segments.
		/// </returns>
		static IEnumerable<UriSegment> ParsePathSegments(Uri template)
		{
			if (template == null)
				throw new ArgumentNullException(nameof(template));

			bool haveRoot = false;
			bool isLastSegmentDirectory = template.AbsolutePath[template.AbsolutePath.Length - 1] == '/';
			
			string[] pathSegments =
				template.AbsolutePath
					.Split('/')
					.Select(
						segment => Uri.UnescapeDataString(segment)
					)
					.ToArray();

			int lastSegmentIndex = pathSegments.Length - 1;
			for (int segmentIndex = 0; segmentIndex < pathSegments.Length; segmentIndex++)
			{
				string pathSegment = pathSegments[segmentIndex];
				if (pathSegment != String.Empty)
				{
					bool isDirectory = isLastSegmentDirectory || segmentIndex < lastSegmentIndex;

					Match variableMatch = VariableRegex.Match(pathSegment);
					if (variableMatch.Success)
					{
						string templateParameterName = variableMatch.Groups["VariableName"].Value;
						if (String.IsNullOrWhiteSpace(templateParameterName))
							yield return new LiteralUriSegment(pathSegment, isDirectory);

						bool isOptional = variableMatch.Groups["VariableIsOptional"].Value.Length > 0;

						yield return new ParameterizedUriSegment(templateParameterName, isDirectory, isOptional);
					}
					else
						yield return new LiteralUriSegment(pathSegment, isDirectory);
				}
				else
				{
					if (haveRoot)
						continue;

					haveRoot = true;

					yield return RootUriSegment.Instance;
				}
			}
		}

		/// <summary>
		///		Parse query segments from the specified template.
		/// </summary>
		/// <param name="template">
		///		The URI template.
		/// </param>
		/// <returns>
		///		A sequence of 0 or more query segments.
		/// </returns>
		static IEnumerable<QuerySegment> ParseQuerySegments(Uri template)
		{
			if (template == null)
				throw new ArgumentNullException(nameof(template));

			if (template.Query == String.Empty)
				yield break;

			string[] queryParameters =
				template.Query.Substring(1).Split(
					separator: new char[]
					{
						'&'
					},
					options: StringSplitOptions.RemoveEmptyEntries

				);

			foreach (string queryParameter in queryParameters)
			{
				string[] parameterNameAndValue = queryParameter.Split(
					separator: new char[]
					{
						'='
					},
					count: 2
				);

				if (parameterNameAndValue.Length != 2)
					continue; // Remove parameter.

				string queryParameterName = parameterNameAndValue[0];
				string queryParameterValue = Uri.UnescapeDataString(parameterNameAndValue[1]);

				Match variableMatch = VariableRegex.Match(queryParameterValue);
				if (variableMatch.Success)
				{
					string templateParameterName = variableMatch.Groups["VariableName"].Value;
					if (String.IsNullOrWhiteSpace(templateParameterName))
						yield return new LiteralQuerySegment(queryParameterName, queryParameterValue);

					bool isOptional = variableMatch.Groups["VariableIsOptional"].Value.Length > 0;

					yield return new ParameterizedQuerySegment(queryParameterName, templateParameterName, isOptional);
				}
				else
					yield return new LiteralQuerySegment(queryParameterName, queryParameterValue);
			}
		}
	}
}
