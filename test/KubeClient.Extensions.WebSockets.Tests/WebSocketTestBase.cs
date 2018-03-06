using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace KubeClient.Extensions.WebSockets.Tests
{
    using Server;

    /// <summary>
    ///     The base class for Kubernetes WebSocket test suites.
    /// </summary>
    public abstract class WebSocketTestBase
        : IDisposable
    {
        /// <summary>
        ///     The next server port to use.
        /// </summary>
        static int NextPort = 13255;

        /// <summary>
        ///     Create a new <see cref="WebSocketTestBase"/>.
        /// </summary>
        protected WebSocketTestBase()
        {
            int port = Interlocked.Increment(ref NextPort);

            BaseAddress = new Uri($"http://localhost:{port}");
            WebSocketBaseAddress = new Uri($"ws://localhost:{port}");

            Host = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(ConfigureTestServerServices)
                .UseUrls(BaseAddress.AbsoluteUri)
                .Build();
        }

        /// <summary>
        ///     Finaliser for <see cref="WebSocketTestBase"/>.
        /// </summary>
        ~WebSocketTestBase() => Dispose(false);

        /// <summary>
        ///     Dispose of resources being used by the <see cref="WebSocketTestBase"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Dispose of resources being used by the <see cref="WebSocketTestBase"/>.
        /// </summary>
        /// <param name="disposing">
        ///     Explicit disposal?
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CancellationSource.Dispose();
                Host.Dispose();
            }
        }

        /// <summary>
        ///     The test server's base address (http://).
        /// </summary>
        protected Uri BaseAddress { get; }

        /// <summary>
        ///     The test server's base WebSockets address (ws://).
        /// </summary>
        protected Uri WebSocketBaseAddress { get; }

        /// <summary>
        ///     The test server's web host.
        /// </summary>
        protected IWebHost Host { get; }

        /// <summary>
        ///     Test adapter for accepting web sockets.
        /// </summary>
        protected WebSocketTestAdapter WebSocketTestAdapter { get; } = new WebSocketTestAdapter();

        /// <summary>
        ///     The source for cancellation tokens used by the test.
        /// </summary>
        protected CancellationTokenSource CancellationSource { get; } = new CancellationTokenSource();

        /// <summary>
        ///     A <see cref="CancellationToken"/> that can be used to cancel asynchronous operations.
        /// </summary>
        /// <seealso cref="CancellationSource"/>
        protected CancellationToken Cancellation => CancellationSource.Token;

        /// <summary>
        ///     Create a <see cref="KubeApiClient"/> that issues requests against the test server.
        /// </summary>
        /// <returns>
        ///     The configured <see cref="KubeApiClient"/>.
        /// </returns>
        protected KubeApiClient CreateTestClient()
        {
            return KubeApiClient.Create(new KubeClientOptions
            {
                ApiEndPoint = BaseAddress,
                KubeNamespace = "default"

                // No authentication.
            });
        }

        /// <summary>
        ///     Configure services for the test server.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        protected virtual void ConfigureTestServerServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            // Inject WebSocketTestData.
            services.AddSingleton(WebSocketTestAdapter);
        }
    }
}
