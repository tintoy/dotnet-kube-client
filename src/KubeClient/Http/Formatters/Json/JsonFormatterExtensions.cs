using Newtonsoft.Json;
using System;

namespace KubeClient.Http
{
	using Formatters;
	using Formatters.Json;
	
	/// <summary>
	///		Extension methods for content formatters.
	/// </summary>
    public static class JsonFormatterExtensions
    {
		/// <summary>
		///		Add the JSON content formatter.
		/// </summary>
		/// <param name="formatters">
		///		The content formatter collection.
		/// </param>
		/// <param name="serializerSettings">
		///		Optional settings for the JSON serialiser.
		/// </param>
		/// <returns>
		///		The content formatter collection (enables method-chaining).
		/// </returns>
		public static IFormatterCollection AddJsonFormatter(this IFormatterCollection formatters, JsonSerializerSettings serializerSettings = null)
		{
			if (formatters == null)
				throw new ArgumentNullException(nameof(formatters));

			formatters.Add(new JsonFormatter
			{
				SerializerSettings = serializerSettings
			});

			return formatters;
		}
    }
}
