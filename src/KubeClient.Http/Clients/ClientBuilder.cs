using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;

namespace KubeClient.Http.Clients
{
    using Utilities;

    /// <summary>
    ///		Builds <see cref="HttpClient"/>s with pipelines of <see cref="DelegatingHandler">HTTP message handler</see>s.
    /// </summary>
    /// <remarks>
    ///		Be aware that, if you return singleton instances of message handlers from factory delegates, those handlers will be disposed if the factory encounters any exception while creating a client.
    /// </remarks>
    public sealed class ClientBuilder
	{
		/// <summary>
		///		The default factory for message-pipeline terminus handlers.
		/// </summary>
		static readonly Func<HttpMessageHandler, HttpMessageHandler> DefaultMessagePipelineTerminus = existingHandler => new HttpClientHandler();

		/// <summary>
		///		The default list of configurator delegates for message-pipeline terminus handlers.
		/// </summary>
		static readonly ImmutableList<Func<HttpMessageHandler, HttpMessageHandler>> DefaultPipelineTerminusConfigurators = ImmutableList.Create(DefaultMessagePipelineTerminus);

		/// <summary>
		///		Factory delegates used to produce the HTTP message handlers that comprise client pipelines.
		/// </summary>
		ImmutableList<Func<DelegatingHandler>> _handlerFactories = ImmutableList<Func<DelegatingHandler>>.Empty;

		/// <summary>
        ///		 Delegates to create or modify the <see cref="HttpMessageHandler"/> that will form the message pipeline terminus.
        /// </summary>
		/// <remarks>
		/// 	Each delegate is passed the result of the previous delegate (if any).
		/// 
        /// 	Can be overridden by the value passed to CreateClient.
        /// </remarks>
		ImmutableList<Func<HttpMessageHandler, HttpMessageHandler>> _pipelineTerminusConfigurators = DefaultPipelineTerminusConfigurators;

		/// <summary>
		///		Create a new HTTP client builder.
		/// </summary>
		public ClientBuilder()
		{
		}

		/// <summary>
		///		Create a new HTTP client builder.
		/// </summary>
		/// <param name="copyFrom">
		/// 	The HTTP client buider to copy configuration from.
		/// </param>
		ClientBuilder(ClientBuilder copyFrom)
		{
			if (copyFrom == null)
				throw new ArgumentNullException(nameof(copyFrom));

			_handlerFactories = copyFrom._handlerFactories;
			_pipelineTerminusConfigurators = copyFrom._pipelineTerminusConfigurators;
		}

		/// <summary>
		///		Does the <see cref="ClientBuilder"/> specify a custom handler for the terminus of the message-hander pipeline.
		/// </summary>
		public bool HasCustomPipelineTerminus => _pipelineTerminusConfigurators != DefaultPipelineTerminusConfigurators;

		/// <summary>
		///		Create an <see cref="HttpClient"/> using the configured message-handler pipeline.
		/// </summary>
		/// <param name="baseUri">
		///		An optional base URI for the <see cref="HttpClient"/>.
		/// </param>
		/// <param name="messagePipelineTerminus">
		///		An optional <see cref="HttpMessageHandler"/> that will form the message pipeline terminus.
		/// 
		/// 	If not specified, the pre-configured message pipeline terminus is used.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpClient"/>.
		/// </returns>
		public HttpClient CreateClient(Uri baseUri = null, HttpMessageHandler messagePipelineTerminus = null)
		{
			HttpMessageHandler pipelineTerminus = null;
			IReadOnlyList<DelegatingHandler> pipelineHandlers = null;
			HttpMessageHandler pipeline = null;
			HttpClient client = null;

			try
			{
				pipelineTerminus = messagePipelineTerminus ?? BuildPipelineTerminus();
				pipelineHandlers = CreatePipelineHandlers();
				
				pipeline = CreatePipeline(pipelineTerminus, pipelineHandlers);

				client = new HttpClient(pipeline);
				if (baseUri != null)
					client.BaseAddress = baseUri;
			}
			catch
			{
				using (pipelineTerminus)
				using (pipelineHandlers?.ToAggregateDisposable())
				using (pipeline)
				using (client)
				{
					throw;
				}
			}

			return client;
		}

