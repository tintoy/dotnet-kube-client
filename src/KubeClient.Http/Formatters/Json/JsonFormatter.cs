#if NET8_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace KubeClient.Http.Formatters.Json
{
    /// <summary>
    ///		Content formatter for JSON using .Json (JSON.NET).
    /// </summary>
    public class JsonFormatter
        : IInputFormatter, IOutputFormatter
    {
        /// <summary>
        ///		Create a new <see cref="JsonFormatter"/>.
        /// </summary>
        public JsonFormatter()
        {
        }

        /// <summary>
        ///		Settings for the JSON serialiser.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions { get; set; }

        /// <summary>
        ///		Content types supported by the formatter.
        /// </summary>
        public ISet<string> SupportedMediaTypes { get; } = new HashSet<string> { WellKnownMediaTypes.Json };

        /// <summary>
        ///		Determine whether the formatter can deserialise the specified data.
        /// </summary>
        /// <param name="context">
        ///		Contextual information about the data being deserialised.
        /// </param>
        /// <returns>
        ///		<c>true</c>, if the formatter can deserialise the data; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRead(InputFormatterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return SupportedMediaTypes.Contains(context.MediaType);
        }

        /// <summary>
        ///		Determine whether the formatter can serialise the specified data.
        /// </summary>
        /// <param name="context">
        ///		Contextual information about the data being serialised.
        /// </param>
        /// <returns>
        ///		<c>true</c>, if the formatter can serialise the data; otherwise, <c>false</c>.
        /// </returns>
        public bool CanWrite(OutputFormatterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return SupportedMediaTypes.Contains(context.MediaType);
        }

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
        public async Task<object> ReadAsync(InputFormatterContext context, Stream stream)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            return await JsonSerializer.DeserializeAsync(stream, context.DataType, JsonSerializerOptions, context.CancellationToken).ConfigureAwait(false);
        }

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
        public async Task WriteAsync(OutputFormatterContext context, Stream stream)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!SupportedMediaTypes.Contains(context.MediaType))
                throw new NotSupportedException($"The {nameof(JsonFormatter)} cannot write content of type '{context.MediaType}'.");

            await JsonSerializer.SerializeAsync(stream, context.Data, JsonSerializerOptions, context.CancellationToken).ConfigureAwait(false);
        }
    }
}

#endif // NET8_0_OR_GREATER
