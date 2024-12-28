using System;
using System.Linq;

namespace KubeClient.Http
{
    /// <summary>
    ///		<see cref="HttpRequest{TContext}"/> / <see cref="IHttpRequest{TContext}"/> extension methods for response-processing actions.
    /// </summary>
    public static partial class TypedRequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request with the specified response-processing action.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseAction">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithResponseAction<TContext>(this HttpRequest<TContext> request, ResponseAction responseAction)
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
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseAction">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithResponseAction<TContext>(this HttpRequest<TContext> request, ResponseAction<TContext> responseAction)
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
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseActions">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithResponseAction<TContext>(this HttpRequest<TContext> request, params ResponseAction[] responseActions)
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
                        ResponseAction<TContext> responseActionWithContext = (message, context) => responseAction(message);

                        return responseActionWithContext;
                    })
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified response-processing actions.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used by the request when resolving deferred template parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="responseActions">
        ///		A delegate that configures incoming response messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithResponseAction<TContext>(this HttpRequest<TContext> request, params ResponseAction<TContext>[] responseActions)
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
