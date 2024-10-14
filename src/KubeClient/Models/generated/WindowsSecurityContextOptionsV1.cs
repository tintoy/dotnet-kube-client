using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     WindowsSecurityContextOptions contain Windows-specific options and credentials.
    /// </summary>
    public partial class WindowsSecurityContextOptionsV1
    {
        /// <summary>
        ///     GMSACredentialSpec is where the GMSA admission webhook (https://github.com/kubernetes-sigs/windows-gmsa) inlines the contents of the GMSA credential spec named by the GMSACredentialSpecName field.
        /// </summary>
        [YamlMember(Alias = "gmsaCredentialSpec")]
        [JsonProperty("gmsaCredentialSpec", NullValueHandling = NullValueHandling.Ignore)]
        public string GmsaCredentialSpec { get; set; }

        /// <summary>
        ///     GMSACredentialSpecName is the name of the GMSA credential spec to use.
        /// </summary>
        [YamlMember(Alias = "gmsaCredentialSpecName")]
        [JsonProperty("gmsaCredentialSpecName", NullValueHandling = NullValueHandling.Ignore)]
        public string GmsaCredentialSpecName { get; set; }

        /// <summary>
        ///     The UserName in Windows to run the entrypoint of the container process. Defaults to the user specified in image metadata if unspecified. May also be set in PodSecurityContext. If set in both SecurityContext and PodSecurityContext, the value specified in SecurityContext takes precedence.
        /// </summary>
        [YamlMember(Alias = "runAsUserName")]
        [JsonProperty("runAsUserName", NullValueHandling = NullValueHandling.Ignore)]
        public string RunAsUserName { get; set; }

        /// <summary>
        ///     HostProcess determines if a container should be run as a 'Host Process' container. All of a Pod's containers must have the same effective HostProcess value (it is not allowed to have a mix of HostProcess containers and non-HostProcess containers). In addition, if HostProcess is true then HostNetwork must also be set to true.
        /// </summary>
        [YamlMember(Alias = "hostProcess")]
        [JsonProperty("hostProcess", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HostProcess { get; set; }
    }
}
