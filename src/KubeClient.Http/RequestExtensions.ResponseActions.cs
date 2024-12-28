using System;
using System.Linq;

namespace KubeClient.Http
{
    /// <summary>
    ///		<see cref="HttpRequest"/> / <see cref="IHttpRequest"/> extension methods for response-processing actions.
    /// </summary>
    public static partial class RequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request with the specified response-processing action.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseAction">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithResponseAction(this HttpRequest request, ResponseAction responseAction)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (responseAction == null)
                throw new ArgumentNullException(nameof(responseAction));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.ResponseActions)] = request.ResponseActions.Add(
                    (message, context) => responseAction(message)
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified response-processing action.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseAction">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithResponseAction(this HttpRequest request, ResponseAction<object> responseAction)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (responseAction == null)
                throw new ArgumentNullException(nameof(responseAction));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.ResponseActions)] = request.ResponseActions.Add(responseAction);
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified response-processing actions.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseActions">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithResponseAction(this HttpRequest request, params ResponseAction[] responseActions)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (responseActions == null)
                throw new ArgumentNullException(nameof(responseActions));

            if (responseActions.Length == 0)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.ResponseActions)] = request.ResponseActions.AddRange(
                    responseActions.Select(responseAction =>
                    {
                        ResponseAction<object> responseActionWithContext = (message, context) => responseAction(message);

                        return responseActionWithContext;
                    })
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified response-processing actions.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseActions">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithResponseAction(this HttpRequest request, params ResponseAction<object>[] responseActions)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (responseActions == null)
                throw new ArgumentNullException(nameof(responseActions));

            if (responseActions.Length == 0)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.ResponseActions)] = request.ResponseActions.AddRange(responseActions);
            });
        }
    }
}
