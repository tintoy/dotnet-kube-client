using System;
using System.Linq;

namespace KubeClient.Models
{
    /// <summary>
    ///     Extension methods for Kubernetes API models.
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        ///     Get the host name and port corresponding to the Service.
        /// </summary>
        /// <param name="service">
        ///     The Kubernetes <see cref="ServiceV1"/>.
        /// </param>
        /// <param name="portName">
        ///     The name of the port to use.
        /// </param>
        /// <returns>
        ///     The host name and port.
        /// </returns>
        public static (string hostName, int? port) GetHostAndPort(this ServiceV1 service, string portName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            if (String.IsNullOrWhiteSpace(portName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'portName'.", nameof(portName));

            string hostName = $"{service.Metadata.Name}.{service.Metadata.Namespace}.svc.cluster.local";
            
            ServicePortV1 targetPort = service.Spec.Ports.FirstOrDefault(
                servicePort => servicePort.Name == portName
            );

            int? port = null;
            if (targetPort != null)
            {
                if (service.Spec.Type == "NodePort")
                    port = targetPort.NodePort;
                else
                    port = targetPort.Port;
            }
            
            return (hostName, port);
        }
    }
}
