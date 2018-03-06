using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Xunit.Abstractions;

namespace KubeClient.Extensions.WebSockets.Tests
{
    using Logging;
    using Microsoft.Extensions.Logging;
    using Server;

    /// <summary>
    ///     The base class for Kubernetes WebSocket test suites.
    /// </summary>
    public abstract class WebSocketTestBase
        : TestBase
    {
        /// <summary>
        ///     The next server port to use.
        /// </summary>
        static int NextPort = 13255;

        /// <summary>
        ///     Create a new <see cref="WebSocketTestBase"/>.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        protected WebSocketTestBase(ITestOutputHelper testOutput)
            : base(testOutput)
        {
            int port = Interlocked.Increment(ref NextPort);

            BaseAddress = new Uri($"http://localhost:{port}");
            WebSocketBaseAddress = new Uri($"ws://localhost:{port}");

            Host = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(ConfigureTestServerServices)
                .ConfigureLogging(ConfigureTestServerLogging)
                .UseUrls(BaseAddress.AbsoluteUri)
                .Build();

            Disposal.Add(CancellationSource);
            Disposal.Add(Host);
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
            }, LoggerFactory);
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

        /// <summary>
        ///     Configure logging for the test server.
        /// </summary>
        /// <param name="services">
        ///     The logger factory to configure.
        /// </param>
        protected virtual void ConfigureTestServerLogging(ILoggingBuilder logging)
        {
            if (logging == null)
                throw new ArgumentNullException(nameof(logging));
            
            logging.AddTestOutput(TestOutput, MinLogLevel);
        }
    }
}
