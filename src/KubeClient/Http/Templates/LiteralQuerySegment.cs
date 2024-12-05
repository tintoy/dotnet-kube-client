using System;

namespace KubeClient.Http.Templates
{
	/// <summary>
	///		A template segment that represents a literal query parameter (i.e. one that has a constant value).
	/// </summary>
	sealed class LiteralQuerySegment
		: QuerySegment
	{
		/// <summary>
		///		The value for the query parameter that the segment represents.
		/// </summary>
		readonly string _queryParameterValue;

		/// <summary>
		///		Create a new literal query segment.
		/// </summary>
		/// <param name="queryParameterName">
		///		The name of the query parameter that the segment represents.
		/// </param>
		/// <param name="queryParameterValue">
		///		The value for the query parameter that the segment represents.
		/// </param>
		public LiteralQuerySegment(string queryParameterName, string queryParameterValue)
			: base(queryParameterName)
		{
			if (String.IsNullOrWhiteSpace(queryParameterValue))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'value'.", nameof(queryParameterValue));

			_queryParameterValue = queryParameterValue;
		}

		/// <summary>
		///		The value for the query parameter that the segment represents.
		/// </summary>
		public string QueryParameterValue
		{
			get
			{
				return _queryParameterValue;
			}
		}

		/// <summary>
		///		Get the value of the segment (if any).
		/// </summary>
		/// <param name="evaluationContext">
		///		The current template evaluation context.
		/// </param>
		/// <returns>
		///		The segment value, or <c>null</c> if the segment has no value.
		/// </returns>
		public override string GetValue(ITemplateEvaluationContext evaluationContext)
		{
			if (evaluationContext == null)
				throw new ArgumentNullException(nameof(evaluationContext));

			return _queryParameterValue;
		}
	}
}
