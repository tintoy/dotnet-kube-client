using KubeClient.Models;
using Microsoft.AspNetCore.Http;

namespace KubeClient.TestCommon.Mocks
{
    /// <summary>
    ///     Factory methods for well-known API results returned from minimal APIs.
    /// </summary>
    public static class MockApiResults
    {
        /// <summary>
        ///     Create an HTTP 200 result that serialises the specified value as JSON using the Newtonsoft serialiser and sends it as the response body.
        /// </summary>
        /// <typeparam name="TValue">
        ///     The type of value to use as the response body.
        /// </typeparam>
        /// <param name="value">
        ///     The value to use as the response body.
        /// </param>
        /// <returns>
        ///     The <see cref="IResult"/>.
        /// </returns>
        public static IResult Ok<TValue>(TValue value) => new NewtonsoftJsonHttpResult<TValue>(value, StatusCodes.Status200OK);

        /// <summary>
        ///     Create an HTTP 400 result that serialises the specified status as JSON using the Newtonsoft serialiser and sends it as the response body.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> to use as the response body.
        /// </param>
        /// <returns>
        ///     The <see cref="IResult"/>.
        /// </returns>
        public static IResult BadRequest(StatusV1 status) => new NewtonsoftJsonHttpResult<StatusV1>(status, StatusCodes.Status400BadRequest);

        /// <summary>
        ///     Create an HTTP 404 result that serialises the specified status as JSON using the Newtonsoft serialiser and sends it as the response body.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> to use as the response body.
        /// </param>
        /// <returns>
        ///     The <see cref="IResult"/>.
        /// </returns>
        public static IResult NotFound(StatusV1 status) => new NewtonsoftJsonHttpResult<StatusV1>(status, StatusCodes.Status404NotFound);

        /// <summary>
        ///     Create an HTTP 500 result that serialises the specified status as JSON using the Newtonsoft serialiser and sends it as the response body.
        /// </summary>
        /// <param name="status">
        ///     The <see cref="StatusV1"/> to use as the response body.
        /// </param>
        /// <returns>
        ///     The <see cref="IResult"/>.
        /// </returns>
        public static IResult InternalServerError(StatusV1 status) => new NewtonsoftJsonHttpResult<StatusV1>(status, StatusCodes.Status500InternalServerError);
    }
}
