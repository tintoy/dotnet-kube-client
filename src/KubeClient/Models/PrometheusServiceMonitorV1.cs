using Newtonsoft.Json;
using System;

namespace KubeClient.Models
{
    /// <summary>
    ///     A Prometheus ServiceMonitor (v1) resource.
    /// /// </summary>
    public class PrometheusServiceMonitorV1 : KubeResourceV1
    {
        /// <summary>
        ///     The service monitor specification.
        /// </summary>
        [JsonProperty("spec")]
        public PrometheusServiceMonitorSpecV1 Spec { get; set; }
    }
}
