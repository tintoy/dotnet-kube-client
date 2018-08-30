using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     OwnerReference contains enough information to let you identify an owning object. Currently, an owning object must be in the same namespace, so there is no namespace field.
    /// </summary>
    public partial class OwnerReferenceV1
    {
        /// <summary>
        ///     Kind of the referent. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public virtual string Kind { get; set; }

        /// <summary>
        ///     UID of the referent. More info: http://kubernetes.io/docs/user-guide/identifiers#uids
        /// </summary>
        [JsonProperty("uid")]
        [YamlMember(Alias = "uid")]
        public virtual string Uid { get; set; }

        /// <summary>
        ///     Name of the referent. More info: http://kubernetes.io/docs/user-guide/identifiers#names
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     API version of the referent.
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public virtual string ApiVersion { get; set; }

        /// <summary>
        ///     If true, AND if the owner has the "foregroundDeletion" finalizer, then the owner cannot be deleted from the key-value store until this reference is removed. Defaults to false. To set this field, a user needs "delete" permission of the owner, otherwise 422 (Unprocessable Entity) will be returned.
        /// </summary>
        [JsonProperty("blockOwnerDeletion")]
        [YamlMember(Alias = "blockOwnerDeletion")]
        public virtual bool BlockOwnerDeletion { get; set; }

        /// <summary>
        ///     If true, this reference points to the managing controller.
        /// </summary>
        [JsonProperty("controller")]
        [YamlMember(Alias = "controller")]
        public virtual bool Controller { get; set; }
    }
}
