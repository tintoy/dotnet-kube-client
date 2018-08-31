using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ObjectReference contains enough information to let you inspect or modify the referred object.
    /// </summary>
    public partial class ObjectReferenceV1 : Models.ObjectReferenceV1, ITracked
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
        ///     UID of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#uids
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
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
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
        ///     Namespace of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
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
        ///     If referring to a piece of an object instead of an entire object, this string should contain a valid JSON/Go field access statement, such as desiredState.manifest.containers[2]. For example, if the object reference is to a container within a pod, this would take on a value like: "spec.containers{name}" (where "name" refers to the name of the container that triggered the event) or if no container name is specified "spec.containers[2]" (container with index 2 in this pod). This syntax is chosen only to have some well-defined way of referencing a part of an object.
        /// </summary>
        [JsonProperty("fieldPath")]
        [YamlMember(Alias = "fieldPath")]
        public override string FieldPath
        {
            get
            {
                return base.FieldPath;
            }
            set
            {
                base.FieldPath = value;

                __ModifiedProperties__.Add("FieldPath");
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
        ///     Specific resourceVersion to which this reference is made, if any. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#concurrency-control-and-consistency
        /// </summary>
        [JsonProperty("resourceVersion")]
        [YamlMember(Alias = "resourceVersion")]
        public override string ResourceVersion
        {
            get
            {
                return base.ResourceVersion;
            }
            set
            {
                base.ResourceVersion = value;

                __ModifiedProperties__.Add("ResourceVersion");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
