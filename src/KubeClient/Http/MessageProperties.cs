namespace KubeClient.Http
{
    // TODO: Switch to HttpRequestOptions once we drop netstandard2.1 support.

    /// <summary>
    ///		The names of well-known HttpRequestMessage / HttpResponseMessage properties.
    /// </summary>
    public static class MessageProperties
    {
        /// <summary>
        ///		The prefix for HTTPlease property names.
        /// </summary>
        static readonly string Prefix = "HTTPlease.";

        /// <summary>
        ///		The <see cref="IHttpRequest"/> that created the message.
        /// </summary>
        public static readonly string Request = Prefix + "Request";

        /// <summary>
        ///		The message's collection of content formatters.
        /// </summary>
        public static readonly string ContentFormatters = Prefix + "ContentFormatters";

        /// <summary>
        /// 	Is the request configured for a streamed response?
        /// </summary>
        public static readonly string IsStreamed = Prefix + "IsStreamed";
    }
}
