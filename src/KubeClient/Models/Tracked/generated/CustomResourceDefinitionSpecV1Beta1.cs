using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CustomResourceDefinitionSpec describes how a user wants their resource to appear
    /// </summary>
    public partial class CustomResourceDefinitionSpecV1Beta1 : Models.CustomResourceDefinitionSpecV1Beta1
    {
        /// <summary>
        ///     Scope indicates whether this resource is cluster or namespace scoped.  Default is namespaced
        /// </summary>
        [JsonProperty("scope")]
        [YamlMember(Alias = "scope")]
        public override string Scope
        {
            get
            {
                return base.Scope;
            }
            set
            {
                base.Scope = value;

                __ModifiedProperties__.Add("Scope");
            }
        }


        /// <summary>
        ///     Validation describes the validation methods for CustomResources This field is alpha-level and should only be sent to servers that enable the CustomResourceValidation feature.
        /// </summary>
        [JsonProperty("validation")]
        [YamlMember(Alias = "validation")]
        public override Models.CustomResourceValidationV1Beta1 Validation
        {
            get
            {
                return base.Validation;
            }
            set
            {
                base.Validation = value;

                __ModifiedProperties__.Add("Validation");
            }
        }


        /// <summary>
        ///     Version is the version this resource belongs in
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
        ///     Group is the group this resource belongs in
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
        ///     Names are the names used to describe this custom resource
        /// </summary>
        [JsonProperty("names")]
        [YamlMember(Alias = "names")]
        public override Models.CustomResourceDefinitionNamesV1Beta1 Names
        {
            get
            {
                return base.Names;
            }
            set
            {
                base.Names = value;

                __ModifiedProperties__.Add("Names");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
