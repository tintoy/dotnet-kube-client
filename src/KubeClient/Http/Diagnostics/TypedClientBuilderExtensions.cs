using Microsoft.Extensions.Logging;
using System;

namespace KubeClient.Http.Diagnostics
{
    using Clients;
	using MessageHandlers;
	
	/// <summary>
	/// 	Extension methods for the <see cref="ClientBuilder{TContext}">HTTP client builder</see>.
	/// </summary>
	public static class TypedClientBuilderExtensions
	{
		/// <summary>
		/// 	Create a copy of the HTTP client builder whose clients will log requests and responses to the specified logger.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the client.
		/// </typeparam>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <param name="logger">
		///		The logger used to log the event.
		/// </param>
		/// <param name="requestComponents">
		/// 	A <see cref="LogMessageComponents"/> value indicating which components of each request message should be logged.
		/// </param>
		/// <param name="responseComponents">
		/// 	A <see cref="LogMessageComponents"/> value indicating which components of each response message should be logged.
		/// </param>
		/// <returns>
		///		The new <see cref="ClientBuilder{TContext}"/>.
		/// </returns>
		/// <remarks>
		///		This overload is for convenience only; for the purposes of reliability you should resolve the logger when you are creating the client (it's not good practice to share the same instance of a logger between multiple clients).
		/// </remarks>
		public static ClientBuilder<TContext> WithLogging<TContext>(this ClientBuilder<TContext> clientBuilder, ILogger logger, LogMessageComponents requestComponents = LogMessageComponents.Basic, LogMessageComponents responseComponents = LogMessageComponents.Basic)
		{
			return clientBuilder.WithLogging(context => logger, requestComponents, responseComponents);
		}

		/// <summary>
		/// 	Create a copy of the HTTP client builder whose clients will log requests and responses to the specified logger.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the client.
		/// </typeparam>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <param name="loggerFactory">
		///		A delegate that produces the logger for each client.
		/// </param>
		/// <param name="requestComponents">
		/// 	A <see cref="LogMessageComponents"/> value indicating which components of each request message should be logged.
		/// </param>
		/// <param name="responseComponents">
		/// 	A <see cref="LogMessageComponents"/> value indicating which components of each response message should be logged.
		/// </param>
		/// <returns>
		///		The new <see cref="ClientBuilder{TContext}"/>.
		/// </returns>
		/// <remarks>
		///		Each call to <paramref name="loggerFactory"/> should return a new instance of the logger (it's not good practice to share the same instance of a logger between multiple clients).
		/// </remarks>
		public static ClientBuilder<TContext> WithLogging<TContext>(this ClientBuilder<TContext> clientBuilder, Func<TContext, ILogger> loggerFactory, LogMessageComponents requestComponents = LogMessageComponents.Basic, LogMessageComponents responseComponents = LogMessageComponents.Basic)
		{
			return clientBuilder.AddHandler(context =>
			{
				ILogger logger = loggerFactory(context);
				
				return new LoggingMessageHandler(logger, requestComponents, responseComponents);
			});
		}
	}
}