		/// <summary>
		/// 	Build / configure an HTTP message handler to act as the message pipeline terminus.
		/// </summary>
		/// <param name="initialPipelineTerminus">
		///		The initial <see cref="HttpMessageHandler"/> to use as the pipeline terminus (this is optional, and may ignored by the <see cref="ClientBuilder"/>'s configuration).
		/// </param>
		/// <returns>
		/// 	The configured <see cref="HttpMessageHandler"/>.
		/// </returns>
		public HttpMessageHandler BuildPipelineTerminus(HttpMessageHandler initialPipelineTerminus = null)
		{
			HttpMessageHandler pipelineTerminus = initialPipelineTerminus;

			foreach (Func<HttpMessageHandler, HttpMessageHandler> terminusConfiguration in _pipelineTerminusConfigurators)
				pipelineTerminus = terminusConfiguration(pipelineTerminus);

			if (pipelineTerminus == null)
				throw new InvalidOperationException("One or more configuration delegates for the message pipeline terminus returned null.");

			return pipelineTerminus;
		}

		/// <summary>
		///		Create non-terminal message pipeline handlers (if any).
		/// </summary>
		/// <returns>
		///		A list of message handlers.
		/// </returns>
		/// <remarks>
		///		The returned handlers are not chained together via <see cref="DelegatingHandler.InnerHandler"/> (this is done by <see cref="ClientBuilder.CreatePipeline(HttpMessageHandler, IEnumerable{DelegatingHandler})"/>) and the list does not include the pipeline terminus.
		/// </remarks>
		public List<DelegatingHandler> CreatePipelineHandlers()
		{
			List<DelegatingHandler> pipelineHandlers = new List<DelegatingHandler>();

			try
			{
				foreach (Func<DelegatingHandler> handlerFactory in _handlerFactories)
				{
					DelegatingHandler currentHandler = null;
					try
					{
						currentHandler = handlerFactory();
					}
					catch
					{
						using (currentHandler)
							throw;
					}
					pipelineHandlers.Add(currentHandler);
				}
			}
			catch
			{
				using (pipelineHandlers.ToAggregateDisposable())
				{
					throw;
				}
			}

			return pipelineHandlers;
		}

		/// <summary>
        ///		Create a copy of the <see cref="ClientBuilder"/>, but with the specified configuration for its message pipeline terminus.
        /// </summary>
        /// <param name="pipelineTerminusConfigurator">
		/// 	A delegate that creates the <see cref="HttpMessageHandler"/> for each <see cref="HttpClient"/> that will form its message pipeline terminus.
		/// 
		/// 	If <c>null</c>, the default message handler pipeline terminus will be used.
		/// </param>
        /// <returns>
		/// 	The configured <see cref="ClientBuilder"/>.
		/// </returns>
		public ClientBuilder WithMessagePipelineTerminus(Func<HttpMessageHandler, HttpMessageHandler> pipelineTerminusConfigurator)
		{
			return new ClientBuilder(this)
			{
				_pipelineTerminusConfigurators = _pipelineTerminusConfigurators.Add(
					pipelineTerminusConfigurator ?? DefaultMessagePipelineTerminus
				)
			};
		}

		/// <summary>
        ///		Create a copy of the <see cref="ClientBuilder"/>, but with the specified message pipeline terminus.
        /// </summary>
        /// <param name="pipelineTerminusFactory">
		/// 	A delegate that creates the <see cref="HttpMessageHandler"/> for each <see cref="HttpClient"/> that will form its message pipeline terminus.
		/// 
		/// 	If <c>null</c>, the default message handler pipeline terminus will be used.
		/// </param>
        /// <returns>
		/// 	The configured <see cref="ClientBuilder"/>.
		/// </returns>
		public ClientBuilder WithMessagePipelineTerminus(Func<HttpMessageHandler> pipelineTerminusFactory)
		{
			Func<HttpMessageHandler, HttpMessageHandler> configurator = DefaultMessagePipelineTerminus;
			if (pipelineTerminusFactory != null)
				configurator = _ => pipelineTerminusFactory();

			return new ClientBuilder(this)
			{
				_pipelineTerminusConfigurators = ImmutableList.Create(configurator) // Replaces any existing configurators.
			};
		}

		/// <summary>
		///		Create a copy of the <see cref="ClientBuilder"/>, but with the default message pipeline terminus.
		/// </summary>
		/// <returns>
		/// 	The configured <see cref="ClientBuilder"/>.
		/// </returns>
		public ClientBuilder WithDefaultMessagePipelineTerminus()
		{
			return new ClientBuilder(this)
			{
				_pipelineTerminusConfigurators = DefaultPipelineTerminusConfigurators
			};
		}

