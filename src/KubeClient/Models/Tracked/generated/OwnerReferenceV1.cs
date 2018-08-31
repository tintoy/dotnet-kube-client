using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     OwnerReference contains enough information to let you identify an owning object. Currently, an owning object must be in the same namespace, so there is no namespace field.
    /// </summary>
    public partial class OwnerReferenceV1 : Models.OwnerReferenceV1
    {
        /// <summary>
        ///     Kind of the referent. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public override string Kind
        {
            get
            {
                return base.Kind;
            }
            set
            {
                base.Kind = value;

                __ModifiedProperties__.Add("Kind");
            }
        }


        /// <summary>
        ///     UID of the referent. More info: http://kubernetes.io/docs/user-guide/identifiers#uids
        /// </summary>
        [JsonProperty("uid")]
        [YamlMember(Alias = "uid")]
        public override string Uid
        {
            get
            {
                return base.Uid;
            }
            set
            {
                base.Uid = value;

                __ModifiedProperties__.Add("Uid");
            }
        }


        /// <summary>
        ///     Name of the referent. More info: http://kubernetes.io/docs/user-guide/identifiers#names
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
        ///     API version of the referent.
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public override string ApiVersion
        {
            get
            {
                return base.ApiVersion;
            }
            set
            {
                base.ApiVersion = value;

                __ModifiedProperties__.Add("ApiVersion");
            }
        }


        /// <summary>
        ///     If true, AND if the owner has the "foregroundDeletion" finalizer, then the owner cannot be deleted from the key-value store until this reference is removed. Defaults to false. To set this field, a user needs "delete" permission of the owner, otherwise 422 (Unprocessable Entity) will be returned.
        /// </summary>
        [JsonProperty("blockOwnerDeletion")]
        [YamlMember(Alias = "blockOwnerDeletion")]
        public override bool BlockOwnerDeletion
        {
            get
            {
                return base.BlockOwnerDeletion;
            }
            set
            {
                base.BlockOwnerDeletion = value;

                __ModifiedProperties__.Add("BlockOwnerDeletion");
            }
        }


        /// <summary>
        ///     If true, this reference points to the managing controller.
        /// </summary>
        [JsonProperty("controller")]
        [YamlMember(Alias = "controller")]
        public override bool Controller
        {
            get
            {
                return base.Controller;
            }
            set
            {
                base.Controller = value;

                __ModifiedProperties__.Add("Controller");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
