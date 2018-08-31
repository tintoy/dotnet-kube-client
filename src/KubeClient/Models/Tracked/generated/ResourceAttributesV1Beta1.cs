using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ResourceAttributes includes the authorization attributes available for resource requests to the Authorizer interface
    /// </summary>
    public partial class ResourceAttributesV1Beta1 : Models.ResourceAttributesV1Beta1, ITracked
    {
        /// <summary>
        ///     Verb is a kubernetes resource API verb, like: get, list, watch, create, update, delete, proxy.  "*" means all.
        /// </summary>
        [JsonProperty("verb")]
        [YamlMember(Alias = "verb")]
        public override string Verb
        {
            get
            {
                return base.Verb;
            }
            set
            {
                base.Verb = value;

                __ModifiedProperties__.Add("Verb");
            }
        }


        /// <summary>
        ///     Name is the name of the resource being requested for a "get" or deleted for a "delete". "" (empty) means all.
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
        ///     Namespace is the namespace of the action being requested.  Currently, there is no distinction between no namespace and all namespaces "" (empty) is defaulted for LocalSubjectAccessReviews "" (empty) is empty for cluster-scoped resources "" (empty) means "all" for namespace scoped resources from a SubjectAccessReview or SelfSubjectAccessReview
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
        ///     Resource is one of the existing resource types.  "*" means all.
        /// </summary>
        [JsonProperty("resource")]
        [YamlMember(Alias = "resource")]
        public override string Resource
        {
            get
            {
                return base.Resource;
            }
            set
            {
                base.Resource = value;

                __ModifiedProperties__.Add("Resource");
            }
        }


        /// <summary>
        ///     Subresource is one of the existing resource types.  "" means none.
        /// </summary>
        [JsonProperty("subresource")]
        [YamlMember(Alias = "subresource")]
        public override string Subresource
        {
            get
            {
                return base.Subresource;
            }
            set
            {
                base.Subresource = value;

                __ModifiedProperties__.Add("Subresource");
            }
        }


        /// <summary>
        ///     Version is the API Version of the Resource.  "*" means all.
        /// </summary>
        [JsonProperty("version")]
        [YamlMember(Alias = "version")]
        public override string Version
        {
            get
            {
                return base.Version;
            }
            set
            {
                base.Version = value;

                __ModifiedProperties__.Add("Version");
            }
        }


        /// <summary>
        ///     Group is the API Group of the Resource.  "*" means all.
        /// </summary>
        [JsonProperty("group")]
        [YamlMember(Alias = "group")]
        public override string Group
        {
            get
            {
                return base.Group;
            }
            set
            {
                base.Group = value;

                __ModifiedProperties__.Add("Group");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
