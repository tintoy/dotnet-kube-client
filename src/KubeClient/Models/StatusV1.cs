using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Status is a return value for calls that don't return other objects.
    /// </summary>
    public partial class StatusV1
    {
        /// <summary>
        ///     The <see cref="Status"/> value representing success.
        /// </summary>
        public const string SuccessStatus = "Success";

        /// <summary>
        ///     The <see cref="Status"/> value representing failure.
        /// </summary>
        public const string FailureStatus = "Failure";

        /// <summary>
        ///     Does the <see cref="StatusV1"/> represent success?
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public bool IsSuccess => Status == SuccessStatus;

        /// <summary>
        ///     Does the <see cref="StatusV1"/> represent failure?
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public bool IsFailure => Status == FailureStatus;

        /// <summary>
        ///     Enumerate the list's items.
        /// </summary>
        /// <returns>
        ///     The list's items.
        /// </returns>
        public override IEnumerable<KubeResourceV1> EnumerateItems()
        {
            yield break; // StatusV1 is not really a Kubernetes resource list.
        }

        /// <summary>
        ///     Create a new <see cref="StatusV1"/> representing success.
        /// </summary>
        /// <param name="message">
        ///     The status message.
        /// </param>
        /// <returns>
        ///     The configured <see cref="StatusV1"/>.
        /// </returns>
        public static StatusV1 Success(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'message'.", nameof(message));
            
            return new StatusV1
            {
                Status = SuccessStatus,
                Message = message
            };
        }

        /// <summary>
        ///     Create a new <see cref="StatusV1"/> representing failure.
        /// </summary>
        /// <param name="message">
        ///     The status message.
        /// </param>
        /// <param name="reason">
        ///     An optional machine-parseable reason code indicating the cause of the failure.
        /// </param>
        /// <param name="code">
        ///     An optional HTTP status code representing the cause of the failure.
        /// </param>
        /// <returns>
        ///     The configured <see cref="StatusV1"/>.
        /// </returns>
        public static StatusV1 Failure(string message, string reason = null, int? code = null)
        {
            if (String.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'message'.", nameof(message));
            
            return new StatusV1
            {
                Status = FailureStatus,
                Message = message,
                Reason = reason,
                Code = code
            };
        }
    }
}
