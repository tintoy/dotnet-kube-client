using HTTPlease;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes APIResources (v1) API.
    /// </summary>
    public class APIResourceClientV1
        : KubeResourceClient, IAPIResourceClientV1
    {
        /// <summary>
        ///     Create a new <see cref="APIResourceClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public APIResourceClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get all APIResources with the specified prefix.
        /// </summary>
        /// <param name="prefix">
        ///     The prefix for the API groups (usually "api/v1" for core APIs or "apis" for all other APIs).
        /// </param>
        /// <param name="groupVersion">
        ///     The API group version.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="APIResourceListV1"/> containing the APIResources.
        /// </returns>
        public async Task<APIResourceListV1> List(string prefix, string groupVersion, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'prefix'.", nameof(prefix));

            HttpRequest request = Requests.Collection
                .WithTemplateParameters(new
                {
                    Prefix = prefix,
                    GroupVersion = groupVersion
                });

            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.ReadContentAsAsync<APIResourceListV1>().ConfigureAwait(false);

                throw new KubeClientException($"Unable to list API resource types for '{prefix}/{groupVersion}' (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode,
                        response: await responseMessage.ReadContentAsStatusV1Async(responseMessage.StatusCode).ConfigureAwait(false)
                    )
                );
            }
        }

        /// <summary>
        ///     Request templates for the APIResource (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level APIResource (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("{Prefix}/{GroupVersion}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes APIResources (v1) API.
    /// </summary>
    public interface IAPIResourceClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get all APIResources.
        /// </summary>
        /// <param name="prefix">
        ///     The prefix for the API groups (usually "api/v1" for core APIs or "apis" for all other APIs).
        /// </param>
        /// <param name="groupVersion">
        ///     The API group version.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="APIResourceListV1"/> containing the APIResources.
        /// </returns>
        Task<APIResourceListV1> List(string prefix, string groupVersion, CancellationToken cancellationToken = default);
    }
}
