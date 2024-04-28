using KubeClient.Models;
using KubeClient.TestCommon.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

#pragma warning disable ASP0012 // Suggest using builder.Services over Host.ConfigureServices or WebHost.ConfigureServices

namespace KubeClient.Extensions.DataProtection.Tests.Mocks
{
    public sealed class MockKubeApi
        : IAsyncDisposable
    {
        readonly WebApplication _webApplication;

        readonly TestServer _testServer;

        int _isDisposed;

        MockKubeApi(WebApplication webApplication)
        {
            if (webApplication == null)
                throw new ArgumentNullException(nameof(webApplication));

            _webApplication = webApplication;
            _testServer = webApplication.GetTestServer();
        }

        public async ValueTask DisposeAsync()
        {
            int wasDisposed = Interlocked.Exchange(ref _isDisposed, 1);
            if (wasDisposed != 0)
                return;

            await using WebApplication webApplication = _webApplication;
            using TestServer testServer = _testServer;
        }

        /// <summary>
        ///     Check if the <see cref="MockKubeApi"/> has been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="MockKubeApi"/> has been disposed.
        /// </exception>
        void CheckDisposed()
        {
            if (_isDisposed != 0)
                throw new ObjectDisposedException(GetType().Name);
        }

        public Uri BaseAddress
        {
            get
            {
                CheckDisposed();

                return _testServer.BaseAddress;
            }
        }

        /// <summary>
        ///     Create a custom <see cref="HttpMessageHandler" /> for processing HTTP requests/responses against the mock Kubernetes API.
        /// </summary>
        public HttpMessageHandler CreateHandler()
        {
            CheckDisposed();

            return _testServer.CreateHandler();
        }

        /// <summary>
        ///     Create a custom <see cref="HttpMessageHandler" /> for processing HTTP requests/responses with custom configuration against the mock Kubernetes API.
        /// </summary>
        public HttpMessageHandler CreateHandler(Action<HttpContext> additionalContextConfiguration)
        {
            CheckDisposed();

            return _testServer.CreateHandler(additionalContextConfiguration);
        }

        /// <summary>
        ///     Create a <see cref="HttpClient" /> for processing HTTP requests/responses against the mock Kubernetes API.
        /// </summary>
        public HttpClient CreateClient()
        {
            CheckDisposed();

            return _testServer.CreateClient();
        }

        /// <summary>
        ///     Create a <see cref="WebSocketClient" /> for interacting with the mock Kubernetes API.
        /// </summary>
        public WebSocketClient CreateWebSocketClient()
        {
            CheckDisposed();
            
            return _testServer.CreateWebSocketClient();
        }

        /// <summary>
        ///     Start constructing a request message for submission to the mock Kubernetes API.
        /// </summary>
        /// <param name="path">
        ///     The request path.
        /// </param>
        /// <returns>
        ///     A <see cref="RequestBuilder"/> used to construct the request.
        /// </returns>
        public RequestBuilder CreateRequest(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            return _testServer.CreateRequest(path);
        }

        public static MockKubeApi Create(ITestOutputHelper testOutput, Action<WebApplication> configureApp = null, Action<WebHostBuilderContext, IServiceCollection> configureServices = null, Action<ConfigurationManager> configureConfiguration = null, LogLevel? minLogLevel = null)
        {
            if (testOutput == null)
                throw new ArgumentNullException(nameof(testOutput));

            WebApplication webApp = BuildWebApplication(testOutput, configureApp, configureServices, configureConfiguration, minLogLevel);

            try
            {
                webApp.Start();

                return new MockKubeApi(webApp);
            }
            catch (Exception)
            {
                // Clean up.
                using (webApp)
                {
                    throw;
                }
            }
        }

        static WebApplication BuildWebApplication(ITestOutputHelper testOutput, Action<WebApplication> configureApp, Action<WebHostBuilderContext, IServiceCollection> configureServices, Action<ConfigurationManager> configureConfiguration, LogLevel? minLogLevel)
        {
            if (testOutput == null)
                throw new ArgumentNullException(nameof(testOutput));

            WebApplicationBuilder webAppBuilder = WebApplication.CreateBuilder();

            DefaultConfiguration(webAppBuilder.Configuration);
            if (configureConfiguration != null)
                configureConfiguration(webAppBuilder.Configuration);

            webAppBuilder.Logging.AddDebug();
            webAppBuilder.Logging.AddTestOutput(testOutput, minLogLevel);
            if (minLogLevel != null)
                webAppBuilder.Logging.SetMinimumLevel(minLogLevel.Value);
            
            webAppBuilder.WebHost.UseTestServer(testServer =>
            {
                testServer.AllowSynchronousIO = true;
            });
            
            webAppBuilder.WebHost.ConfigureServices(DefaultServices);
            if (configureServices != null)
                webAppBuilder.WebHost.ConfigureServices(configureServices);

            WebApplication webApp = webAppBuilder.Build();

            DefaultApplication(webApp);

            if (configureApp != null)
                configureApp(webApp);

            return webApp;
        }

        public static void DefaultConfiguration(ConfigurationManager configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
        }

        public static void DefaultServices(WebHostBuilderContext context, IServiceCollection services)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (services == null)
                throw new ArgumentNullException(nameof(services));
        }

        public static void DefaultApplication(WebApplication webApp)
        {
            if (webApp == null)
                throw new ArgumentNullException(nameof(webApp));

            webApp.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async (context) =>
                {
                    if (context.Response.HasStarted)
                        return;

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    StatusV1 responseBody = StatusV1.Failure($"Unexpected error  handler is registered for {context.Request.Method} requests on '{context.Request.Path}'.",
                        reason: "InternalServerError",
                        code: StatusCodes.Status500InternalServerError
                    );

                    IExceptionHandlerFeature exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature?.Error != null)
                        responseBody.Message = exceptionHandlerFeature.Error.ToString();

                    await context.Response.WriteAsync(
                        JsonConvert.SerializeObject(responseBody),
                        cancellationToken: context.RequestAborted
                    );
                }
            });

            webApp.MapFallback((HttpContext context) =>
            {
                return Results.Content(
                    JsonConvert.SerializeObject(
                        StatusV1.Failure($"No handler is registered for {context.Request.Method} requests on '{context.Request.Path}'.",
                            reason: "NotImplemented",
                            code: StatusCodes.Status501NotImplemented
                        )
                    ),
                    contentType: "application/json",
                    statusCode: StatusCodes.Status501NotImplemented
                );
            });
        }
    }
}
