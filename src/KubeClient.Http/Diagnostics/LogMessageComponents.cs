using System;

namespace KubeClient.Http.Diagnostics
{
	/// <summary>
	/// 	Components of an HTTP message that should be logged.
	/// </summary>
	[Flags]
	public enum LogMessageComponents
	{
		/// <summary>
		///		No message components should be logged.
		/// </summary>
		None = 0,

		/// <summary>
		///		Basic message components (e.g. method and request URI) should be logged.
		/// </summary>
		Basic = 1,

		/// <summary>
		///		Message body should be logged.
		/// </summary>
		Body = 2,

		/// <summary>
		///		Message headers should be logged.
		/// </summary>
		Headers = 4,

		/// <summary>
		///		All message components should be logged.
		/// </summary>
		All = Basic | Body | Headers
	}
}