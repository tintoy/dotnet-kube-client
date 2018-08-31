using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a Flocker volume mounted by the Flocker agent. One and only one of datasetName and datasetUUID should be set. Flocker volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class FlockerVolumeSourceV1 : Models.FlockerVolumeSourceV1
    {
        /// <summary>
        ///     UUID of the dataset. This is unique identifier of a Flocker dataset
        /// </summary>
        [JsonProperty("datasetUUID")]
        [YamlMember(Alias = "datasetUUID")]
        public override string DatasetUUID
        {
            get
            {
                return base.DatasetUUID;
            }
            set
            {
                base.DatasetUUID = value;

                __ModifiedProperties__.Add("DatasetUUID");
            }
        }


        /// <summary>
        ///     Name of the dataset stored as metadata -&gt; name on the dataset for Flocker should be considered as deprecated
        /// </summary>
        [JsonProperty("datasetName")]
        [YamlMember(Alias = "datasetName")]
        public override string DatasetName
        {
            get
            {
                return base.DatasetName;
            }
            set
            {
                base.DatasetName = value;

                __ModifiedProperties__.Add("DatasetName");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
