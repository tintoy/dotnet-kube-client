using System;
using System.Linq;

namespace KubeClient.Http
{
    /// <summary>
    ///		<see cref="HttpRequest{TContext}"/> / <see cref="IHttpRequest{TContext}"/> extension methods for request-configuration actions.
    /// </summary>
    public static partial class TypedRequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request with the specified request-configuration action.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestAction">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithRequestAction<TContext>(this HttpRequest<TContext> request, RequestAction requestAction)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestAction == null)
                throw new ArgumentNullException(nameof(requestAction));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest<TContext>.RequestActions)] = request.RequestActions.Add(
                    (message, context) => requestAction(message)
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request-configuration action.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestAction">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithRequestAction<TContext>(this HttpRequest<TContext> request, RequestAction<TContext> requestAction)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestAction == null)
                throw new ArgumentNullException(nameof(requestAction));

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest<TContext>.RequestActions)] = request.RequestActions.Add(requestAction);
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request-configuration actions.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestActions">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithRequestAction<TContext>(this HttpRequest<TContext> request, params RequestAction[] requestActions)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestActions == null)
                throw new ArgumentNullException(nameof(requestActions));

            if (requestActions.Length == 0)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest<TContext>.RequestActions)] = request.RequestActions.AddRange(
                    requestActions.Select(requestAction =>
                    {
                        RequestAction<TContext> requestActionWithContext = (message, context) => requestAction(message);

                        return requestActionWithContext;
                    })
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request-configuration actions.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="requestActions">
        ///		A delegate that configures outgoing request messages.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithRequestAction<TContext>(this HttpRequest<TContext> request, params RequestAction<TContext>[] requestActions)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestActions == null)
                throw new ArgumentNullException(nameof(requestActions));

            if (requestActions.Length == 0)
                return request;

            return request.Clone(properties =>
            {
                properties[nameof(HttpRequest<TContext>.RequestActions)] = request.RequestActions.AddRange(requestActions);
            });
        }
    }
}
