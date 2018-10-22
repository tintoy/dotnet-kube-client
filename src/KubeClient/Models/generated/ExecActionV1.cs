using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExecAction describes a "run in container" action.
    /// </summary>
    public partial class ExecActionV1
    {
        /// <summary>
        ///     Command is the command line to execute inside the container, the working directory for the command  is root ('/') in the container's filesystem. The command is simply exec'd, it is not run inside a shell, so traditional shell instructions ('|', etc) won't work. To use a shell, you need to explicitly call out to that shell. Exit status of 0 is treated as live/healthy and non-zero is unhealthy.
        /// </summary>
        [YamlMember(Alias = "command")]
        [JsonProperty("command", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Command { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Command"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCommand() => Command.Count > 0;
    }
}
