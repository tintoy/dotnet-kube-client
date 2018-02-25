using HTTPlease;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Jobs (v1) API.
    /// </summary>
    public class JobClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="JobClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public JobClientV1(KubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get all Jobs in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Jobs.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The Jobs, as a list of <see cref="JobV1"/>s.
        /// </returns>
        public async Task<List<JobV1>> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            JobListV1 matchingJobs =
                await Http.GetAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = kubeNamespace ?? Client.DefaultNamespace,
                        LabelSelector = labelSelector
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<JobListV1, StatusV1>();

            return matchingJobs.Items;
        }

        /// <summary>
        ///     Watch for events relating to Jobs.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Jobs.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<ResourceEventV1<JobV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<JobV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? Client.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                })
            );
        }

        /// <summary>
        ///     Get the Job with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Job to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="JobV1"/> representing the current state for the Job, or <c>null</c> if no Job was found with the specified name and namespace.
        /// </returns>
        public async Task<JobV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<JobV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? Client.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="JobV1"/>.
        /// </summary>
        /// <param name="newJob">
        ///     A <see cref="JobV1"/> representing the Job to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="JobV1"/> representing the current state for the newly-created Job.
        /// </returns>
        public async Task<JobV1> Create(JobV1 newJob, CancellationToken cancellationToken = default)
        {
            if (newJob == null)
                throw new ArgumentNullException(nameof(newJob));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newJob?.Metadata?.Namespace ?? Client.DefaultNamespace
                    }),
                    postBody: newJob,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<JobV1, StatusV1>();
        }

        /// <summary>
        ///     Request deletion of the specified Job.
        /// </summary>
        /// <param name="name">
        ///     The name of the Job to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace containing the Job to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     A <see cref="DeletePropagationPolicy"/> indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="JobV1"/> representing the job's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public async Task<KubeObjectV1> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy propagationPolicy = DeletePropagationPolicy.Background, CancellationToken cancellationToken = default)
        {
            var request = Http.DeleteAsJsonAsync(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? Client.DefaultNamespace
                }),
                deleteBody: new DeleteOptionsV1
                {
                    PropagationPolicy = propagationPolicy
                },
                cancellationToken: cancellationToken
            );
            
            if (propagationPolicy == DeletePropagationPolicy.Foreground)
                return await request.ReadContentAsObjectV1Async<JobV1>(HttpStatusCode.OK);
            
            return await request.ReadContentAsObjectV1Async<StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the Job (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Job (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("apis/batch/v1/namespaces/{Namespace}/jobs?labelSelector={LabelSelector?}&watch={Watch?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name Job (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("apis/batch/v1/namespaces/{Namespace}/jobs/{Name}", SerializerSettings);
        }
    }
}
