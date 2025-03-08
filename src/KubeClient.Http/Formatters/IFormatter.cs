using System.Collections.Generic;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Represents a content formatter.
	/// </summary>
    public interface IFormatter
    {
		/// <summary>
		///		Content types supported by the formatter.
		/// </summary>
		ISet<string> SupportedMediaTypes { get; }
	}
}
