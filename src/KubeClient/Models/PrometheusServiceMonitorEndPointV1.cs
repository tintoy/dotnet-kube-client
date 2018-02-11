using Newtonsoft.Json;

namespace KubeClient.Models
{
    /// <summary>
    ///     Defines a Prometheus exporter end-point that a ServiceMonitor will connect to.
    /// </summary>
    public class PrometheusServiceMonitorEndPointV1
    {
        /// <summary>
        ///     The port name or number that the Prometheus exporter(s) listen on.
        /// </summary>
        [JsonProperty("port")]
        public string Port { get; set; }
    }
}
