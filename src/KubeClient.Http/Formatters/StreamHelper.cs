using System;
using System.IO;
using System.Text;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Helper methods for formatters working with streams.
	/// </summary>
	static class StreamHelper
	{
		/// <summary>
		///		The default buffer size for <see cref="TextReader"/>s / <see cref="TextWriter"/>s created by the helper.
		/// </summary>
		public const int DefaultBufferSize = 1024;

		/// <summary>
		///		Create a <see cref="TextReader"/> that, when it is closed, leaves its input stream open.
		/// </summary>
		/// <param name="inputStream">
		///		The input stream.
		/// </param>
		/// <param name="encoding">
		///		The stream's text encoding.
		/// </param>
		/// <param name="bufferSize">
		///		An optional buffer size.
		/// 
		///		Defaults to <see cref="DefaultBufferSize"/>.
		/// </param>
		/// <returns>
		///		The <see cref="TextReader"/>.
		/// </returns>
		public static TextReader CreateTransientTextReader(Stream inputStream, Encoding encoding, int bufferSize = DefaultBufferSize)
		{
			if (inputStream == null)
				throw new ArgumentNullException(nameof(inputStream));

			if (encoding == null)
				throw new ArgumentNullException(nameof(encoding));

			if (bufferSize < DefaultBufferSize)
				throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, $"Buffer size cannot be less than {bufferSize} bytes.");

			return new StreamReader(inputStream, encoding,
				detectEncodingFromByteOrderMarks: false,
				bufferSize: bufferSize,
				leaveOpen: true
			);
		}

		/// <summary>
		///		Create a <see cref="TextWriter"/> that, when it is closed, leaves its output stream open.
		/// </summary>
		/// <param name="outputStream">
		///		The output stream.
		/// </param>
		/// <param name="encoding">
		///		The stream's text encoding.
		/// </param>
		/// <param name="bufferSize">
		///		An optional buffer size.
		/// 
		///		Defaults to <see cref="DefaultBufferSize"/>.
		/// </param>
		/// <returns>
		///		The <see cref="TextWriter"/>.
		/// </returns>
		public static TextWriter CreateTransientTextWriter(Stream outputStream, Encoding encoding, int bufferSize = DefaultBufferSize)
		{
			if (outputStream == null)
				throw new ArgumentNullException(nameof(outputStream));

			if (encoding == null)
				throw new ArgumentNullException(nameof(encoding));

			if (bufferSize < DefaultBufferSize)
				throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, $"Buffer size cannot be less than {bufferSize} bytes.");

			return new StreamWriter(outputStream, encoding,
				bufferSize: bufferSize,
				leaveOpen: true
			);
		}
	}
}
