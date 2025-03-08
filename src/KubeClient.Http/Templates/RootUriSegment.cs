using System;

namespace KubeClient.Http.Templates
{
	/// <summary>
	///		A literal URI segment representing the root folder ("/").
	/// </summary>
	sealed class RootUriSegment
		: UriSegment
	{
		/// <summary>
		///		The singleton instance of the root URI segment.
		/// </summary>
		public static readonly RootUriSegment Instance = new RootUriSegment();

		/// <summary>
		///		Create a new literal URI segment.
		/// </summary>
		RootUriSegment()
			: base(isDirectory: true)
		{
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

			return String.Empty;
		}
	}
}