		/// <summary>
		///		Create a copy of the <see cref="ClientBuilder"/>, adding an HTTP message-handler factory to the end of the pipeline.
		/// </summary>
		/// <typeparam name="THandler">
		///		The handler type.
		/// </typeparam>
		/// <param name="handlerFactory">
		///		The message-handler factory.
		/// </param>
		/// <returns>
		///		The <see cref="ClientBuilder"/> (enables method-chaining).
		/// </returns>
		/// <remarks>
		///		<typeparamref name="THandler"/> cannot be the <see cref="DelegatingHandler"/> base class.
		/// </remarks>
		public ClientBuilder AddHandler<THandler>(Func<THandler> handlerFactory)
			where THandler : DelegatingHandler
		{
			if (handlerFactory == null)
				throw new ArgumentNullException(nameof(handlerFactory));

			if (typeof(THandler) == typeof(DelegatingHandler))
				throw new InvalidOperationException("Handler type cannot be the DelegatingHandler base class.");

			if (_handlerFactories.OfType<Func<THandler>>().Any())
			{
				throw new InvalidOperationException(
					String.Format(
						"The configured handler pipeline already contains a factory for message-handlers of type '{0}'.",
						typeof(THandler).AssemblyQualifiedName
					)
				);
			}

			return new ClientBuilder(this)
			{
				_handlerFactories = _handlerFactories.Add(handlerFactory)
			};
		}

		/// <summary>
		///		Create a copy of the <see cref="ClientBuilder"/>, inserting an HTTP message-handler factory to the pipeline before the factory that produces handlers of the specified type.
		/// </summary>
		/// <typeparam name="THandler">
		///		The handler type.
		/// </typeparam>
		/// <typeparam name="TBeforeHandler">
		///		The type of handler before whose factory the new handler factory should be inserted.
		/// </typeparam>
		/// <param name="handlerFactory">
		///		The message-handler factory.
		/// </param>
		/// <param name="throwIfNotPresent">
		///		Throw an <see cref="InvalidOperationException"/> if no factory for <typeparamref name="TBeforeHandler"/> is present?
		/// 
		///		Default is <c>false</c>.
		/// </param>
		/// <returns>
		///		The <see cref="ClientBuilder"/> (enables method-chaining).
		/// </returns>
		/// <remarks>
		///		<typeparamref name="THandler"/> and <typeparamref name="TBeforeHandler"/> cannot be the <see cref="DelegatingHandler"/> base class.
		/// </remarks>
		public ClientBuilder AddHandlerBefore<THandler, TBeforeHandler>(Func<THandler> handlerFactory, bool throwIfNotPresent = false)
			where THandler : DelegatingHandler
			where TBeforeHandler : DelegatingHandler
		{
			if (handlerFactory == null)
				throw new ArgumentNullException(nameof(handlerFactory));

			if (typeof(THandler) == typeof(DelegatingHandler))
				throw new InvalidOperationException("Handler type cannot be the DelegatingHandler base class.");

			if (typeof(THandler) == typeof(DelegatingHandler))
				throw new InvalidOperationException("Handler type cannot be the DelegatingHandler base class.");

			if (_handlerFactories.OfType<Func<THandler>>().Any())
			{
				throw new InvalidOperationException(
					String.Format(
						"The configured handler pipeline already contains a factory for message-handlers of type '{0}'.",
						typeof(THandler).AssemblyQualifiedName
					)
				);
			}

			Type beforeHandlerFactoryType = typeof(Func<TBeforeHandler>);
			for (int handlerIndex = 0; handlerIndex < _handlerFactories.Count; handlerIndex++)
			{
				if (_handlerFactories[handlerIndex].GetType() == beforeHandlerFactoryType)
				{
					return new ClientBuilder(this)
					{
						_handlerFactories = _handlerFactories.Insert(handlerIndex, handlerFactory)
					};
				}
			}

			if (throwIfNotPresent)
			{
				throw new InvalidOperationException(
					String.Format(
						"Cannot insert factory for message-handlers of type '{0}' before the factory for message-handlers of type '{1}' (the pipeline does not contain a factory for message-handlers of this type.",
						typeof(THandler).AssemblyQualifiedName,
						typeof(TBeforeHandler).AssemblyQualifiedName
					)
				);
			}

			// TBefore is not present, so just append to the end of the pipeline.
			return new ClientBuilder(this)
			{
				_handlerFactories = _handlerFactories.Add(handlerFactory)
			};
		}

