using System.Collections.Generic;
using KubeClient.Models;
using YamlDotNet.Serialization;

namespace KubeClient.Extensions.KubeConfig.Models
{
    /// <summary>
    ///     Configuration for a K8s client-go credential plugin.
    /// </summary>
    public class CredentialPluginConfig
    {
        /// <summary>
        ///     The plugin command.
        /// </summary>
        [YamlMember(Alias = "command")]
        public string Command { get; set; }

        /// <summary>
        ///     The API version to use when decoding the ExecCredentials resource.
        /// </summary>
        /// <remarks>
        ///     The API version returned by the plugin MUST match the version specified here.
        /// </remarks>
        [YamlMember(Alias = "apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        ///     Environment variables (if any) to set when running the plugin command.
        /// </summary>
        [YamlMember(Alias = "env")]
        public List<EnvVarV1> EnvironmentVariables { get; } = new List<EnvVarV1>();

        /// <summary>
        ///     Should <see cref="EnvironmentVariables"/> be serialised?
        /// </summary>
        /// <returns>
        ///     <c>true</c>, if <see cref="EnvironmentVariables"/> should be serialised; otherwise, <c>false</c>.
        /// </returns>
        public bool ShouldSerializeEnvironmentVariables() => EnvironmentVariables.Count > 0;

        /// <summary>
        ///     Command-line arguments (if any) to pass to the plugin command.
        /// </summary>
        [YamlMember(Alias = "args")]
        public List<string> Arguments { get; } = new List<string>();

        /// <summary>
        ///     Should <see cref="Arguments"/> be serialised?
        /// </summary>
        /// <returns>
        ///     <c>true</c>, if <see cref="Arguments"/> should be serialised; otherwise, <c>false</c>.
        /// </returns>
        public bool ShouldSerializeArguments() => Arguments.Count > 0;
    }
}
