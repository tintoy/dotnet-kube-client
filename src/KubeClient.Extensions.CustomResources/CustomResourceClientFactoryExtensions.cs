using System;

namespace KubeClient
{
    using ResourceClients;

    /// <summary>
    ///     CRD-related extension methods for <see cref="KubeClient"/>.
    /// </summary>
    public static class CustomResourceClientFactoryExtensions
    {
        /// <summary>
        ///     Get the Kubernetes CustomResourceDefinitions (v1beta1) resource client.
        /// </summary>
        /// <param name="kubeClient">
        ///     The Kubernetes API client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public static ICustomResourceDefinitionClientV1Beta1 CustomResourceDefinitionsV1Beta1(this KubeApiClient kubeClient)
        {
            if (kubeClient == null)
                throw new ArgumentNullException(nameof(kubeClient));
            
            return kubeClient.ResourceClient(
                client => new CustomResourceDefinitionClientV1Beta1(client)
            );
        }
    }
}
