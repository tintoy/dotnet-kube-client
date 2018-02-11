using Newtonsoft.Json;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     A Prometheus ServiceMonitor resource list (v1).
    /// /// </summary>
    public class PrometheusServiceMonitorListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     The resources in the list.
        /// </summary>
        [JsonProperty("items")]
        public List<PrometheusServiceMonitorV1> Items { get; set; } = new List<PrometheusServiceMonitorV1>();
    }
}
