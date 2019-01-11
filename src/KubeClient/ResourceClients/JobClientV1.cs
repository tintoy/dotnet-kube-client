using HTTPlease;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Jobs (v1) API.
    /// </summary>
    public class JobClientV1
        : KubeResourceClient, IJobClientV1
    {
        /// <summary>
        ///     Create a new <see cref="JobClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public JobClientV1(IKubeApiClient client)
            : base(client)
        {
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
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
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
        ///     A <see cref="JobListV1"/> containing the jobs.
        /// </returns>
        public async Task<JobListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<JobListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific Job.
        /// </summary>
        /// <param name="name">
        ///     The name of the job to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<JobV1>> Watch(string name, string kubeNamespace = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<JobV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                operationDescription: $"watch v1/Job '{name}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
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
        public IObservable<IResourceEventV1<JobV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<JobV1>(
                Requests.WatchCollection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                operationDescription: $"watch all v1/Jobs with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
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
                        Namespace = newJob?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newJob,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<JobV1>("create v1/Job resource");
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
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="JobV1"/> representing the job's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<JobV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<JobV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the Job (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Job (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection       = KubeRequest.Create("apis/batch/v1/namespaces/{Namespace}/jobs?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name Job (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName           = KubeRequest.Create("apis/batch/v1/namespaces/{Namespace}/jobs/{Name}");

            /// <summary>
            ///     A collection-level Job watch (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchCollection  = KubeRequest.Create("apis/batch/v1/watch/namespaces/{Namespace}/jobs?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A watch-by-name Job (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName      = KubeRequest.Create("apis/batch/v1/watch/namespaces/{Namespace}/jobs/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes Jobs (v1) API.
    /// </summary>
    public interface IJobClientV1
        : IKubeResourceClient
    {
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
        Task<JobV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

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
        ///     A <see cref="JobListV1"/> containing the jobs.
        /// </returns>
        Task<JobListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific Job.
        /// </summary>
        /// <param name="name">
        ///     The name of the job to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<JobV1>> Watch(string name, string kubeNamespace = null);

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
        IObservable<IResourceEventV1<JobV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

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
        Task<JobV1> Create(JobV1 newJob, CancellationToken cancellationToken = default);

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
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="JobV1"/> representing the job's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<JobV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
