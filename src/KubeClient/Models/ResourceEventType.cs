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
        ///     Event represents a watch bookmark.
        /// </summary>
        /// <seealso href="https://github.com/kubernetes/enhancements/blob/f61581f3cf591f944e37568b715ff0b2ba357df6/keps/sig-api-machinery/20190206-watch-bookmark.md"/>
        [EnumMember(Value = "BOOKMARK")]
        Bookmark,

        /// <summary>
        ///     Resource encountered an error condition.
        /// </summary>
        [EnumMember(Value = "ERROR")]
        Error
    }
}
