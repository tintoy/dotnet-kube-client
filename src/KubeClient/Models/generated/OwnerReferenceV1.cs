using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     OwnerReference contains enough information to let you identify an owning object. An owning object must be in the same namespace as the dependent, or be cluster-scoped, so there is no namespace field.
    /// </summary>
    public partial class OwnerReferenceV1 : KubeObjectV1
    {
        /// <summary>
        ///     UID of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names#uids
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Include)]
        public string Uid { get; set; }

        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     If true, AND if the owner has the "foregroundDeletion" finalizer, then the owner cannot be deleted from the key-value store until this reference is removed. See https://kubernetes.io/docs/concepts/architecture/garbage-collection/#foreground-deletion for how the garbage collector interacts with this field and enforces the foreground deletion. Defaults to false. To set this field, a user needs "delete" permission of the owner, otherwise 422 (Unprocessable Entity) will be returned.
        /// </summary>
        [YamlMember(Alias = "blockOwnerDeletion")]
        [JsonProperty("blockOwnerDeletion", NullValueHandling = NullValueHandling.Ignore)]
        public bool? BlockOwnerDeletion { get; set; }

        /// <summary>
        ///     If true, this reference points to the managing controller.
        /// </summary>
        [YamlMember(Alias = "controller")]
        [JsonProperty("controller", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Controller { get; set; }
    }
}
