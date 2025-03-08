using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KubeClient.Http.Formatters
{
    /// <summary>
    ///		HTTP content formatted using an <see cref="IOutputFormatter"/>.
    /// </summary>
    public class FormattedObjectContent
        : HttpContent
    {
        /// <summary>
        /// 	The default encoding used by <see cref="FormattedObjectContent"/>.
        /// </summary>
        public static readonly Encoding DefaultEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        /// <summary>
        ///		Create new formatted object content.
        /// </summary>
        /// <param name="formatter">
        ///		The <see cref="IOutputFormatter"/> that will be used to serialise the data.
        /// </param>
        /// <param name="data">
        ///		The data that will be serialised to form the content.
        /// </param>
        /// <param name="dataType">
        ///		The type of data that will be serialised to form the content.
        /// </param>
        /// <param name="mediaType">
        ///		The content type being serialised.
        /// </param>
        /// <remarks>
        ///		Uses UTF-8 encoding.
        /// </remarks>
        public FormattedObjectContent(IOutputFormatter formatter, Type dataType, object data, string mediaType)
            : this(formatter, data, dataType, mediaType, DefaultEncoding)
        {
        }

        /// <summary>
        ///		Create new formatted object content.
        /// </summary>
        /// <param name="formatter">
        ///		The <see cref="IOutputFormatter"/> that will be used to serialise the data.
        /// </param>
        /// <param name="data">
        ///		The data that will be serialised to form the content.
        /// </param>
        /// <param name="dataType">
        ///		The type of data that will be serialised to form the content.
        /// </param>
        /// <param name="mediaType">
        ///		The media type that the formatter should produce.
        /// </param>
        /// <param name="encoding">
        ///		The <see cref="Encoding"/> that the formatter should use for serialised data.
        /// </param>
        public FormattedObjectContent(IOutputFormatter formatter, object data, Type dataType, string mediaType, Encoding encoding)
            : this(formatter, new OutputFormatterContext(data, dataType, mediaType, encoding))
        {
        }

        /// <summary>
        ///		Create new formatted object content.
        /// </summary>
        /// <param name="formatter">
        ///		The <see cref="IOutputFormatter"/> that will be used to serialise the data.
        /// </param>
        /// <param name="formatterContext">
        ///		Contextual information use by the <see cref="IOutputFormatter"/>.
        /// </param>
        public FormattedObjectContent(IOutputFormatter formatter, OutputFormatterContext formatterContext)
        {
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            if (formatterContext == null)
                throw new ArgumentNullException(nameof(formatterContext));

            Formatter = formatter;
            FormatterContext = formatterContext;
            Headers.ContentType = new MediaTypeHeaderValue(MediaType);
        }

        /// <summary>
        ///		The <see cref="IOutputFormatter"/> that will be used to serialise the data.
        /// </summary>
        public IOutputFormatter Formatter { get; }

        /// <summary>
        ///		Contextual information use by the <see cref="Formatter"/>.
        /// </summary>
        public OutputFormatterContext FormatterContext { get; }

        /// <summary>
        ///		The type of data that will be serialised to form the content.
        /// </summary>
        public Type DataType => FormatterContext.DataType;

        /// <summary>
        ///		The data that will be serialised to form the content.
        /// </summary>
        public object Data => FormatterContext.Data;

        /// <summary>
        ///		The media type that the formatter should produce.
        /// </summary>
        public string MediaType => FormatterContext.MediaType;

        /// <summary>
        ///		The <see cref="Encoding"/> that the formatter should use for serialised data.
        /// </summary>
        public Encoding Encoding => FormatterContext.Encoding;

        /// <summary>
        ///     Try to pre-compute the formatted content length.
        /// </summary>
        /// <param name="length">
        ///     The length (in bytes) of the content.
        ///
        ///		Always -1, since <see cref="FormattedObjectContent"/> length is not known before serialisation.
        /// </param>
        /// <returns>
        ///     <c>false</c>.
        /// </returns>
        protected override bool TryComputeLength(out long length)
        {
            // We don't know the length in advance.
            length = -1;

            return false;
        }

        /// <summary>
        ///     Serialize the HTTP content to a stream as an asynchronous operation.
        /// </summary>
        /// <param name="stream">
        ///     The target stream.
        /// </param>
        /// <param name="context">
        ///     Information about the transport (channel binding token, for example).
        ///
        ///		Can be null.
        /// </param>
        /// <returns>
        ///     Returns <see cref="Task" />.The task object representing the asynchronous operation.
        /// </returns>
        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            await Formatter.WriteAsync(FormatterContext, stream);
        }
    }
}
