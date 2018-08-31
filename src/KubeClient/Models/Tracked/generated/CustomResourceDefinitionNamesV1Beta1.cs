using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CustomResourceDefinitionNames indicates the names to serve this CustomResourceDefinition
    /// </summary>
    public partial class CustomResourceDefinitionNamesV1Beta1 : Models.CustomResourceDefinitionNamesV1Beta1
    {
        /// <summary>
        ///     Kind is the serialized kind of the resource.  It is normally CamelCase and singular.
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
        ///     ListKind is the serialized kind of the list for this resource.  Defaults to &lt;kind&gt;List.
        /// </summary>
        [JsonProperty("listKind")]
        [YamlMember(Alias = "listKind")]
        public override string ListKind
        {
            get
            {
                return base.ListKind;
            }
            set
            {
                base.ListKind = value;

                __ModifiedProperties__.Add("ListKind");
            }
        }


        /// <summary>
        ///     Plural is the plural name of the resource to serve.  It must match the name of the CustomResourceDefinition-registration too: plural.group and it must be all lowercase.
        /// </summary>
        [JsonProperty("plural")]
        [YamlMember(Alias = "plural")]
        public override string Plural
        {
            get
            {
                return base.Plural;
            }
            set
            {
                base.Plural = value;

                __ModifiedProperties__.Add("Plural");
            }
        }


        /// <summary>
        ///     Singular is the singular name of the resource.  It must be all lowercase  Defaults to lowercased &lt;kind&gt;
        /// </summary>
        [JsonProperty("singular")]
        [YamlMember(Alias = "singular")]
        public override string Singular
        {
            get
            {
                return base.Singular;
            }
            set
            {
                base.Singular = value;

                __ModifiedProperties__.Add("Singular");
            }
        }


        /// <summary>
        ///     ShortNames are short names for the resource.  It must be all lowercase.
        /// </summary>
        [YamlMember(Alias = "shortNames")]
        [JsonProperty("shortNames", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> ShortNames { get; set; } = new List<string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
