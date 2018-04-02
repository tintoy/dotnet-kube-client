using System.Runtime.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Well-known types of Kubernetes resource events.
    /// </summary>
    public enum ResourceEventType
    {
        /// <summary>
        ///     Resource was created.
        /// </summary>
        [EnumMember(Value = "ADDED")]
        Added,

        /// <summary>
        ///     Resource was modified.
        /// </summary>
        [EnumMember(Value = "MODIFIED")]
        Modified,

        /// <summary>
        ///     Resource was deleted.
        /// </summary>
        [EnumMember(Value = "DELETED")]
        Deleted,

        /// <summary>
        ///     Resource encountered an error condition.
        /// </summary>
        [EnumMember(Value = "ERROR")]
        Error
    }
}
