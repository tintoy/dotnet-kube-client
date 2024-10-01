using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class GRPCActionV1
    {
        /// <summary>
        ///     Service is the name of the service to place in the gRPC HealthCheckRequest (see https://github.com/grpc/grpc/blob/master/doc/health-checking.md).
        ///     
        ///     If this is not specified, the default behavior is defined by gRPC.
        /// </summary>
        [YamlMember(Alias = "service")]
        [JsonProperty("service", NullValueHandling = NullValueHandling.Ignore)]
        public string Service { get; set; }

        /// <summary>
        ///     Port number of the gRPC service. Number must be in the range 1 to 65535.
        /// </summary>
        [YamlMember(Alias = "port")]
        [JsonProperty("port", NullValueHandling = NullValueHandling.Include)]
        public int Port { get; set; }
    }
}
