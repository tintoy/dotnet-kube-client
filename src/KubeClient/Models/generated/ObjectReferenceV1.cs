using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectReference contains enough information to let you inspect or modify the referred object.
    /// </summary>
    public partial class ObjectReferenceV1
    {
        /// <summary>
        ///     Kind of the referent. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public virtual string Kind { get; set; }

        /// <summary>
        ///     UID of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#uids
        /// </summary>
        [JsonProperty("uid")]
        [YamlMember(Alias = "uid")]
        public virtual string Uid { get; set; }

        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Namespace of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
        /// </summary>
        [JsonProperty("namespace")]
        [YamlMember(Alias = "namespace")]
        public virtual string Namespace { get; set; }

        /// <summary>
        ///     If referring to a piece of an object instead of an entire object, this string should contain a valid JSON/Go field access statement, such as desiredState.manifest.containers[2]. For example, if the object reference is to a container within a pod, this would take on a value like: "spec.containers{name}" (where "name" refers to the name of the container that triggered the event) or if no container name is specified "spec.containers[2]" (container with index 2 in this pod). This syntax is chosen only to have some well-defined way of referencing a part of an object.
        /// </summary>
        [JsonProperty("fieldPath")]
        [YamlMember(Alias = "fieldPath")]
        public virtual string FieldPath { get; set; }

        /// <summary>
        ///     API version of the referent.
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public virtual string ApiVersion { get; set; }

        /// <summary>
        ///     Specific resourceVersion to which this reference is made, if any. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#concurrency-control-and-consistency
        /// </summary>
        [JsonProperty("resourceVersion")]
        [YamlMember(Alias = "resourceVersion")]
        public virtual string ResourceVersion { get; set; }
    }
}
