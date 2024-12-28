namespace KubeClient.Http
{
    /// <summary>
    ///     Represents an HTTP error response whose properties can be used to populate an <see cref="HttpRequestException{TResponse}"/>.
    /// </summary>
    public interface IHttpErrorResponse
    {
        /// <summary>
        ///     Get the exception message associated with the response.
        /// </summary>
        string GetExceptionMesage();
    }
}