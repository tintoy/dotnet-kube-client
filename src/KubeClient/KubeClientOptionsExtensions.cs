using KubeClient.MessageHandlers;
using System;

namespace KubeClient
{
    using Http.Clients;
    using Http.Diagnostics;

    /// <summary>
    ///     Extension methods for <see cref="KubeClientOptions"/>.
    /// </summary>
    public static class KubeClientOptionsExtensions
    {
        /// <summary>
        ///     Configure a <see cref="ClientBuilder"/> from <see cref="KubeClientOptions"/>.
        /// </summary>
        /// <param name="kubeClientOptions">
        ///     The <see cref="KubeClientOptions"/> used to configure the client.
        /// </param>
        /// <param name="clientBuilder">
        ///     The <see cref="ClientBuilder"/> to configure.
        /// </param>
        /// <returns>
        ///     The configured <see cref="ClientBuilder"/>.
        /// </returns>
        public static ClientBuilder Configure(this KubeClientOptions kubeClientOptions, ClientBuilder clientBuilder)
        {
            if (kubeClientOptions == null)
                throw new ArgumentNullException(nameof(kubeClientOptions));

            if (clientBuilder == null)
                throw new ArgumentNullException(nameof(clientBuilder));

            kubeClientOptions.EnsureValid();

            switch (kubeClientOptions.AuthStrategy)
            {
                case KubeAuthStrategy.Basic:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        () => new BasicAuthenticationHandler(kubeClientOptions.Username, kubeClientOptions.Password)
                    );

                    break;
                }
                case KubeAuthStrategy.BearerToken:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        () => new StaticBearerTokenHandler(kubeClientOptions.AccessToken)
                    );

