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
        ///     Enumerate the list's items.
        /// </summary>
        /// <returns>
        ///     The list's items.
        /// </returns>
        public override IEnumerable<KubeResourceV1> EnumerateItems()
        {
            yield break; // StatusV1 is not really a Kubernetes resource list.
        }
    }
}