		/// <summary>
		///		Create a copy of the <see cref="ClientBuilder"/>, inserting an HTTP message-handler factory to the pipeline after the factory that produces handlers of the specified type.
		/// </summary>
		/// <typeparam name="THandler">
		///		The handler type.
		/// </typeparam>
		/// <typeparam name="TAfterHandler">
		///		The type of handler after whose factory the new handler factory should be inserted.
		/// </typeparam>
		/// <param name="handlerFactory">
		///		The message-handler factory.
		/// </param>
		/// <param name="throwIfNotPresent">
		///		Throw an <see cref="InvalidOperationException"/> if no factory for <typeparamref name="TAfterHandler"/> is present?
		/// 
		///		Default is <c>false</c>.
		/// </param>
		/// <returns>
		///		The <see cref="ClientBuilder"/> (enables method-chaining).
		/// </returns>
		/// <remarks>
		///		<typeparamref name="THandler"/> and <typeparamref name="TAfterHandler"/> cannot be the <see cref="DelegatingHandler"/> base class.
		/// </remarks>
		public ClientBuilder AddHandlerAfter<THandler, TAfterHandler>(Func<THandler> handlerFactory, bool throwIfNotPresent = false)
			where THandler : DelegatingHandler
			where TAfterHandler : DelegatingHandler
		{
			if (handlerFactory == null)
				throw new ArgumentNullException(nameof(handlerFactory));

			if (typeof(THandler) == typeof(DelegatingHandler))
				throw new InvalidOperationException("Handler type cannot be the DelegatingHandler base class.");

			if (typeof(THandler) == typeof(DelegatingHandler))
				throw new InvalidOperationException("Handler type cannot be the DelegatingHandler base class.");

			if (_handlerFactories.OfType<Func<THandler>>().Any())
			{
				throw new InvalidOperationException(
					String.Format(
						"The configured handler pipeline already contains a factory for message-handlers of type '{0}'.",
						typeof(THandler).AssemblyQualifiedName
					)
				);
			}

			Type afterHandlerFactoryType = typeof(Func<TAfterHandler>);
			for (int handlerIndex = 0; handlerIndex < _handlerFactories.Count; handlerIndex++)
			{
				if (_handlerFactories[handlerIndex].GetType() == afterHandlerFactoryType)
				{
					return new ClientBuilder(this)
					{
						_handlerFactories = _handlerFactories.Insert(handlerIndex + 1, handlerFactory)
					};
				}
			}

			if (throwIfNotPresent)
			{
				throw new InvalidOperationException(
					String.Format(
						"Cannot insert factory for message-handlers of type '{0}' after the factory for message-handlers of type '{1}' (the pipeline does not contain a factory for message-handlers of this type.",
						typeof(THandler).AssemblyQualifiedName,
						typeof(TAfterHandler).AssemblyQualifiedName
					)
				);
			}

			// TAfter is not present, so just append to the end of the pipeline.
			return new ClientBuilder(this)
			{
				_handlerFactories = _handlerFactories.Add(handlerFactory)
			};
		}

		/// <summary>
		///		Enumerate the types of handlers configured in the factory's pipeline.
		/// </summary>
		/// <returns>
		///		A sequence of 0 or more types.
		/// </returns>
		/// <remarks>
		///		This operation uses Reflection, so it can be relatively expensive; use sparingly.
		/// </remarks>
		public IEnumerable<Type> EnumerateHandlerTypes()
		{
			for (int handlerIndex = 0; handlerIndex < _handlerFactories.Count; handlerIndex++)
			{
				Func<DelegatingHandler> factory = _handlerFactories[handlerIndex];
				Type factoryDelegateType = factory.GetType();

				yield return factoryDelegateType.GenericTypeArguments[0];
			}
		}

		/// <summary>
		///		Create an HTTP message-handler pipeline.
		/// </summary>
		/// <param name="pipelineTerminus">
		///		An <see cref="HttpMessageHandler"/> representing the terminus of the pipeline.
		/// </param>
		/// <param name="pipelineHandlers">
		///		A sequence of <see cref="DelegatingHandler"/>s representing additional steps in the pipeline.
		/// </param>
		/// <returns>
		///		An <see cref="HttpMessageHandler"/> representing the head of the pipeline.
		/// </returns>
		public static HttpMessageHandler CreatePipeline(HttpMessageHandler pipelineTerminus, IEnumerable<DelegatingHandler> pipelineHandlers)
		{
			if (pipelineTerminus == null)
				throw new ArgumentNullException(nameof(pipelineTerminus));

			if (pipelineHandlers == null)
				throw new ArgumentNullException(nameof(pipelineHandlers));

			HttpMessageHandler pipeline = pipelineTerminus;
			foreach (DelegatingHandler pipelineHandler in pipelineHandlers.Reverse())
			{
				pipelineHandler.InnerHandler = pipeline;
				pipeline = pipelineHandler;
			}

			return pipeline;
		}
	}
}
