using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectReference contains enough information to let you inspect or modify the referred object.
    /// </summary>
    public partial class ObjectReferenceV1 : KubeObjectV1
    {
        /// <summary>
        ///     UID of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#uids
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        public string Namespace { get; set; }

        /// <summary>
        ///     If referring to a piece of an object instead of an entire object, this string should contain a valid JSON/Go field access statement, such as desiredState.manifest.containers[2]. For example, if the object reference is to a container within a pod, this would take on a value like: "spec.containers{name}" (where "name" refers to the name of the container that triggered the event) or if no container name is specified "spec.containers[2]" (container with index 2 in this pod). This syntax is chosen only to have some well-defined way of referencing a part of an object.
        /// </summary>
        [YamlMember(Alias = "fieldPath")]
        [JsonProperty("fieldPath", NullValueHandling = NullValueHandling.Ignore)]
        public string FieldPath { get; set; }

        /// <summary>
        ///     Specific resourceVersion to which this reference is made, if any. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#concurrency-control-and-consistency
        /// </summary>
        [YamlMember(Alias = "resourceVersion")]
        [JsonProperty("resourceVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceVersion { get; set; }
    }
}
