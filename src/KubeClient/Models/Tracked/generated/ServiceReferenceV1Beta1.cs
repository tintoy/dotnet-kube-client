using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ServiceReference holds a reference to Service.legacy.k8s.io
    /// </summary>
    public partial class ServiceReferenceV1Beta1 : Models.ServiceReferenceV1Beta1
    {
        /// <summary>
        ///     Name is the name of the service
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     Namespace is the namespace of the service
        /// </summary>
        [JsonProperty("namespace")]
        [YamlMember(Alias = "namespace")]
        public override string Namespace
        {
            get
            {
                return base.Namespace;
            }
            set
            {
                base.Namespace = value;

                __ModifiedProperties__.Add("Namespace");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
