using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Flocker volume mounted by the Flocker agent. One and only one of datasetName and datasetUUID should be set. Flocker volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    [KubeObject("FlockerVolumeSource", "v1")]
    public class FlockerVolumeSourceV1
    {
        /// <summary>
        ///     UUID of the dataset. This is unique identifier of a Flocker dataset
        /// </summary>
        [JsonProperty("datasetUUID")]
        public string DatasetUUID { get; set; }

        /// <summary>
        ///     Name of the dataset stored as metadata -> name on the dataset for Flocker should be considered as deprecated
        /// </summary>
        [JsonProperty("datasetName")]
        public string DatasetName { get; set; }
    }
}
