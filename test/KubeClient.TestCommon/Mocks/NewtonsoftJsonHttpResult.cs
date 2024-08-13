using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KubeClient.TestCommon.Mocks
{
    /// <summary>
    ///     A minimal API result that serialises its value using the Newtonsoft JSON serialiser.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class NewtonsoftJsonHttpResult<TValue>
        : IResult, IStatusCodeHttpResult, IValueHttpResult, IValueHttpResult<TValue>, IContentTypeHttpResult
    {
        public NewtonsoftJsonHttpResult(TValue value, int? statusCode = null, string contentType = "application/json")
        {
            if (String.IsNullOrWhiteSpace(contentType))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(contentType)}.", nameof(contentType));

            Value = value;
            StatusCode = statusCode;
            ContentType = contentType;
        }

        /// <summary>
        ///     The value to be serialised and sent as the response body.
        /// </summary>
        public TValue Value { get; }


        /// <summary>
        ///     The response status code (if <c>null</c>, the existing response status code is used).
        /// </summary>
        public int? StatusCode { get; }

        /// <summary>
        ///     The response content type.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        ///     The value to be serialised and sent as the response body.
        /// </summary>
        object IValueHttpResult.Value => Value;

        /// <summary>
        ///     Execute the result.
        /// </summary>
        /// <param name="httpContext">
        ///     The <see cref="HttpContext"/> for the current request.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task ExecuteAsync(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            if (StatusCode != null)
                httpContext.Response.StatusCode = StatusCode.Value;

            httpContext.Response.ContentType = ContentType;

            JsonSerializer serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings
            {
                Converters =
                {
                    new StringEnumConverter(),
                },
                DefaultValueHandling = DefaultValueHandling.Ignore,
            });

            using (MemoryStream buffer = new MemoryStream())
            using (StreamWriter bufferWriter = new StreamWriter(buffer))
            {
                serializer.Serialize(bufferWriter, Value,
                    objectType: typeof(TValue)
                );

                bufferWriter.Flush();

                buffer.Seek(0, SeekOrigin.Begin);

                await buffer.CopyToAsync(httpContext.Response.Body,
                    cancellationToken: httpContext.RequestAborted
                );
            }
        }
    }
}
