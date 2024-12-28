using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KubeClient.Http
{
    using Formatters;

    /// <summary>
    ///		Extension methods for working with <see cref="HttpContent"/>.
    /// </summary>
    public static class ContentExtensions
    {
        /// <summary>
        ///		Asynchronously read the body content as the specified type.
        /// </summary>
        /// <typeparam name="TBody">
        ///		The CLR data-type that the body content will be deserialised into.
        /// </typeparam>
        /// <param name="content">
        ///		The <see cref="HttpContent"/> to read.
        /// </param>
        /// <param name="formatter">
        ///		The content formatter used to deserialise the body content.
        /// </param>
        /// <returns>
        ///		The deserialised body content.
        /// </returns>
        public static Task<TBody> ReadAsAsync<TBody>(this HttpContent content, IInputFormatter formatter)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            InputFormatterContext formatterContext = content.CreateInputFormatterContext<TBody>();

            return content.ReadAsAsync<TBody>(formatter, formatterContext);
        }

        /// <summary>
        ///		Asynchronously read the body content as the specified type.
        /// </summary>
        /// <typeparam name="TBody">
        ///		The CLR data-type that the body content will be deserialised into.
        /// </typeparam>
        /// <param name="content">
        ///		The <see cref="HttpContent"/> to read.
        /// </param>
        /// <param name="formatter">
        ///		The content formatter used to deserialise the body content.
        /// </param>
        /// <param name="formatterContext">
        ///		Contextual information about the content body being deserialised.
        /// </param>
        /// <returns>
        ///		The deserialised body content.
        /// </returns>
        public static async Task<TBody> ReadAsAsync<TBody>(this HttpContent content, IInputFormatter formatter, InputFormatterContext formatterContext)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            if (formatterContext == null)
                throw new ArgumentNullException(nameof(formatterContext));

            using (Stream responseStream = await content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                object responseBody = await formatter.ReadAsync(formatterContext, responseStream).ConfigureAwait(false);

                return (TBody)responseBody;
            }
        }

        /// <summary>
        ///		Create an <see cref="InputFormatterContext"/> for reading the HTTP message content.
        /// </summary>
        /// <typeparam name="TBody">
        ///		The CLR data type into which the message body will be deserialised.
        /// </typeparam>
        /// <param name="content">
        ///		The HTTP message content.
        /// </param>
        /// <returns>
        ///		The configured <see cref="InputFormatterContext"/>.
        /// </returns>
        public static InputFormatterContext CreateInputFormatterContext<TBody>(this HttpContent content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            MediaTypeHeaderValue contentTypeHeader = content.Headers.ContentType;
            if (contentTypeHeader == null)
                throw new InvalidOperationException("Response is missing 'Content-Type' header."); // TODO: Consider custom exception type.

            Encoding encoding = !String.IsNullOrWhiteSpace(contentTypeHeader.CharSet) ?
                Encoding.GetEncoding(contentTypeHeader.CharSet)
                :
                Encoding.UTF8;

            return new InputFormatterContext(
                dataType: typeof(TBody),
                mediaType: contentTypeHeader.MediaType,
                encoding: encoding
            );
        }
    }
}
