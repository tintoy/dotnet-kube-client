using Newtonsoft.Json;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     The specification for a Prometheus ServiceMonitor (v1) resource.
    /// </summary>
    public class PrometheusServiceMonitorSpecV1
    {
        /// <summary>
        ///     The job label to apply to metrics collected by this job.
        /// </summary>
        [JsonProperty("jobLabel")]
        public string JobLabel { get; set; }

        /// <summary>
        ///     The selector used to determine which services are monitored by the ServiceMonitor.
        /// </summary>
        [JsonProperty("selector")]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     The Prometheus exporter end-points that the ServiceMonitor will connect to.
        /// </summary>
        [JsonProperty("endpoints")]
        public List<PrometheusServiceMonitorEndPointV1> EndPoints { get; set; }
    }
}
