using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StorageClass describes the parameters for a class of storage for which PersistentVolumes can be dynamically provisioned.
    ///     
    ///     StorageClasses are non-namespaced; the name of the storage class according to etcd is in ObjectMeta.Name.
    /// </summary>
    [KubeObject("StorageClass", "storage.k8s.io/v1beta1")]
    public partial class StorageClassV1Beta1 : Models.StorageClassV1Beta1
    {
        /// <summary>
        ///     Provisioner indicates the type of the provisioner.
        /// </summary>
        [JsonProperty("provisioner")]
        [YamlMember(Alias = "provisioner")]
        public override string Provisioner
        {
            get
            {
                return base.Provisioner;
            }
            set
            {
                base.Provisioner = value;

                __ModifiedProperties__.Add("Provisioner");
            }
        }


        /// <summary>
        ///     Parameters holds the parameters for the provisioner that should create volumes of this storage class.
        /// </summary>
        [YamlMember(Alias = "parameters")]
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