                    break;
                }
                case KubeAuthStrategy.BearerTokenProvider:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        () => new CommandBearerTokenHandler(
                            accessTokenCommand: kubeClientOptions.AccessTokenCommand,
                            accessTokenCommandArguments: kubeClientOptions.AccessTokenCommandArguments,
                            accessTokenSelector: kubeClientOptions.AccessTokenSelector,
                            accessTokenExpirySelector: kubeClientOptions.AccessTokenExpirySelector,
                            initialAccessToken: kubeClientOptions.InitialAccessToken,
                            initialTokenExpiryUtc: kubeClientOptions.InitialTokenExpiryUtc,
                            environmentVariables: kubeClientOptions.EnvironmentVariables
                        )
                    );

                    break;
                }
                case KubeAuthStrategy.CredentialPlugin:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        () => new CommandBearerTokenHandler(
                            accessTokenCommand: kubeClientOptions.AccessTokenCommand,
                            accessTokenCommandArguments: kubeClientOptions.AccessTokenCommandArguments,
                            accessTokenSelector: kubeClientOptions.AccessTokenSelector ?? ".status.token",
                            accessTokenExpirySelector: kubeClientOptions.AccessTokenExpirySelector ?? ".status.expirationTimestamp",
                            initialAccessToken: kubeClientOptions.InitialAccessToken,
                            initialTokenExpiryUtc: kubeClientOptions.InitialTokenExpiryUtc,
                            environmentVariables: kubeClientOptions.EnvironmentVariables
                        )
                    );

                    break;
                }
                case KubeAuthStrategy.ClientCertificate:
                {
                    if (kubeClientOptions.ClientCertificate == null)
                        throw new KubeClientException("Cannot specify ClientCertificate authentication strategy without supplying a client certificate.");

                    clientBuilder = clientBuilder.WithClientCertificate(kubeClientOptions.ClientCertificate);

                    break;
                }
            }

            if (kubeClientOptions.AllowInsecure)
                clientBuilder = clientBuilder.AcceptAnyServerCertificate();
            else if (kubeClientOptions.CertificationAuthorityCertificate != null)
                clientBuilder = clientBuilder.WithServerCertificate(kubeClientOptions.CertificationAuthorityCertificate);

            LogMessageComponents logComponents = LogMessageComponents.Basic;
            if (kubeClientOptions.LogHeaders)
                logComponents |= LogMessageComponents.Headers;
            if (kubeClientOptions.LogPayloads)
                logComponents |= LogMessageComponents.Body;

            clientBuilder = clientBuilder.WithLogging(
                logger: kubeClientOptions.LoggerFactory.CreateLogger(
                    typeof(KubeApiClient).FullName + ".Http"
                ),
                requestComponents: logComponents,
                responseComponents: logComponents
            );

            return clientBuilder;
        }

        /// <summary>
        ///     Configure a <see cref="ClientBuilder{TContext}"/> from <see cref="KubeClientOptions"/>.
        /// </summary>
        /// <param name="kubeClientOptions">
        ///     The <see cref="KubeClientOptions"/> used to configure the client.
        /// </param>
        /// <param name="clientBuilder">
        ///     The <see cref="ClientBuilder{TContext}"/> to configure.
        /// </param>
        /// <returns>
        ///     The configured <see cref="ClientBuilder{TContext}"/>.
        /// </returns>
        public static ClientBuilder<TContext> Configure<TContext>(this KubeClientOptions kubeClientOptions, ClientBuilder<TContext> clientBuilder)
        {
            if (kubeClientOptions == null)
                throw new ArgumentNullException(nameof(kubeClientOptions));

            if (clientBuilder == null)
                throw new ArgumentNullException(nameof(clientBuilder));

            kubeClientOptions.EnsureValid();

            switch (kubeClientOptions.AuthStrategy)
            {
                case KubeAuthStrategy.Basic:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        context => new BasicAuthenticationHandler(kubeClientOptions.Username, kubeClientOptions.Password)
                    );

                    break;
                }
                case KubeAuthStrategy.BearerToken:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        context => new StaticBearerTokenHandler(kubeClientOptions.AccessToken)
                    );

                    break;
                }
                case KubeAuthStrategy.BearerTokenProvider:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        context => new CommandBearerTokenHandler(
                            accessTokenCommand: kubeClientOptions.AccessTokenCommand,
                            accessTokenCommandArguments: kubeClientOptions.AccessTokenCommandArguments,
                            accessTokenSelector: kubeClientOptions.AccessTokenSelector,
                            accessTokenExpirySelector: kubeClientOptions.AccessTokenExpirySelector,
                            initialAccessToken: kubeClientOptions.InitialAccessToken,
                            initialTokenExpiryUtc: kubeClientOptions.InitialTokenExpiryUtc,
                            environmentVariables: kubeClientOptions.EnvironmentVariables
                        )
                    );

                    break;
                }
                case KubeAuthStrategy.CredentialPlugin:
                {
                    clientBuilder = clientBuilder.AddHandler(
                        context => new CommandBearerTokenHandler(
                            accessTokenCommand: kubeClientOptions.AccessTokenCommand,
                            accessTokenCommandArguments: kubeClientOptions.AccessTokenCommandArguments,
                            accessTokenSelector: kubeClientOptions.AccessTokenSelector ?? ".status.token",
                            accessTokenExpirySelector: kubeClientOptions.AccessTokenExpirySelector ?? ".status.expirationTimestamp",
                            initialAccessToken: kubeClientOptions.InitialAccessToken,
                            initialTokenExpiryUtc: kubeClientOptions.InitialTokenExpiryUtc,
                            environmentVariables: kubeClientOptions.EnvironmentVariables
                        )
                    );

                    break;
                }
                case KubeAuthStrategy.ClientCertificate:
                {
                    if (kubeClientOptions.ClientCertificate == null)
                        throw new KubeClientException("Cannot specify ClientCertificate authentication strategy without supplying a client certificate.");

                    clientBuilder = clientBuilder.WithClientCertificate(kubeClientOptions.ClientCertificate);

                    break;
                }
            }

            if (kubeClientOptions.AllowInsecure)
                clientBuilder = clientBuilder.AcceptAnyServerCertificate();
            else if (kubeClientOptions.CertificationAuthorityCertificate != null)
                clientBuilder = clientBuilder.WithServerCertificate(kubeClientOptions.CertificationAuthorityCertificate);

            LogMessageComponents logComponents = LogMessageComponents.Basic;
            if (kubeClientOptions.LogHeaders)
                logComponents |= LogMessageComponents.Headers;
            if (kubeClientOptions.LogPayloads)
                logComponents |= LogMessageComponents.Body;

            clientBuilder = clientBuilder.WithLogging(
                logger: kubeClientOptions.LoggerFactory.CreateLogger(
                    typeof(KubeApiClient).FullName + ".Http"
                ),
                requestComponents: logComponents,
                responseComponents: logComponents
            );

            return clientBuilder;
        }

        /// <summary>
        ///     Determine if Kubernetes client options are configured to use the default (no-op) logger factory (<see cref="KubeClientOptions.DefaultLoggerFactory"/>).
        /// </summary>
        /// <param name="kubeClientOptions">
        ///     The <see cref="KubeClientOptions"/> to examine.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the options are configured to use the default logger factory; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUsingDefaultLoggerFactory(this KubeClientOptions kubeClientOptions)
        {
            if (kubeClientOptions == null)
                throw new ArgumentNullException(nameof(kubeClientOptions));

            return ReferenceEquals(kubeClientOptions.LoggerFactory, KubeClientOptions.DefaultLoggerFactory);
        }

        /// <summary>
        ///     Configure Kubernetes client options to use the default (no-op) logger factory (<see cref="KubeClientOptions.DefaultLoggerFactory"/>).
        /// </summary>
        /// <param name="kubeClientOptions">
        ///     The <see cref="KubeClientOptions"/> to configure.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeClientOptions"/>.
        /// </returns>
        public static KubeClientOptions UseDefaultLoggerFactory(this KubeClientOptions kubeClientOptions)
        {
            if (kubeClientOptions == null)
                throw new ArgumentNullException(nameof(kubeClientOptions));

            kubeClientOptions.LoggerFactory = KubeClientOptions.DefaultLoggerFactory;

            return kubeClientOptions;
        }
    }
}
