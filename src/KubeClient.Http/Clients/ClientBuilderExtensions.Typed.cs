using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace KubeClient.Http.Clients
{
    /// <summary>
    ///		General-purpose extensions for <see cref="ClientBuilder{TContext}"/> and <see cref="ClientBuilder{TContext}"/>.
    /// </summary>
    public static partial class ClientBuilderExtensions
	{
		/// <summary>
		///		Create a new <see cref="HttpClient"/> with the specified credentials.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the client.
		/// </typeparam>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> that contains contextual information used when creating the client.
		/// </param>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the HTTP client.
		/// </param>
		/// <param name="credentials">
		///		The client credentials used for authentication.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpClient"/>.
		/// </returns>
		public static HttpClient CreateClient<TContext>(this ClientBuilder<TContext> clientBuilder, TContext context, Uri baseUri, ICredentials credentials)
		{
			HttpClientHandler clientHandler = null;
			try
			{
				clientHandler = new HttpClientHandler
				{
					Credentials = credentials
				};

				return clientBuilder.CreateClient(context, baseUri, clientHandler);
			}
			catch (Exception)
			{
				using (clientHandler)
				{
					throw;
				}
			}
		}

		/// <summary>
		///		Create a new <see cref="HttpClient"/> with the specified credentials.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the client.
		/// </typeparam>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> that contains contextual information used when creating the client.
		/// </param>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the HTTP client.
		/// </param>
		/// <param name="credentials">
		///		The client credentials used for authentication.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpClient"/>.
		/// </returns>
		public static HttpClient CreateClient<TContext>(this ClientBuilder<TContext> clientBuilder, TContext context, string baseUri, ICredentials credentials)
		{
			HttpClientHandler clientHandler = null;
			try
			{
				clientHandler = new HttpClientHandler
				{
					Credentials = credentials
				};

				return clientBuilder.CreateClient(context, baseUri, clientHandler);
			}
			catch (Exception)
			{
				using (clientHandler)
				{
					throw;
				}
			}
		}

		/// <summary>
		///		Create a new <see cref="HttpClient"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the client.
		/// </typeparam>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> that contains contextual information used when creating the client.
		/// </param>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the HTTP client.
		/// </param>
		/// <param name="messagePipelineTerminus">
		///		An optional <see cref="HttpMessageHandler"/> that will form the message pipeline terminus.
		/// 
		/// 	If not specified, a new <see cref="HttpClientHandler"/> is used.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpClient"/>.
		/// </returns>
		public static HttpClient CreateClient<TContext>(this ClientBuilder<TContext> clientBuilder, TContext context, string baseUri, HttpMessageHandler messagePipelineTerminus = null)
		{
			if (clientBuilder == null)
				throw new ArgumentNullException(nameof(clientBuilder));

			if (String.IsNullOrWhiteSpace(baseUri))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'baseUri'.", nameof(baseUri));

			return clientBuilder.CreateClient(
				context,
				new Uri(baseUri, UriKind.Absolute),
				messagePipelineTerminus
			);
		}

		/// <summary>
		///		Register a message handler type for the pipeline used by clients created by the factory.
		/// </summary>
		/// <typeparam name="TMessageHandler">
		///		The message handler type.
		/// 
		///		Must be a sub-type of <see cref="DelegatingHandler"/> (not <see cref="DelegatingHandler"/> itself).
		/// </typeparam>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the handler.
		/// </typeparam>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <returns>
		///		The client factory (enables inline use / method chaining).
		/// </returns>
		public static ClientBuilder<TContext> WithMessageHandler<TMessageHandler, TContext>(this ClientBuilder<TContext> clientBuilder)
			where TMessageHandler : DelegatingHandler, new()
		{
			if (clientBuilder == null)
				throw new ArgumentNullException(nameof(clientBuilder));

			if (typeof(TMessageHandler) == DelegatingHandlerType)
				throw new NotSupportedException("TMessageHandler must be a sub-type of DelegatingHandler (it cannot be DelegatingHandler).");

			clientBuilder.AddHandler(
				context => new TMessageHandler()
			);

			return clientBuilder;
		}

		/// <summary>
        ///		Create a copy of the <see cref="ClientBuilder{TContext}"/>, but with the specified configuration action for its <see cref="HttpClientHandler"/> (message pipeline terminus).
        /// </summary>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
        /// <param name="clientHandlerConfigurator">
		/// 	A delegate that configures the <see cref="HttpClientHandler"/> that will form the message pipeline terminus for each <see cref="HttpClient"/> produced by the builder.
		/// </param>
        /// <returns>
		/// 	The configured <see cref="ClientBuilder{TContext}"/>.
		/// </returns>
		public static ClientBuilder<TContext> ConfigureHttpClientHandler<TContext>(this ClientBuilder<TContext> clientBuilder, Action<HttpClientHandler, TContext> clientHandlerConfigurator)
		{
			if (clientHandlerConfigurator == null)
				throw new ArgumentNullException(nameof(clientHandlerConfigurator));
			
			return clientBuilder.ConfigureMessagePipelineTerminus(clientHandlerConfigurator);
		}

		/// <summary>
		///		Create a copy of the <see cref="ClientBuilder{TContext}"/>, but with the specified configuration action for its message pipeline terminus.
		/// </summary>
		/// <typeparam name="TMessageHandler">
		/// 	The type of message handler to expect / configure.
		/// </typeparam>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the pipeline terminus.
		/// </typeparam>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <param name="pipelineTerminusConfigurator">
		/// 	A delegate that configures the <typeparamref name="TMessageHandler"/> that will form the message pipeline terminus for each <see cref="HttpClient"/> produced by the builder.
		/// </param>
		/// <returns>
		/// 	The configured <see cref="ClientBuilder{TContext}"/>.
		/// </returns>
		public static ClientBuilder<TContext> ConfigureMessagePipelineTerminus<TMessageHandler, TContext>(this ClientBuilder<TContext> clientBuilder, Action<TMessageHandler, TContext> pipelineTerminusConfigurator)
			where TMessageHandler : HttpMessageHandler
		{
			if (pipelineTerminusConfigurator == null)
				throw new ArgumentNullException(nameof(pipelineTerminusConfigurator));
			
			return clientBuilder.WithMessagePipelineTerminus((existingTerminator, context) =>
			{
				if (existingTerminator == null)
					throw new InvalidOperationException($"Cannot configure pipeline terminus (expected a handler of type '{typeof(TMessageHandler).FullName}', but the previous factory function returned null).");

				if (existingTerminator is TMessageHandler typedHandler)
					pipelineTerminusConfigurator(typedHandler, context);
				else
					throw new InvalidOperationException($"Cannot configure pipeline terminus (expected a handler of type '{typeof(TMessageHandler).FullName}', but the previous factory function returned a handler of type '{existingTerminator.GetType().FullName}').");
				
				return existingTerminator;
			});
		}

		/// <summary>
		///		Create a copy of the <see cref="ClientBuilder{TContext}"/>, but using the specified X.509 certificate for client authentication.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type that contains contextual information used when creating the client.
		/// </typeparam>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <param name="clientCertificate">
		/// 	The X.509 certificate to use.
		/// </param>
		/// <returns>
		/// 	The configured <see cref="ClientBuilder{TContext}"/>.
		/// </returns>
		public static ClientBuilder<TContext> WithClientCertificate<TContext>(this ClientBuilder<TContext> clientBuilder, X509Certificate2 clientCertificate)
		{
			if (clientBuilder == null)
                throw new ArgumentNullException(nameof(clientBuilder));
            
            if (clientCertificate == null)
                throw new ArgumentNullException(nameof(clientCertificate));
            
            if (!clientCertificate.HasPrivateKey)
                throw new InvalidOperationException($"Cannot use certificate '{clientCertificate.Subject}' as a client certificate (no private key is not available for it).");

            return clientBuilder.ConfigureHttpClientHandler((clientHandler, context) =>
			{
				clientHandler.ClientCertificates.Add(clientCertificate);
				clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
			});
		}

		/// <summary>
        ///		Create a copy of the <see cref="ClientBuilder{TContext}"/>, but using the specified X.509 certificate for server authentication.
        /// </summary>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
        /// <param name="expectServerCertificate">
		/// 	The X.509 certificate to expect the server to use.
		/// </param>
		/// <param name="logError">
		/// 	An optional delegate called if an unexpected error is encountered while validating the server certificate.
		/// 
		/// 	Use this delegate to log the error.
		/// </param>
        /// <returns>
		/// 	The configured <see cref="ClientBuilder{TContext}"/>.
		/// </returns>
		/// <remarks>
		/// 	Will accept the certificate or (if it's a CA certificate) any certificate issued by it.
		/// </remarks>
		public static ClientBuilder<TContext> WithServerCertificate<TContext>(this ClientBuilder<TContext> clientBuilder, X509Certificate2 expectServerCertificate, Action<Exception> logError = null)
		{
			if (clientBuilder == null)
                throw new ArgumentNullException(nameof(clientBuilder));
            
            if (expectServerCertificate == null)
                throw new ArgumentNullException(nameof(expectServerCertificate));

            return clientBuilder.ConfigureHttpClientHandler((clientHandler, context) =>
			{
				clientHandler.ServerCertificateCustomValidationCallback = (request, certificate, chain, sslPolicyErrors) =>
				{
					if (sslPolicyErrors != SslPolicyErrors.RemoteCertificateChainErrors)
						return false;

					try
					{
						using (X509Chain certificateChain = new X509Chain())
						{
							certificateChain.ChainPolicy.ExtraStore.Add(expectServerCertificate);
							certificateChain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;
							certificateChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
							
							return certificateChain.Build(certificate);
						}
					}
					catch (Exception chainException)
					{
						if (logError != null)
							logError(chainException);
						
						return false;
					}
				};
			});
		}

		/// <summary>
        ///		Create a copy of the <see cref="ClientBuilder{TContext}"/>, but with verification of the server's SSL certificate disabled (useful when the server has a self-signed certificate).
        /// </summary>
		/// <param name="clientBuilder">
		///		The HTTP client builder.
		/// </param>
		/// <returns>
		/// 	The configured <see cref="ClientBuilder{TContext}"/>.
		/// </returns>
		/// <remarks>
		/// 	Will accept any certificate.
		/// </remarks>
		public static ClientBuilder<TContext> AcceptAnyServerCertificate<TContext>(this ClientBuilder<TContext> clientBuilder)
		{
			if (clientBuilder == null)
                throw new ArgumentNullException(nameof(clientBuilder));
            
            return clientBuilder.ConfigureHttpClientHandler((clientHandler, context) =>
			{
				clientHandler.ServerCertificateCustomValidationCallback = (request, certificate, chain, sslPolicyErrors) =>
				{
					return true; // Verification disabled.
				};
			});
		}
	}
}
