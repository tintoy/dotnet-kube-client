using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace KubeClient.Http
{
    using Clients;

    /// <summary>
    ///		Extension methods for registering and resolving components for dependency injection.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        ///		Register a named <see cref="HttpClient"/> (via <see cref="IHttpClientFactory"/>) using the configuration from an <see cref="ClientBuilder"/>.
        /// </summary>
        /// <param name="services">
        ///		The <see cref="IServiceCollection"/> to configure.
        /// </param>
        /// <param name="name">
        ///		The name of the <see cref="HttpClient"/> configuration to register.
        /// </param>
        /// <param name="http">
        ///		The <see cref="ClientBuilder"/> to use for handler configuration.
        /// </param>
        /// <returns>
        ///		An <see cref="IHttpClientBuilder"/> representing the <see cref="HttpClient"/> configuration (enables further customisation).
        /// </returns>
        public static IHttpClientBuilder AddHttpClient(this IServiceCollection services, string name, ClientBuilder http)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(name)}.", nameof(name));

            if (http == null)
                throw new ArgumentNullException(nameof(http));

            IHttpClientBuilder client = services.AddHttpClient(name);

            return client.Configure(http);
        }

        /// <summary>
        ///		Register a named <see cref="HttpClient"/> (via <see cref="IHttpClientFactory"/>) using the configuration from an <see cref="ClientBuilder"/>.
        /// </summary>
        /// <param name="services">
        ///		The <see cref="IServiceCollection"/> to configure.
        /// </param>
        /// <param name="name">
        ///		The name of the <see cref="HttpClient"/> configuration to register.
        /// </param>
        /// <param name="http">
        ///		The <see cref="ClientBuilder{TContext}"/> to use for handler configuration.
        ///		
        ///		<para>
        ///			 Configuration delegates will have access to a scoped <see cref="IServiceProvider"/>; see <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory#message-handler-scopes-in-ihttpclientfactory"/> for further information.
        ///		</para>
        /// </param>
        /// <returns>
        ///		An <see cref="IHttpClientBuilder"/> representing the <see cref="HttpClient"/> configuration (enables further customisation).
        /// </returns>
        public static IHttpClientBuilder AddHttpClient(this IServiceCollection services, string name, ClientBuilder<IServiceProvider> http)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(name)}.", nameof(name));

            if (http == null)
                throw new ArgumentNullException(nameof(http));

            IHttpClientBuilder client = services.AddHttpClient(name);

            return client.Configure(http);
        }

        /// <summary>
        ///		Configure an <see cref="HttpClient"/> using an client-builder.
        /// </summary>
        /// <param name="client">
        ///		An <see cref="IHttpClientBuilder"/> representing the <see cref="HttpClient"/> configuration.
        /// </param>
        /// <param name="http">
        ///		The <see cref="ClientBuilder"/> used to configure the <see cref="HttpClient"/>.
        /// </param>
        /// <returns>
        ///		The configured <see cref="IHttpClientBuilder"/>.
        /// </returns>
        static IHttpClientBuilder Configure(this IHttpClientBuilder client, ClientBuilder http)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (http == null)
                throw new ArgumentNullException(nameof(http));

            if (http.HasCustomPipelineTerminus)
            {
                client.ConfigurePrimaryHttpMessageHandler(
                    () => http.BuildPipelineTerminus()
                );
            }
            client.ConfigureAdditionalHttpMessageHandlers((handlers, serviceProvider) =>
            {
                List<DelegatingHandler> httpHandlers = http.CreatePipelineHandlers();

                foreach (DelegatingHandler httpHandler in httpHandlers)
                    handlers.Add(httpHandler);
            });

            return client;
        }

        /// <summary>
        ///		Configure an <see cref="HttpClient"/> using an client-builder.
        /// </summary>
        /// <param name="client">
        ///		An <see cref="IHttpClientBuilder"/> representing the <see cref="HttpClient"/> configuration.
        /// </param>
        /// <param name="http">
        ///		The <see cref="ClientBuilder{TContext}"/> used to configure the <see cref="HttpClient"/>.
        /// </param>
        /// <returns>
        ///		The configured <see cref="IHttpClientBuilder"/>.
        /// </returns>
        static IHttpClientBuilder Configure(this IHttpClientBuilder client, ClientBuilder<IServiceProvider> http)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (http == null)
                throw new ArgumentNullException(nameof(http));

            if (http.HasCustomPipelineTerminus)
            {
                client.ConfigurePrimaryHttpMessageHandler(
                    serviceProvider => http.BuildPipelineTerminus(serviceProvider)
                );
            }

            client.ConfigureAdditionalHttpMessageHandlers((handlers, serviceProvider) =>
            {
                List<DelegatingHandler> httpHandlers = http.CreatePipelineHandlers(serviceProvider);

                foreach (DelegatingHandler httpHandler in httpHandlers)
                    handlers.Add(httpHandler);
            });

            return client;
        }

#if NET7_0 || NETSTANDARD2_1
        /// <summary>
        ///     Configure additional HTTP message handlers.
        /// </summary>
        /// <param name="client">
        ///     The <see cref="IHttpClientBuilder"/> to configure.
        /// </param>
        /// <param name="configureAdditionalHandlers">
        ///     A delegate that configures additional handlers.
        /// </param>
        /// <returns>
        ///     The configured <see cref="IHttpClientBuilder"/>.
        /// </returns>
        /// <remarks>
        ///     Polyfill for net7.0/netstandard2.1: ConfigureAdditionalHttpMessageHandlers extension method for IHttpClientBuilder.
        /// </remarks>
        internal static IHttpClientBuilder ConfigureAdditionalHttpMessageHandlers(this IHttpClientBuilder client, Action<IList<DelegatingHandler>, IServiceProvider> configureAdditionalHandlers)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (configureAdditionalHandlers == null)
                throw new ArgumentNullException(nameof(configureAdditionalHandlers));

            client.ConfigureHttpMessageHandlerBuilder(handlerBuilder =>
            {
                configureAdditionalHandlers(handlerBuilder.AdditionalHandlers, handlerBuilder.Services);
            });

            return client;
        }
#endif // NET7_0 || NETSTANDARD2_1
    }
}
