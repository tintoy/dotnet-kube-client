using System.IO;
using System.Threading.Tasks;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Represents a facility for serialising data to one or more media types.
	/// </summary>
    public interface IOutputFormatter
		: IFormatter
    {
		/// <summary>
		///		Determine whether the formatter can serialise the specified data.
		/// </summary>
		/// <param name="context">
		///		Contextual information about the data being serialised.
		/// </param>
		/// <returns>
		///		<c>true</c>, if the formatter can serialise the data; otherwise, <c>false</c>.
		/// </returns>
		bool CanWrite(OutputFormatterContext context);

		/// <summary>
		///		Asynchronously serialise data to an output stream.
		/// </summary>
		/// <param name="context">
		///		Contextual information about the data being deserialised.
		/// </param>
		/// <param name="stream">
		///		The output stream to which the serialised data will be written.
		/// </param>
		/// <returns>
		///		A <see cref="Task"/> representing the asynchronous operation.
		/// </returns>
		Task WriteAsync(OutputFormatterContext context, Stream stream);
    }
}
