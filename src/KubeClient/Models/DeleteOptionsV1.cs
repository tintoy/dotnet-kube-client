using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeleteOptions may be provided when deleting an API object.
    /// </summary>
    [KubeResource("DeleteOptions", "v1")]
    public class DeleteOptionsV1
    {
        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; }

        /// <summary>
        ///     APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#resources
        /// </summary>
        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        ///     The duration in seconds before the object should be deleted. Value must be non-negative integer. The value zero indicates delete immediately. If this value is nil, the default grace period for the specified type will be used. Defaults to a per object value if not specified. zero means delete immediately.
        /// </summary>
        [JsonProperty("gracePeriodSeconds")]
        public int? GracePeriodSeconds { get; set; }

        /// <summary>
        ///     Deprecated: please use the PropagationPolicy, this field will be deprecated in 1.7. Should the dependent objects be orphaned. If true/false, the "orphan" finalizer will be added to/removed from the object's finalizers list. Either this field or PropagationPolicy may be set, but not both.
        /// </summary>
        [JsonProperty("orphanDependents")]
        public bool OrphanDependents { get; set; }

        /// <summary>
        ///     Must be fulfilled before a deletion is carried out. If not possible, a 409 Conflict status will be returned.
        /// </summary>
        [JsonProperty("preconditions")]
        public PreconditionsV1 Preconditions { get; set; }

        /// <summary>
        ///     Whether and how garbage collection will be performed. Either this field or OrphanDependents may be set, but not both. The default policy is decided by the existing finalizer set in the metadata.finalizers and the resource-specific default policy.
        /// </summary>
        [JsonProperty("propagationPolicy")]
        public string PropagationPolicy { get; set; }
    }
}
