using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Http.Diagnostics.MessageHandlers
{
    /// <summary>
    ///		Client-side HTTP message handler that logs outgoing requests and incoming responses.
    /// </summary>
    public class LoggingMessageHandler
        : DelegatingHandler
    {
        /// <summary>
        ///		Create a new <see cref="LoggingMessageHandler"/>.
        /// </summary>
        /// <param name="logger">
        ///		The <see cref="ILogger"/> used to log messages about requests and responses.
        /// </param>
        /// <param name="requestComponents">
        /// 	A <see cref="LogMessageComponents"/> value indicating which components of each request message should be logged.
        /// </param>
        /// <param name="responseComponents">
        /// 	A <see cref="LogMessageComponents"/> value indicating which components of each response message should be logged.
        /// </param>
        public LoggingMessageHandler(ILogger logger, LogMessageComponents requestComponents = LogMessageComponents.Basic, LogMessageComponents responseComponents = LogMessageComponents.Basic)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            Log = logger;
            RequestComponents = requestComponents;
            ResponseComponents = responseComponents;
        }

        /// <summary>
        ///		The <see cref="ILogger"/> used to log messages about requests and responses.
        /// </summary>
        protected ILogger Log { get; }

        /// <summary>
        /// 	A <see cref="LogMessageComponents"/> value indicating which components of each request message should be logged.
        /// </summary>
        public LogMessageComponents RequestComponents { get; }

        /// <summary>
        /// 	A <see cref="LogMessageComponents"/> value indicating which components of each response message should be logged.
        /// </summary>
        public LogMessageComponents ResponseComponents { get; }

        /// <summary>
        ///		Asynchronously process an outgoing HTTP request message and its incoming response message.
        /// </summary>
        /// <param name="request">
        ///		The <see cref="HttpRequestMessage"/> representing the outgoing request.
        /// </param>
        /// <param name="cancellationToken">
        ///		A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        /// </param>
        /// <returns>
        ///		Create a new <see cref="LoggingMessageHandler"/>.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (ShouldLogRequest(LogMessageComponents.Basic))
                Log.BeginRequest(request);

            try
            {
                if (ShouldLogRequest(LogMessageComponents.Body) && request.Content != null)
                    await Log.RequestBody(request);

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                if (ShouldLogResponse(LogMessageComponents.Body) && response.Content != null)
                {
                    if (!response.RequestMessage.IsStreamed())
                        await Log.ResponseBody(response);
                    else
                        Log.StreamedResponse(response);
                }

                if (ShouldLogRequest(LogMessageComponents.Basic))
                    Log.EndRequest(request, response.StatusCode);

                return response;
            }
            catch (Exception eRequest)
            {
                // Errors are always logged.
                Log.RequestError(request, eRequest);

                throw;
            }
        }

        /// <summary>
        /// 	Determine whether the specified component of request messages should be logged.
        /// </summary>
        /// <param name="requestComponent">
        /// 	A <see cref="LogMessageComponents"/> value representing the message component.
        /// </param>
        /// <returns>
        /// 	<c>true</c>, if the message component should be logged; otherwise, <c>false</c>.
        /// </returns>
        protected bool ShouldLogRequest(LogMessageComponents requestComponent) => (RequestComponents & requestComponent) != 0;

        /// <summary>
        /// 	Determine whether the specified component of response messages should be logged.
        /// </summary>
        /// <param name="responseComponent">
        /// 	A <see cref="LogMessageComponents"/> value representing the message component.
        /// </param>
        /// <returns>
        /// 	<c>true</c>, if the message component should be logged; otherwise, <c>false</c>.
        /// </returns>
        protected bool ShouldLogResponse(LogMessageComponents responseComponent) => (ResponseComponents & responseComponent) != 0;
    }
}
