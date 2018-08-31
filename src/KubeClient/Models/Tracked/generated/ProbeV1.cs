using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Probe describes a health check to be performed against a container to determine whether it is alive or ready to receive traffic.
    /// </summary>
    public partial class ProbeV1 : Models.ProbeV1
    {
        /// <summary>
        ///     One and only one of the following should be specified. Exec specifies the action to take.
        /// </summary>
        [JsonProperty("exec")]
        [YamlMember(Alias = "exec")]
        public override Models.ExecActionV1 Exec
        {
            get
            {
                return base.Exec;
            }
            set
            {
                base.Exec = value;

                __ModifiedProperties__.Add("Exec");
            }
        }


        /// <summary>
        ///     Minimum consecutive failures for the probe to be considered failed after having succeeded. Defaults to 3. Minimum value is 1.
        /// </summary>
        [JsonProperty("failureThreshold")]
        [YamlMember(Alias = "failureThreshold")]
        public override int FailureThreshold
        {
            get
            {
                return base.FailureThreshold;
            }
            set
            {
                base.FailureThreshold = value;

                __ModifiedProperties__.Add("FailureThreshold");
            }
        }


        /// <summary>
        ///     Minimum consecutive successes for the probe to be considered successful after having failed. Defaults to 1. Must be 1 for liveness. Minimum value is 1.
        /// </summary>
        [JsonProperty("successThreshold")]
        [YamlMember(Alias = "successThreshold")]
        public override int SuccessThreshold
        {
            get
            {
                return base.SuccessThreshold;
            }
            set
            {
                base.SuccessThreshold = value;

                __ModifiedProperties__.Add("SuccessThreshold");
            }
        }


        /// <summary>
        ///     Number of seconds after the container has started before liveness probes are initiated. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
        /// </summary>
        [JsonProperty("initialDelaySeconds")]
        [YamlMember(Alias = "initialDelaySeconds")]
        public override int InitialDelaySeconds
        {
            get
            {
                return base.InitialDelaySeconds;
            }
            set
            {
                base.InitialDelaySeconds = value;

                __ModifiedProperties__.Add("InitialDelaySeconds");
            }
        }


        /// <summary>
        ///     How often (in seconds) to perform the probe. Default to 10 seconds. Minimum value is 1.
        /// </summary>
        [JsonProperty("periodSeconds")]
        [YamlMember(Alias = "periodSeconds")]
        public override int PeriodSeconds
        {
            get
            {
                return base.PeriodSeconds;
            }
            set
            {
                base.PeriodSeconds = value;

                __ModifiedProperties__.Add("PeriodSeconds");
            }
        }


        /// <summary>
        ///     Number of seconds after which the probe times out. Defaults to 1 second. Minimum value is 1. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#container-probes
        /// </summary>
        [JsonProperty("timeoutSeconds")]
        [YamlMember(Alias = "timeoutSeconds")]
        public override int TimeoutSeconds
        {
            get
            {
                return base.TimeoutSeconds;
            }
            set
            {
                base.TimeoutSeconds = value;

                __ModifiedProperties__.Add("TimeoutSeconds");
            }
        }


        /// <summary>
        ///     HTTPGet specifies the http request to perform.
        /// </summary>
        [JsonProperty("httpGet")]
        [YamlMember(Alias = "httpGet")]
        public override Models.HTTPGetActionV1 HttpGet
        {
            get
            {
                return base.HttpGet;
            }
            set
            {
                base.HttpGet = value;

                __ModifiedProperties__.Add("HttpGet");
            }
        }


        /// <summary>
        ///     TCPSocket specifies an action involving a TCP port. TCP hooks not yet supported
        /// </summary>
        [JsonProperty("tcpSocket")]
        [YamlMember(Alias = "tcpSocket")]
        public override Models.TCPSocketActionV1 TcpSocket
        {
            get
            {
                return base.TcpSocket;
            }
            set
            {
                base.TcpSocket = value;

                __ModifiedProperties__.Add("TcpSocket");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
