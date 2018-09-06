namespace KubeClient.ApiMetadata
{
    /// <summary>
    ///     Well-known verbs supported by Kubernetes resource APIs.
    /// </summary>
    public static class KubeApiVerb
    {
        /// <summary>
        ///     Create a resource.
        /// </summary>
        public const string Create = "create";

        /// <summary>
        ///     Delete a resource.
        /// </summary>
        public const string Delete = "delete";

        /// <summary>
        ///     Delete a collection of resources.
        /// </summary>
        public const string DeleteCollection = "deletecollection";

        /// <summary>
        ///     Retrieve a single resource.
        /// </summary>
        public const string Get = "get";

        /// <summary>
        ///     List resources.
        /// </summary>
        public const string List = "list";

        /// <summary>
        ///     Patch (selectively update) a resource.
        /// </summary>
        public const string Patch = "patch";

        /// <summary>
        ///     Update (replace) a resource.
        /// </summary>
        public const string Update = "update";

        /// <summary>
        ///     Watch a resource or resource collection for changes.
        /// </summary>
        public const string Watch = "watch";
    }
}
