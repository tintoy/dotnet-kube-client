using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatusCause provides more information about an api.Status failure, including cases when multiple errors are encountered.
    /// </summary>
    [KubeObject("StatusCause", "v1")]
    public class StatusCauseV1
    {
        /// <summary>
        ///     The field of the resource that has caused this error, as named by its JSON serialization. May include dot and postfix notation for nested attributes. Arrays are zero-indexed.  Fields may appear more than once in an array of causes due to fields having multiple errors. Optional.
        ///     
        ///     Examples:
        ///       "name" - the field "name" on the current resource
        ///       "items[0].name" - the field "name" on the first array entry in "items"
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }

        /// <summary>
        ///     A human-readable description of the cause of the error.  This field may be presented as-is to a reader.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     A machine-readable description of the cause of the error. If this value is empty there is no information available.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
