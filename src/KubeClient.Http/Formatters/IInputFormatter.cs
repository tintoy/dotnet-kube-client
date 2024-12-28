using System.IO;
using System.Threading.Tasks;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Represents a facility for deserialising data for one or more media types.
	/// </summary>
    public interface IInputFormatter
		: IFormatter
	{
		/// <summary>
		///		Determine whether the formatter can deserialise the specified data.
		/// </summary>
		/// <param name="context">
		///		Contextual information about the data being deserialised.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the formatter can deserialise the data; otherwise, <c>false</c>.
		/// </returns>
		bool CanRead(InputFormatterContext context);

		/// <summary>
		///		Asynchronously deserialise data from an input stream.
		/// </summary>
		/// <param name="context">
		///		Contextual information about the data being deserialised.
		/// </param>
		/// <param name="stream">
		///		The input stream from which to read serialised data.
		/// </param>
		/// <returns>
		///		The deserialised object.
		/// </returns>
		Task<object> ReadAsync(InputFormatterContext context, Stream stream);
	}
}
