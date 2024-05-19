using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;

namespace KubeClient.Extensions.DataProtection.Tests.Mocks
{
    /// <summary>
    ///     Represents a single operation within a JSON Patch request.
    /// </summary>
    /// <param name="Kind">
    ///     A <see cref="JsonPatchOperationKind"/> value indicating the kind of operation.
    /// </param>
    /// <param name="Path">
    ///     The path of the operation's target token.
    /// </param>
    /// <param name="Value">
    ///     The operation's associated value (semantics dependent on <see cref="Kind"/>).
    /// </param>
    public record class JsonPatchOperation(
        [JsonProperty("op")] JsonPatchOperationKind Kind,
        [JsonProperty("path")] string Path,
        [JsonProperty("value")] JToken Value
    )
    {
        /// <summary>
        ///     Is the <see cref="JsonPatchOperation"/> valid?
        /// </summary>
        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                if (Kind == JsonPatchOperationKind.Unknown || !Enum.IsDefined(Kind))
                    return false;

                if (String.IsNullOrWhiteSpace(Path))
                    return false;

                if (Value == null && Kind != JsonPatchOperationKind.Delete)
                    return false;

                return true;
            }
        }

        /// <summary>
        ///     Ensure that the <see cref="JsonPatchOperation"/> is valid.
        /// </summary>
        /// <exception cref="JsonException">
        ///     The <see cref="JsonPatchOperation"/> is not valid.
        /// </exception>
        public void EnsureValid()
        {
            if (Kind == JsonPatchOperationKind.Unknown || !Enum.IsDefined(Kind))
                throw new JsonException($"Invalid JSON-patch operation (invalid 'operation' kind).");

            if (String.IsNullOrWhiteSpace(Path))
                throw new JsonException($"Invalid JSON-patch operation (missing 'path' directive).");

            if (Value == null && Kind != JsonPatchOperationKind.Delete)
                throw new JsonException($"Invalid JSON-patch operation (missing 'value' directive but operation is '{Kind}', not 'delete').");
        }
    }

    /// <summary>
    ///     Well-known kinds of JSON Patch operation.
    /// </summary>
    public enum JsonPatchOperationKind
    {
        /// <summary>
        ///     An unknown kind of JSON Patch operation.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     A deletion operation.
        /// </summary>
        [EnumMember(Value = "delete")]
        Delete = 5,

        /// <summary>
        ///     A replacement operation.
        /// </summary>
        [EnumMember(Value = "replace")]
        Replace = 10,
    }
}
