using System;
using System.Linq;

namespace KubeClient.Http
{
    /// <summary>
    ///		<see cref="HttpRequest"/> / <see cref="IHttpRequest"/> extension methods for request-configuration actions.
    /// </summary>
    public static partial class RequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request with the specified request-configuration action.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestAction">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithRequestAction(this HttpRequest request, RequestAction requestAction)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestAction == null)
                throw new ArgumentNullException(nameof(requestAction));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.RequestActions)] = request.RequestActions.Add(
                    (message, context) => requestAction(message)
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request-configuration action.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestAction">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithRequestAction(this HttpRequest request, RequestAction<object> requestAction)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestAction == null)
                throw new ArgumentNullException(nameof(requestAction));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.RequestActions)] = request.RequestActions.Add(requestAction);
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request-configuration actions.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestActions">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithRequestAction(this HttpRequest request, params RequestAction[] requestActions)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestActions == null)
                throw new ArgumentNullException(nameof(requestActions));

            if (requestActions.Length == 0)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.RequestActions)] = request.RequestActions.AddRange(
                    requestActions.Select(requestAction =>
                    {
                        RequestAction<object> requestActionWithContext = (message, context) => requestAction(message);

                        return requestActionWithContext;
                    })
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request-configuration actions.
        /// </summary>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestActions">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest WithRequestAction(this HttpRequest request, params RequestAction<object>[] requestActions)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestActions == null)
                throw new ArgumentNullException(nameof(requestActions));

            if (requestActions.Length == 0)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest.RequestActions)] = request.RequestActions.AddRange(requestActions);
            });
        }
    }
}
