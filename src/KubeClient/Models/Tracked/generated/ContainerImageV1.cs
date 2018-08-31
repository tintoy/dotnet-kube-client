using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Describe a container image
    /// </summary>
    public partial class ContainerImageV1 : Models.ContainerImageV1
    {
        /// <summary>
        ///     Names by which this image is known. e.g. ["gcr.io/google_containers/hyperkube:v1.0.7", "dockerhub.io/google_containers/hyperkube:v1.0.7"]
        /// </summary>
        [YamlMember(Alias = "names")]
        [JsonProperty("names", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Names { get; set; } = new List<string>();

        /// <summary>
        ///     The size of the image in bytes.
        /// </summary>
        [JsonProperty("sizeBytes")]
        [YamlMember(Alias = "sizeBytes")]
        public override int SizeBytes
        {
            get
            {
                return base.SizeBytes;
            }
            set
            {
                base.SizeBytes = value;

                __ModifiedProperties__.Add("SizeBytes");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
