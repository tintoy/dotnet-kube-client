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
    ///     A client for the Kubernetes APIGroups (v1) API.
    /// </summary>
    public class APIGroupClientV1
        : KubeResourceClient, IAPIGroupClientV1
    {
        /// <summary>
        ///     Create a new <see cref="APIGroupClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public APIGroupClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get all APIGroups with the specified prefix.
        /// </summary>
        /// <param name="prefix">
        ///     The prefix for the API groups (usually "api/v1" for core APIs or "apis" for all other APIs).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="APIGroupListV1"/> containing the APIGroups.
        /// </returns>
        public async Task<APIGroupListV1> List(string prefix = "apis", CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'prefix'.", nameof(prefix));

            HttpRequest request = Requests.Collection.WithTemplateParameter("Prefix", prefix);

            using (HttpResponseMessage responseMessage = await Http.GetAsync(request, cancellationToken).ConfigureAwait(false))
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.ReadContentAsAsync<APIGroupListV1>().ConfigureAwait(false);

                throw new KubeClientException($"Unable to list API groups (HTTP status {responseMessage.StatusCode}).",
                    innerException: new HttpRequestException<StatusV1>(responseMessage.StatusCode,
                        response: await responseMessage.ReadContentAsStatusV1Async(responseMessage.StatusCode).ConfigureAwait(false)
                    )
                );
            }
        }

        /// <summary>
        ///     Request templates for the APIGroup (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level APIGroup (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("{Prefix}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes APIGroups (v1) API.
    /// </summary>
    public interface IAPIGroupClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get all APIGroups.
        /// </summary>
        /// <param name="prefix">
        ///     The prefix for the API groups (usually "api/v1" for core APIs or "/apis" for all other APIs).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="APIGroupListV1"/> containing the APIGroups.
        /// </returns>
        Task<APIGroupListV1> List(string prefix = "apis", CancellationToken cancellationToken = default);
    }
}
