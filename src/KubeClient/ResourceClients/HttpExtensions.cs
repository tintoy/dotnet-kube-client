using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Http;
    using Http.Formatters;
    using Http.Formatters.Json;
    using Models;

    /// <summary>
    ///     Extension methods for KubeClient.Http types.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// The CLR <see cref="Type"/> representing <see cref="KubeResourceV1"/>.
        /// </summary>
        static readonly TypeInfo KubeResourceV1TypeInfo = typeof(KubeResourceV1).GetTypeInfo();

        /// <summary>
        ///     Read response content as a <see cref="StatusV1"/>.
        /// </summary>
        /// <param name="response">
        ///     A <see cref="Task{TResult}"/> representing the HTTP response.
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="StatusV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<StatusV1> ReadContentAsStatusV1Async(this Task<HttpResponseMessage> response, params HttpStatusCode[] successStatusCodes)
        {
            try
            {
                return await response.ReadContentAsAsync<StatusV1, StatusV1>(successStatusCodes);
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeApiException(requestError.Response, requestError);
            }
        }

        /// <summary>
        ///     Read response content as a <see cref="StatusV1"/>.
        /// </summary>
        /// <param name="response">
        ///     The HTTP response.
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="StatusV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<StatusV1> ReadContentAsStatusV1Async(this HttpResponseMessage response, params HttpStatusCode[] successStatusCodes)
        {
            try
            {
                return await response.ReadContentAsAsync<StatusV1, StatusV1>(successStatusCodes);
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeApiException(requestError.Response, requestError);
            }
        }

        /// <summary>
        ///     Read response content as a <see cref="KubeObjectV1"/>.
        /// </summary>
        /// <param name="response">
        ///     The HTTP response.
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="KubeObjectV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<TObject> ReadContentAsObjectV1Async<TObject>(this Task<HttpResponseMessage> response, params HttpStatusCode[] successStatusCodes)
            where TObject : KubeObjectV1
        {
            try
            {
                return await response.ReadContentAsAsync<TObject, StatusV1>(successStatusCodes);
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeApiException(requestError.Response, requestError);
            }
        }

        /// <summary>
        ///     Read response content as either a <see cref="StatusV1"/> or a <typeparamref name="TResource"/> resource.<see cref="KubeObjectV1"/>.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The expected resource type.
        /// </typeparam>
        /// <param name="response">
        ///     The HTTP response.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation represented by the request (used in exception message if request was not successful).
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="KubeObjectV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<KubeResourceResultV1<TResource>> ReadContentAsResourceOrStatusV1<TResource>(this Task<HttpResponseMessage> response, string operationDescription, params HttpStatusCode[] successStatusCodes)
            where TResource : KubeResourceV1
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            (string expectedKind, string expectedApiVersion) = KubeObjectV1.GetKubeKind<TResource>();

            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await response;

                JObject responseJson = await responseMessage.ReadContentAsAsync<JObject, StatusV1>(successStatusCodes);

                string actualKind = responseJson.Value<string>("kind");
                if (actualKind == null)
                    throw new KubeClientException($"Unable to {operationDescription}: received an invalid response from the Kubernetes API (expected a resource, but response was missing 'kind' property).");

                string actualApiVersion = responseJson.Value<string>("apiVersion");
                if (actualKind == null)
                    throw new KubeClientException($"Unable to {operationDescription}: received an invalid response from the Kubernetes API (expected a resource, but response was missing 'apiVersion' property).");

                JsonSerializer serializer = responseMessage.GetJsonSerializer();

                if ((actualKind, actualApiVersion) == (expectedKind, expectedApiVersion))
                    return serializer.Deserialize<TResource>(responseJson.CreateReader());
                else if ((actualKind, actualApiVersion) == ("Status", "v1"))
                    return serializer.Deserialize<StatusV1>(responseJson.CreateReader());
                else
                    throw new KubeClientException($"Unable to {operationDescription}: received an unexpected response from the Kubernetes API (should be v1/Status or {expectedApiVersion}/{expectedKind}, but was {actualApiVersion}/{actualKind}).");
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeApiException(requestError.Response, requestError);
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        /// <summary>
        ///     Read response content as either a <see cref="StatusV1"/> or a resource model derived from <see cref="KubeResourceV1"/>.
        /// </summary>
        /// <param name="response">
        ///     The HTTP response.
        /// </param>
        /// <param name="modelType">
        ///     The CLR <see cref="Type"/> representing the model for the resource.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation represented by the request (used in exception message if request was not successful).
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="KubeObjectV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<KubeResourceResultV1<KubeResourceV1>> ReadContentAsResourceOrStatusV1(this Task<HttpResponseMessage> response, Type modelType, string operationDescription, params HttpStatusCode[] successStatusCodes)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            if (!KubeResourceV1TypeInfo.IsAssignableFrom(modelType.GetTypeInfo()))
                throw new ArgumentException($"Model type '{modelType.FullName}' does not derive from '{KubeResourceV1TypeInfo.FullName}'.", nameof(modelType));

            (string expectedKind, string expectedApiVersion) = KubeObjectV1.GetKubeKind(modelType);
            if (String.IsNullOrWhiteSpace(expectedKind))
                throw new ArgumentException($"Model type '{modelType.FullName}' has not been decorated with KubeResourceAttribute or KubeResourceListAttribute.", nameof(modelType));

            HttpResponseMessage responseMessage = null;

            try
            {
                responseMessage = await response;

                JObject responseJson = await responseMessage.ReadContentAsAsync<JObject, StatusV1>(successStatusCodes);

                string actualKind = responseJson.Value<string>("kind");
                if (actualKind == null)
                    throw new KubeClientException($"Unable to {operationDescription}: received an invalid response from the Kubernetes API (expected a resource, but response was missing 'kind' property).");

                string actualApiVersion = responseJson.Value<string>("apiVersion");
                if (actualKind == null)
                    throw new KubeClientException($"Unable to {operationDescription}: received an invalid response from the Kubernetes API (expected a resource, but response was missing 'apiVersion' property).");

                JsonSerializer serializer = responseMessage.GetJsonSerializer();

                if ((actualKind, actualApiVersion) == (expectedKind, expectedApiVersion))
                    return (KubeResourceV1)serializer.Deserialize(responseJson.CreateReader(), modelType);
                else if ((actualKind, actualApiVersion) == ("Status", "v1"))
                    return serializer.Deserialize<StatusV1>(responseJson.CreateReader());
                else
                    throw new KubeClientException($"Unable to {operationDescription}: received an unexpected response from the Kubernetes API (should be v1/Status or {expectedApiVersion}/{expectedKind}, but was {actualApiVersion}/{actualKind}).");
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeApiException(requestError.Response, requestError);
            }
            finally
            {
                responseMessage?.Dispose();
            }
        }

        /// <summary>
        ///     Read response content as a <see cref="KubeObjectV1"/>.
        /// </summary>
        /// <param name="response">
        ///     The HTTP response.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation represented by the request (used in exception message if request was not successful).
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="KubeObjectV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<TObject> ReadContentAsObjectV1Async<TObject>(this Task<HttpResponseMessage> response, string operationDescription, params HttpStatusCode[] successStatusCodes)
            where TObject : KubeObjectV1
        {
            try
            {
                return await response.ReadContentAsAsync<TObject, StatusV1>(successStatusCodes);
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeApiException($"Unable to {operationDescription}.", requestError);
            }
        }

        /// <summary>
        /// Creates a <see cref="JsonSerializer"/> using settings from the <see cref="IFormatter"/>s
        /// configured for the <paramref name="response"/>.
        /// Falls back to <see cref="KubeResourceClient.SerializerSettings"/> if there is none.
        /// </summary>
        public static JsonSerializer GetJsonSerializer(this HttpResponseMessage response)
        {
            return JsonSerializer.Create(response.GetFormatters().GetJsonSerializerSettings());
        }

        /// <summary>
        /// Gets the <see cref="JsonSerializerSettings"/> from a set of <paramref name="formatters"/>.
        /// Falls back to <see cref="KubeResourceClient.SerializerSettings"/> if there is none.
        /// </summary>
        public static JsonSerializerSettings GetJsonSerializerSettings(this IEnumerable<IFormatter> formatters)
        {
            return formatters.OfType<NewtonsoftJsonFormatter>().FirstOrDefault()?.SerializerSettings
                   ?? KubeResourceClient.SerializerSettings;
        }
    }
}