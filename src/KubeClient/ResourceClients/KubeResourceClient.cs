using HTTPlease;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Microsoft.Extensions.Logging;
    using Models;

    /// <summary>
    ///     The base class for Kubernetes resource API clients.
    /// </summary>
    public abstract class KubeResourceClient
    {
        /// <summary>
        ///     The default buffer size to use when streaming data from the Kubernetes API.
        /// </summary>
        protected const int DefaultStreamingBufferSize = 2048;

        /// <summary>
        ///     The media type used to indicate that request is a Kubernetes PATCH request.
        /// </summary>
        protected static readonly string PatchMediaType = "application/merge-patch+json";

        /// <summary>
        ///     JSON serialisation settings.
        /// </summary>
        protected internal static JsonSerializerSettings SerializerSettings => new JsonSerializerSettings
        {
            Converters =
            {
                new StringEnumConverter()
            }
        };

        /// <summary>
        ///     Create a new <see cref="KubeResourceClient"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public KubeResourceClient(KubeApiClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            KubeClient = client;
        }

        /// <summary>
        ///     The Kubernetes API client.
        /// </summary>
        public KubeApiClient KubeClient { get; }

        /// <summary>
        ///     The underlying HTTP client.
        /// </summary>
        protected HttpClient Http => KubeClient.Http;

        /// <summary>
        ///     An <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </summary>
        protected ILoggerFactory LoggerFactory => KubeClient.LoggerFactory;

        /// <summary>
        ///     Get a single resource, returning <c>null</c> if it does not exist.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of resource to retrieve.
        /// </typeparam>
        /// <param name="request">
        ///     An <see cref="HttpRequest"/> representing the resource to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the current state for the resource, or <c>null</c> if no resource was found with the specified name and namespace.
        /// </returns>
        protected async Task<TResource> GetSingleResource<TResource>(HttpRequest request, CancellationToken cancellationToken = default)
            where TResource : class
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken))
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.ReadContentAsAsync<TResource>();

                // Ensure that HttpStatusCode.NotFound actually refers to the target resource.
                StatusV1 status = await responseMessage.ReadContentAsAsync<StatusV1, StatusV1>(HttpStatusCode.NotFound);
                if (status.Reason != "NotFound")
                    throw new HttpRequestException<StatusV1>(responseMessage.StatusCode, status);

                return null;
            }
        }

        /// <summary>
        ///     Get an <see cref="IObservable{T}"/> for <see cref="ResourceEventV1{TResource}"/>s streamed from an HTTP GET request.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The resource type that the events relate to.
        /// </typeparam>
        /// <param name="request">
        ///     The <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <returns>
        ///     The <see cref="IObservable{T}"/>.
        /// </returns>
        protected IObservable<ResourceEventV1<TResource>> ObserveEvents<TResource>(HttpRequest request)
            where TResource : KubeResourceV1
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return ObserveLines(request).Select(
                line => JsonConvert.DeserializeObject<ResourceEventV1<TResource>>(line, SerializerSettings)
            );
        }

        /// <summary>
        ///     Get an <see cref="IObservable{T}"/> for lines streamed from an HTTP GET request.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <param name="bufferSize">
        ///     The buffer size to use when streaming data.
        /// 
        ///     Default is 2048 bytes.
        /// </param>
        /// <returns>
        ///     The <see cref="IObservable{T}"/>.
        /// </returns>
        protected IObservable<string> ObserveLines(HttpRequest request, int bufferSize = DefaultStreamingBufferSize)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            return Observable.Create<string>(async (subscriber, cancellationToken) =>
            {
                try
                {
                    using (HttpResponseMessage responseMessage = await Http.GetStreamedAsync(request, cancellationToken))
                    {
                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            throw HttpRequestException<StatusV1>.Create(responseMessage.StatusCode,
                                await responseMessage.ReadContentAsAsync<StatusV1, StatusV1>()
                            );
                        }

                        MediaTypeHeaderValue contentTypeHeader = responseMessage.Content.Headers.ContentType;
                        if (contentTypeHeader == null)
                            throw new InvalidOperationException("Response is missing 'Content-Type' header."); // TODO: Consider custom exception type.

                        Encoding encoding =
                            !String.IsNullOrWhiteSpace(contentTypeHeader.CharSet)
                                ? Encoding.GetEncoding(contentTypeHeader.CharSet)
                                : Encoding.UTF8;

                        Decoder decoder = encoding.GetDecoder();

                        using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
                        {
                            StringBuilder lineBuilder = new StringBuilder();
                            
                            byte[] buffer = new byte[bufferSize];
                            int bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                            while (bytesRead > 0)
                            {
                                // AF: Slightly inefficient because we wind up scanning the buffer twice.
                                char[] decodedCharacters = new char[decoder.GetCharCount(buffer, 0, bytesRead)];
                                int charactersDecoded = decoder.GetChars(buffer, 0, bytesRead, decodedCharacters, 0);
                                for (int charIndex = 0; charIndex < charactersDecoded; charIndex++)
                                {
                                    const char CR = '\r';
                                    const char LF = '\n';

                                    char decodedCharacter = decodedCharacters[charIndex];
                                    switch (decodedCharacter)
                                    {
                                        case CR:
                                        {
                                            if (charIndex < charactersDecoded - 1 && decodedCharacters[charIndex + 1] == LF)
                                            {
                                                charIndex++;

                                                goto case LF;
                                            }

                                            break;
                                        }
                                        case LF:
                                        {
                                            string line = lineBuilder.ToString();
                                            lineBuilder.Clear();

                                            subscriber.OnNext(line);

                                            break;
                                        }
                                        default:
                                        {
                                            lineBuilder.Append(decodedCharacter);

                                            break;
                                        }
                                    }
                                }

                                bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                            }

                            // If stream doesn't end with a line-terminator sequence, publish trailing characters as the last line.
                            if (lineBuilder.Length > 0)
                            {
                                subscriber.OnNext(
                                    lineBuilder.ToString()
                                );
                            }
                        }
                    }
                }
                catch (OperationCanceledException operationCanceled) when (operationCanceled.CancellationToken != cancellationToken)
                {
                    if (!cancellationToken.IsCancellationRequested) // Don't bother publishing if subscriber has already disconnected.
                        subscriber.OnError(operationCanceled);
                }
                catch (Exception exception)
                {
                    if (!cancellationToken.IsCancellationRequested) // Don't bother publishing if subscriber has already disconnected.
                        subscriber.OnError(exception);
                }
                finally
                {
                    if (!cancellationToken.IsCancellationRequested) // Don't bother publishing if subscriber has already disconnected.
                        subscriber.OnCompleted();
                }
            });
        }
    }
}
