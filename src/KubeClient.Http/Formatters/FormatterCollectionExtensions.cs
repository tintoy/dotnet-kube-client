using System;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Extension methods for working with <see cref="IFormatterCollection"/>s.
	/// </summary>
    public static class FormatterCollectionExtensions
    {
		/// <summary>
		///		Remove the formatter of the specified type from the collection (if it is present).
		/// </summary>
		/// <typeparam name="TFormatter">
		///		The type of formatter to remove.
		/// </typeparam>
		/// <param name="formatters">
		///		The formatter to remove.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the formatter was removed; otherwise, <c>false</c>.
		/// </returns>
		public static bool Remove<TFormatter>(this IFormatterCollection formatters)
			where TFormatter : IFormatter
		{
			if (formatters == null)
				throw new ArgumentNullException(nameof(formatters));

			return formatters.Remove(typeof(TFormatter));
		}
    }
}
