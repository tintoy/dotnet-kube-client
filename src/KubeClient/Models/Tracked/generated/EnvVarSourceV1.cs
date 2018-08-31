using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EnvVarSource represents a source for the value of an EnvVar.
    /// </summary>
    public partial class EnvVarSourceV1 : Models.EnvVarSourceV1, ITracked
    {
        /// <summary>
        ///     Selects a key of a ConfigMap.
        /// </summary>
        [JsonProperty("configMapKeyRef")]
        [YamlMember(Alias = "configMapKeyRef")]
        public override Models.ConfigMapKeySelectorV1 ConfigMapKeyRef
        {
            get
            {
                return base.ConfigMapKeyRef;
            }
            set
            {
                base.ConfigMapKeyRef = value;

                __ModifiedProperties__.Add("ConfigMapKeyRef");
            }
        }


        /// <summary>
        ///     Selects a field of the pod: supports metadata.name, metadata.namespace, metadata.labels, metadata.annotations, spec.nodeName, spec.serviceAccountName, status.hostIP, status.podIP.
        /// </summary>
        [JsonProperty("fieldRef")]
        [YamlMember(Alias = "fieldRef")]
        public override Models.ObjectFieldSelectorV1 FieldRef
        {
            get
            {
                return base.FieldRef;
            }
            set
            {
                base.FieldRef = value;

                __ModifiedProperties__.Add("FieldRef");
            }
        }


        /// <summary>
        ///     Selects a resource of the container: only resources limits and requests (limits.cpu, limits.memory, requests.cpu and requests.memory) are currently supported.
        /// </summary>
        [JsonProperty("resourceFieldRef")]
        [YamlMember(Alias = "resourceFieldRef")]
        public override Models.ResourceFieldSelectorV1 ResourceFieldRef
        {
            get
            {
                return base.ResourceFieldRef;
            }
            set
            {
                base.ResourceFieldRef = value;

                __ModifiedProperties__.Add("ResourceFieldRef");
            }
        }


        /// <summary>
        ///     Selects a key of a secret in the pod's namespace
        /// </summary>
        [JsonProperty("secretKeyRef")]
        [YamlMember(Alias = "secretKeyRef")]
        public override Models.SecretKeySelectorV1 SecretKeyRef
        {
            get
            {
                return base.SecretKeyRef;
            }
            set
            {
                base.SecretKeyRef = value;

                __ModifiedProperties__.Add("SecretKeyRef");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
