using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     IngressBackend describes all endpoints for a given service and port.
    /// </summary>
    public partial class IngressBackendV1Beta1 : Models.IngressBackendV1Beta1
    {
        /// <summary>
        ///     Specifies the name of the referenced service.
        /// </summary>
        [JsonProperty("serviceName")]
        [YamlMember(Alias = "serviceName")]
        public override string ServiceName
        {
            get
            {
                return base.ServiceName;
            }
            set
            {
                base.ServiceName = value;

                __ModifiedProperties__.Add("ServiceName");
            }
        }


        /// <summary>
        ///     Specifies the port of the referenced service.
        /// </summary>
        [JsonProperty("servicePort")]
        [YamlMember(Alias = "servicePort")]
        public override string ServicePort
        {
            get
            {
                return base.ServicePort;
            }
            set
            {
                base.ServicePort = value;

                __ModifiedProperties__.Add("ServicePort");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
