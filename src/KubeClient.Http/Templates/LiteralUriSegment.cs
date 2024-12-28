using System;

namespace KubeClient.Http.Templates
{
	/// <summary>
	///		Represents a literal URI segment (i.e. one that has a constant value).
	/// </summary>
	sealed class LiteralUriSegment
		: UriSegment
	{
		/// <summary>
		///		The segment value;
		/// </summary>
		readonly string _value;

		/// <summary>
		///		Create a new literal URI segment.
		/// </summary>
		/// <param name="value">
		///		The segment value.
		/// </param>
		/// <param name="isDirectory">
		///		Does the segment represent a directory (i.e. have a trailing slash?).
		/// </param>
		public LiteralUriSegment(string value, bool isDirectory)
			: base(isDirectory)
		{
			if (String.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'value'.", nameof(value));

			_value = value;
		}

		/// <summary>
		///		The segment value;
		/// </summary>
		public string Value
		{
			get
			{
				return _value;
			}
		}

		/// <summary>
		///		Get the value of the segment (if any).
		/// </summary>
		/// <param name="evaluationContext">
		///		The current template evaluation context.
		/// </param>
		/// <returns>
		///		The segment value, or <c>null</c> if the segment is missing.
		/// </returns>
		public override string GetValue(ITemplateEvaluationContext evaluationContext)
		{
			if (evaluationContext == null)
				throw new ArgumentNullException(nameof(evaluationContext));

			return _value;
		}
	}
}
