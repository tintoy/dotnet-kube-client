using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DaemonEndpoint contains information about a single Daemon endpoint.
    /// </summary>
    public partial class DaemonEndpointV1 : Models.DaemonEndpointV1, ITracked
    {
        /// <summary>
        ///     Port number of the given endpoint.
        /// </summary>
        [JsonProperty("Port")]
        [YamlMember(Alias = "Port")]
        public override int Port
        {
            get
            {
                return base.Port;
            }
            set
            {
                base.Port = value;

                __ModifiedProperties__.Add("Port");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
