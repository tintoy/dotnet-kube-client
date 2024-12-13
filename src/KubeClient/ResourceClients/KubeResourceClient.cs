using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Http;
    using Http.Formatters.Json;
    using Models;
    using Models.ContractResolvers;
    using Models.Converters;

    /// <summary>
    ///     The base class for Kubernetes resource API clients.
    /// </summary>
    public abstract class KubeResourceClient
        : IKubeResourceClient
    {
        /// <summary>
        ///     The default even
        /// </summary>
        public static readonly EventId DefaultEventId = new EventId(8500, typeof(KubeResourceClient).FullName);

        /// <summary>
        ///     The default buffer size to use when streaming data from the Kubernetes API.
        /// </summary>
        protected const int DefaultStreamingBufferSize = 2048;

        /// <summary>
        ///     The media type used to indicate that request is a Kubernetes PATCH request.
        /// </summary>
        protected static readonly string PatchMediaType = "application/json-patch+json";

        /// <summary>
        ///     The media type used to indicate that request is a Kubernetes merge-style PATCH request.
        /// </summary>
        protected static readonly string MergePatchMediaType = "application/merge-patch+json";

        /// <summary>
        ///     The media type used to indicate that request is a Kubernetes server-side-apply PATCH request in JSON format.
        /// </summary>
        protected static readonly string ApplyPatchJsonMediaType = "application/apply-patch+json";

        /// <summary>
        ///     The media type used to indicate that request is a Kubernetes server-side-apply PATCH request in YAML format.
        /// </summary>
        protected static readonly string ApplyPatchYamlMediaType = "application/apply-patch+yaml";

        /// <summary>
        ///     JSON serialisation settings.
        /// </summary>
        public static JsonSerializerSettings SerializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new KubeContractResolver(),
            Converters =
            {
                new StringEnumConverter(),
                new Int32OrStringV1Converter()
            }
        };

        /// <summary>
        ///     The factory for Kubernetes API requests.
        /// </summary>
        protected static HttpRequestFactory KubeRequest { get; } = new HttpRequestFactory(
            HttpRequest.Empty.ExpectJson().WithFormatter(new JsonFormatter
            {
                SerializerSettings = SerializerSettings,
                SupportedMediaTypes =
                {
                    PatchMediaType,
                    MergePatchMediaType
                }
            })
        );

        /// <summary>
        ///     Create a new <see cref="KubeResourceClient"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        protected KubeResourceClient(IKubeApiClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            KubeClient = client;
        }

        /// <summary>
        ///     The Kubernetes API client.
        /// </summary>
        public IKubeApiClient KubeClient { get; }

        /// <summary>
        ///     The Kubernetes API client (for <see cref="IKubeResourceClient"/>).
        /// </summary>
        IKubeApiClient IKubeResourceClient.KubeClient => KubeClient;

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
            where TResource : KubeResourceV1
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.ReadContentAsAsync<TResource>().ConfigureAwait(false);

                // Ensure that HttpStatusCode.NotFound actually refers to the target resource.
                StatusV1 status = await responseMessage.ReadContentAsStatusV1Async(HttpStatusCode.NotFound).ConfigureAwait(false);
                if (status.Reason == "NotFound")
                    return null;

                // If possible, tell the consumer which resource type we had a problem with (helpful when all you find is the error message in the log).
                (string itemKind, string itemApiVersion) = KubeObjectV1.GetKubeKind<TResource>();
                string resourceTypeDescription =
                    !String.IsNullOrWhiteSpace(itemKind)
                        ? $"{itemKind} ({itemApiVersion}) resource"
                        : typeof(TResource).Name;

                throw new KubeApiException($"Unable to retrieve {resourceTypeDescription} (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode,
                        response: await responseMessage.ReadContentAsStatusV1Async(responseMessage.StatusCode).ConfigureAwait(false)
                    )
                );
            }
        }

        /// <summary>
        ///     Get a list of resources.
        /// </summary>
        /// <typeparam name="TResourceList">
        ///     The type of resource list to retrieve.
        /// </typeparam>
        /// <param name="request">
        ///     An <see cref="HttpRequest"/> representing the resource to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResourceList"/> containing the resources.
        /// </returns>
        protected async Task<TResourceList> GetResourceList<TResourceList>(HttpRequest request, CancellationToken cancellationToken = default)
            where TResourceList : KubeResourceListV1
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.ReadContentAsAsync<TResourceList>().ConfigureAwait(false);

                // If possible, tell the consumer which resource type we had a problem with (helpful when all you find is the error message in the log).
                (string itemKind, string itemApiVersion) = KubeResourceListV1.GetListItemKubeKind<TResourceList>();
                string resourceTypeDescription =
                    !String.IsNullOrWhiteSpace(itemKind)
                        ? $"{itemKind} ({itemApiVersion}) resources"
                        : typeof(TResourceList).Name;

                throw new KubeApiException($"Unable to list {resourceTypeDescription} (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode,
                        response: await responseMessage.ReadContentAsStatusV1Async(responseMessage.StatusCode).ConfigureAwait(false)
                    )
                );
            }
        }

        /// <summary>
        ///     Perform a JSON patch operation on a Kubernetes resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The target resource type.
        /// </typeparam>
        /// <param name="patchAction">
        ///     A delegate that performs customisation of the patch operation.
        /// </param>
        /// <param name="request">
        ///     An <see cref="HttpRequest"/> representing the patch request.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the updated resource.
        /// </returns>
        protected async Task<TResource> PatchResource<TResource>(Action<JsonPatchDocument<TResource>> patchAction, HttpRequest request, CancellationToken cancellationToken)
            where TResource : KubeResourceV1
        {
            if (patchAction == null)
                throw new ArgumentNullException(nameof(patchAction));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // If possible, tell the consumer which resource type we had a problem with (helpful when all you find is the error message in the log).
            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();

            var patch = new JsonPatchDocument<TResource>();

            patchAction(patch);

            return await
                Http.PatchAsync(request,
                    patchBody: patch,
                    mediaType: PatchMediaType,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<TResource>(
                    operationDescription: $"patch {apiVersion}/{kind} resource"
                )
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Perform a JSON patch operation on a Kubernetes resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The target resource type.
        /// </typeparam>
        /// <param name="patchAction">
        ///     A delegate that performs customisation of the patch operation.
        /// </param>
        /// <param name="request">
        ///     An <see cref="HttpRequest"/> representing the patch request.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the updated resource.
        /// </returns>
        protected async Task<TResource> PatchResourceRaw<TResource>(Action<JsonPatchDocument> patchAction, HttpRequest request, CancellationToken cancellationToken)
            where TResource : KubeResourceV1
        {
            if (patchAction == null)
                throw new ArgumentNullException(nameof(patchAction));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // If possible, tell the consumer which resource type we had a problem with (helpful when all you find is the error message in the log).
            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();

            var patch = new JsonPatchDocument();
            patchAction(patch);

            return await
                Http.PatchAsync(request,
                    patchBody: patch,
                    mediaType: PatchMediaType,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<TResource>(
                    operationDescription: $"patch {apiVersion}/{kind} resource"
                )
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     Request deletion of the specified resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of resource to delete.
        /// </typeparam>
        /// <param name="resourceByNameRequestTemplate">
        ///     The HTTP request template for addressing a <typeparamref name="TResource"/> by name.
        /// </param>
        /// <param name="name">
        ///     The name of the resource to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="propagationPolicy">
        ///     A <see cref="DeletePropagationPolicy"/> indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the resource's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/> indicating the operation result.
        /// </returns>
        protected async Task<KubeResourceResultV1<TResource>> DeleteResource<TResource>(HttpRequest resourceByNameRequestTemplate, string name, string kubeNamespace, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
            where TResource : KubeResourceV1
        {
            if (resourceByNameRequestTemplate == null)
                throw new ArgumentNullException(nameof(resourceByNameRequestTemplate));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (String.IsNullOrWhiteSpace(kubeNamespace))
                kubeNamespace = KubeClient.DefaultNamespace;

            var response = Http.DeleteAsJsonAsync(
                resourceByNameRequestTemplate.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                deleteBody: new DeleteOptionsV1
                {
                    PropagationPolicy = propagationPolicy
                },
                cancellationToken: cancellationToken
            );

            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
            string operationDescription = $"delete {apiVersion}/{kind} resource '{name}' in namespace '{kubeNamespace}'";

            return await response.ReadContentAsResourceOrStatusV1<TResource>(operationDescription, HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request deletion of the specified global (non-namespaced) resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of resource to delete.
        /// </typeparam>
        /// <param name="resourceByNameNoNamespaceRequestTemplate">
        ///     The HTTP request template for addressing a non-namespaced <typeparamref name="TResource"/> by name.
        /// </param>
        /// <param name="name">
        ///     The name of the resource to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     A <see cref="DeletePropagationPolicy"/> indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the resource's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/> indicating the operation result.
        /// </returns>
        protected async Task<KubeResourceResultV1<TResource>> DeleteGlobalResource<TResource>(HttpRequest resourceByNameNoNamespaceRequestTemplate, string name, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
            where TResource : KubeResourceV1
        {
            if (resourceByNameNoNamespaceRequestTemplate == null)
                throw new ArgumentNullException(nameof(resourceByNameNoNamespaceRequestTemplate));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            var response = Http.DeleteAsJsonAsync(
                resourceByNameNoNamespaceRequestTemplate.WithTemplateParameters(new
                {
                    Name = name
                }),
                deleteBody: new DeleteOptionsV1
                {
                    PropagationPolicy = propagationPolicy
                },
                cancellationToken: cancellationToken
            );

            (string kind, string apiVersion) = KubeObjectV1.GetKubeKind<TResource>();
            string operationDescription = $"delete {apiVersion}/{kind} resource '{name}'";

            return await response.ReadContentAsResourceOrStatusV1<TResource>(operationDescription, HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Get an <see cref="IObservable{T}"/> for <see cref="IResourceEventV1{TResource}"/>s streamed from an HTTP GET request.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The resource type that the events relate to.
        /// </typeparam>
        /// <param name="request">
        ///     The <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation (used in error messages if the request fails).
        /// </param>
        /// <returns>
        ///     The <see cref="IObservable{T}"/>.
        /// </returns>
        protected IObservable<IResourceEventV1<TResource>> ObserveEvents<TResource>(HttpRequest request, string operationDescription)
            where TResource : KubeResourceV1
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(operationDescription))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'operationDescription'.", nameof(operationDescription));

            JsonSerializerSettings serializerSettings = request.GetFormatters().Values.GetJsonSerializerSettings();

            ILogger logger = LoggerFactory.CreateLogger(GetType());

            // If we have already observed any events, we only want to see ones newer than the last one we have seen so far.
            string lastObservedResourceVersion = null;

            return
                ObserveLinesWithRetry(operationDescription,
                    requestFactory: () =>
                    {
                        HttpRequest currentRequest = request;

                        if (!String.IsNullOrWhiteSpace(lastObservedResourceVersion))
                            currentRequest = currentRequest.WithQueryParameter("resourceVersion", lastObservedResourceVersion);

                        return currentRequest;
                    },
                    shouldRetry: exception => exception == null // Only retry if there was no exception
                )
                .Do(
                    line => CheckForEventError(line, operationDescription)
                )
                .Select(
                    line => (IResourceEventV1<TResource>)JsonConvert.DeserializeObject<ResourceEventV1<TResource>>(line, serializerSettings)
                )
                .Do(resourceEvent =>
                {
                    lastObservedResourceVersion = resourceEvent.Resource.Metadata.ResourceVersion;
                });
        }

        /// <summary>
        ///     Get an <see cref="IObservable{T}"/> for dynamically-typed <see cref="IResourceEventV1{TResource}"/>s streamed from an HTTP GET request.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation (used in error messages if the request fails).
        /// </param>
        /// <returns>
        ///     The <see cref="IObservable{T}"/>.
        /// </returns>
        /// <remarks>If you have custom model types you need to deserialise, ensure that their assemblies are added to <see cref="KubeClientOptions.ModelTypeAssemblies"/>.</remarks>
        protected IObservable<IResourceEventV1<KubeResourceV1>> ObserveEventsDynamic(HttpRequest request, string operationDescription)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(operationDescription))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'operationDescription'.", nameof(operationDescription));

            JsonSerializer eventSerializer = JsonSerializer.Create(
                request.GetFormatters().Values.GetJsonSerializerSettings()
            );
            eventSerializer.Converters.Add(
                new ResourceEventV1Converter(
                    KubeClient.GetClientOptions().ModelTypeAssemblies
                )
            );

            ILogger logger = LoggerFactory.CreateLogger(GetType());

            // If we have already observed any events, we only want to see ones newer than the last one we have seen so far.
            string lastObservedResourceVersion = null;

            return
                ObserveLinesWithRetry(operationDescription,
                    requestFactory: () =>
                    {
                        HttpRequest currentRequest = request;

                        if (!String.IsNullOrWhiteSpace(lastObservedResourceVersion))
                            currentRequest = currentRequest.WithQueryParameter("resourceVersion", lastObservedResourceVersion);

                        return currentRequest;
                    },
                    shouldRetry: exception => exception == null // Only retry if there was no exception
                )
                .Do(
                    line => CheckForEventError(line, operationDescription)
                )
                .Select(line =>
                {
                    IResourceEventV1<KubeResourceV1> resourceEvent;

                    using (StringReader lineReader = new StringReader(line))
                    using (JsonReader lineJsonReader = new JsonTextReader(lineReader))
                    {
                        resourceEvent = eventSerializer.Deserialize<ResourceEventV1<KubeResourceV1>>(lineJsonReader);
                    }

                    return resourceEvent;
                })
                .Do(resourceEvent =>
                {
                    lastObservedResourceVersion = resourceEvent.Resource.Metadata.ResourceVersion;
                });
        }

        /// <summary>
        ///     Get an <see cref="IObservable{T}"/> for lines streamed from an HTTP GET request.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation (used in error messages if the request fails).
        /// </param>
        /// <param name="bufferSize">
        ///     The buffer size to use when streaming data.
        /// 
        ///     Default is 2048 bytes.
        /// </param>
        /// <returns>
        ///     The <see cref="IObservable{T}"/>.
        /// </returns>
        protected IObservable<string> ObserveLines(HttpRequest request, string operationDescription, int bufferSize = DefaultStreamingBufferSize)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(operationDescription))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'operationDescription'.", nameof(operationDescription));

            return ObserveLines(
                requestFactory: () => request,
                operationDescription,
                bufferSize
            );
        }

        /// <summary>
        ///     Get an <see cref="IObservable{T}"/> for lines streamed from an HTTP GET request.
        /// </summary>
        /// <param name="requestFactory">
        ///     A delegate that produces the <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation (used in error messages if the request fails).
        /// </param>
        /// <param name="bufferSize">
        ///     The buffer size to use when streaming data.
        /// 
        ///     Default is 2048 bytes.
        /// </param>
        /// <returns>
        ///     The <see cref="IObservable{T}"/>.
        /// </returns>
        protected IObservable<string> ObserveLines(Func<HttpRequest> requestFactory, string operationDescription, int bufferSize = DefaultStreamingBufferSize)
        {
            if (requestFactory == null)
                throw new ArgumentNullException(nameof(requestFactory));

            if (String.IsNullOrWhiteSpace(operationDescription))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'operationDescription'.", nameof(operationDescription));

            return Observable.Create<string>(async (subscriber, subscriptionCancellationToken) =>
            {
                // NOTE: The CancellationToken above represents the subscriber (i.e. IObserver) subscription to this sequence (i.e. IObservable), and is canceled only when their subscription is disposed.

                ILogger logger = LoggerFactory.CreateLogger(GetType());

                try
                {
                    HttpRequest request = requestFactory();

                    logger.LogDebug("Start streaming {RequestMethod} request for {RequestUri}...", HttpMethod.Get.Method, request.Uri.PathAndQuery);

                    using (HttpResponseMessage responseMessage = await Http.GetStreamedAsync(request, subscriptionCancellationToken).ConfigureAwait(false))
                    {
                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            throw HttpRequestException<StatusV1>.Create(responseMessage.StatusCode,
                                await responseMessage.ReadContentAsStatusV1Async().ConfigureAwait(false)
                            );
                        }

                        MediaTypeHeaderValue contentTypeHeader = responseMessage.Content.Headers.ContentType;
                        if (contentTypeHeader == null)
                            throw new KubeClientException($"Unable to {operationDescription} (response is missing 'Content-Type' header).");

                        Encoding encoding =
                            !String.IsNullOrWhiteSpace(contentTypeHeader.CharSet)
                                ? Encoding.GetEncoding(contentTypeHeader.CharSet)
                                : Encoding.UTF8;

                        Decoder decoder = encoding.GetDecoder();

                        using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            StringBuilder lineBuilder = new StringBuilder();

                            byte[] buffer = new byte[bufferSize];
                            int bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length, subscriptionCancellationToken).ConfigureAwait(false);
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

                                bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length, subscriptionCancellationToken).ConfigureAwait(false);
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
                catch (OperationCanceledException operationCanceled) when (operationCanceled.CancellationToken == subscriptionCancellationToken)
                {
                    // Don't bother publishing if subscriber has already disconnected (this CancellationToken represents the subscription).
                }
                catch (HttpRequestException<StatusV1> requestError)
                {
                    logger.LogError(new EventId(0, "NoEventId"), requestError, "Unexpected error while streaming from the Kubernetes API to {operationDescription}.", operationDescription);

                    if (!subscriptionCancellationToken.IsCancellationRequested)
                    {
                        subscriber.OnError(
                            new KubeClientException($"Unexpected error while streaming from the Kubernetes API to {operationDescription}.", requestError)
                        );
                    }
                }
                catch (Exception exception)
                {
                    logger.LogError(new EventId(0, "NoEventId"), exception, "Unexpected error while streaming from the Kubernetes API to {operationDescription}.", operationDescription);

                    if (!subscriptionCancellationToken.IsCancellationRequested)
                        subscriber.OnError(exception);
                }
                finally
                {
                    if (!subscriptionCancellationToken.IsCancellationRequested) // Don't bother publishing if subscriber has already disconnected.
                        subscriber.OnCompleted();
                }
            });
        }

        /// <summary>
        ///     Get an <see cref="IObservable{T}"/> (with automatic retry) for lines streamed from an HTTP GET request.
        /// </summary>
        /// <param name="operationDescription">
        ///     A short description of the operation (used in error messages if the request fails).
        /// </param>
        /// <param name="requestFactory">
        ///     A delegate that produces the <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <param name="shouldRetry">
        ///     A delegate that returns <c>true</c>, if the operation should be retried (i.e. sequence continues); otherwise, <c>false</c>.
        ///     
        ///     <para>
        ///         If the retry is due to successful completion of the underlying sequence of lines (<see cref="IObserver{T}.OnCompleted"/>), the exception passed to the delegate be <c>null</c>.
        ///         If the retry is due to an exception (<see cref="IObserver{T}.OnError(Exception)"/>), the exception will be passed to the delegate.
        ///     </para>
        /// </param>
        /// <param name="bufferSize">
        ///     The buffer size to use when streaming data.
        /// 
        ///     Default is 2048 bytes.
        /// </param>
        /// <returns>
        ///     The <see cref="IObservable{T}"/>.
        /// </returns>
        protected IObservable<string> ObserveLinesWithRetry(string operationDescription, Func<HttpRequest> requestFactory, Func<Exception, bool> shouldRetry, int bufferSize = DefaultStreamingBufferSize)
        {
            if (requestFactory == null)
                throw new ArgumentNullException(nameof(requestFactory));

            if (String.IsNullOrWhiteSpace(operationDescription))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(operationDescription)}.", nameof(operationDescription));

            if (shouldRetry == null)
                throw new ArgumentNullException(nameof(shouldRetry));

            HttpRequest currentRequest = requestFactory();

            return ObserveLines(requestFactory, operationDescription, bufferSize)
                .RetryWhen(exceptions => Observable.Create((IObserver<Unit> retrySignal) =>
                {
                    return exceptions.Subscribe(
                        onNext: (Exception sourceError) =>
                        {
                            if (shouldRetry(sourceError))
                                retrySignal.OnNext(Unit.Default); // Retry (this will seamlessly continue the sequence).
                            else
                                retrySignal.OnError(sourceError); // Bubble up (this will terminate the sequence with an error).
                        },
                        onError: (Exception exceptionSourceError) =>
                        {
                            // Under normal circumstances this should not be called, and so we never retry from here.
                            retrySignal.OnError(exceptionSourceError); // Bubble up (this will terminate the sequence with an error).
                        },
                        onCompleted: () =>
                        {
                            if (shouldRetry(null))
                                retrySignal.OnNext(Unit.Default); // Retry (this will seamlessly continue the sequence).
                            else
                                retrySignal.OnCompleted(); // Bubble up (this will terminate the sequence).
                        }
                    );
                }));
        }

        /// <summary>
        ///     Check if an error was encountered in an event stream.
        /// </summary>
        /// <param name="line">
        ///     The current line in the event stream.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation being performed (used in exception message if an error is encountered).
        /// </param>
        static void CheckForEventError(string line, string operationDescription)
        {
            JToken watchEvent = JToken.Parse(line);
            if (!watchEvent.SelectToken("type").Value<string>().Equals("error", StringComparison.OrdinalIgnoreCase))
                return;

            StatusV1 status = watchEvent.SelectToken("object").ToObject<StatusV1>();

            throw new KubeApiException($"Unable to {operationDescription}.", status);
        }
    }
}
