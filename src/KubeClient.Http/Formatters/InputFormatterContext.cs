using System;
using System.IO;
using System.Text;

namespace KubeClient.Http.Formatters
{
	/// <summary>
	///		Contextual information used by input formatters.
	/// </summary>
	public class InputFormatterContext
	{
		/// <summary>
		///		Create a new <see cref="InputFormatterContext"/>.
		/// </summary>
		/// <param name="dataType">
		///		The CLR type into which the data will be deserialised.
		/// </param>
		/// <param name="mediaType">
		///		The media type that the formatter should expect.
		/// </param>
		/// <param name="encoding">
		///		The content encoding that the formatter should expect.
		/// </param>
		public InputFormatterContext(Type dataType, string mediaType, Encoding encoding)
		{
			DataType = dataType;
			MediaType = mediaType;
			Encoding = encoding;
		}

		/// <summary>
		///		The CLR type into which the data will be deserialised.
		/// </summary>
		public Type DataType { get; }

		/// <summary>
		///		The media type that the formatter should expect.
		/// </summary>
		public string MediaType { get; }

		/// <summary>
		///		The content encoding that the formatter should expect.
		/// </summary>
		public Encoding Encoding { get; }

		/// <summary>
		///		Create a <see cref="TextReader"/> from the specified input stream.
		/// </summary>
		/// <param name="inputStream">
		///		The input stream.
		/// </param>
		/// <returns>
		///		The <see cref="TextReader"/>.
		/// </returns>
		/// <remarks>
		///		The <see cref="TextReader"/>, when closed, will not close the input stream.
		/// </remarks>
		public virtual TextReader CreateReader(Stream inputStream)
		{
			if (inputStream == null)
				throw new ArgumentNullException(nameof(inputStream));

			return StreamHelper.CreateTransientTextReader(inputStream, Encoding);
		}
	}
}