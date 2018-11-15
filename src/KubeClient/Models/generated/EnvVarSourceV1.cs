using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EnvVarSource represents a source for the value of an EnvVar.
    /// </summary>
    public partial class EnvVarSourceV1
    {
        /// <summary>
        ///     Selects a key of a ConfigMap.
        /// </summary>
        [YamlMember(Alias = "configMapKeyRef")]
        [JsonProperty("configMapKeyRef", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigMapKeySelectorV1 ConfigMapKeyRef { get; set; }

        /// <summary>
        ///     Selects a field of the pod: supports metadata.name, metadata.namespace, metadata.labels, metadata.annotations, spec.nodeName, spec.serviceAccountName, status.hostIP, status.podIP.
        /// </summary>
        [YamlMember(Alias = "fieldRef")]
        [JsonProperty("fieldRef", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectFieldSelectorV1 FieldRef { get; set; }

        /// <summary>
        ///     Selects a resource of the container: only resources limits and requests (limits.cpu, limits.memory, limits.ephemeral-storage, requests.cpu, requests.memory and requests.ephemeral-storage) are currently supported.
        /// </summary>
        [YamlMember(Alias = "resourceFieldRef")]
        [JsonProperty("resourceFieldRef", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceFieldSelectorV1 ResourceFieldRef { get; set; }

        /// <summary>
        ///     Selects a key of a secret in the pod's namespace
        /// </summary>
        [YamlMember(Alias = "secretKeyRef")]
        [JsonProperty("secretKeyRef", NullValueHandling = NullValueHandling.Ignore)]
        public SecretKeySelectorV1 SecretKeyRef { get; set; }
    }
}
