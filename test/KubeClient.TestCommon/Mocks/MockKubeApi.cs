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

namespace KubeClient.TestCommon.Mocks
{
    /// <summary>
    ///     A mock implementation of the Kubernetes API.
    /// </summary>
    public sealed class MockKubeApi
        : IAsyncDisposable
    {
        /// <summary>
        ///     The underlying <see cref="WebApplication"/> that hosts the mock API.
        /// </summary>
        readonly WebApplication _webApplication;

        /// <summary>
        ///     The <see cref="TestServer"/> that provides in-process access to the hosted <see cref="WebApplication"/>.
        /// </summary>
        readonly TestServer _testServer;

        /// <summary>
        ///     Has the <see cref="MockKubeApi"/> been disposed?
        /// </summary>
        int _isDisposed;

        #region Construction / Disposal

        /// <summary>
        ///     Create a new <see cref="MockKubeApi"/>.
        /// </summary>
        /// <param name="webApplication">
        ///     The underlying <see cref="WebApplication"/> that hosts the mock API.
        /// </param>
        MockKubeApi(WebApplication webApplication)
        {
            if (webApplication == null)
                throw new ArgumentNullException(nameof(webApplication));

            _webApplication = webApplication;
            _testServer = webApplication.GetTestServer();
        }

        /// <summary>
        ///     Asynchronously dispose of resources being used by the mock API.
        /// </summary>
        /// <returns>
        ///     A <see cref="ValueTask"/> representing the disposal operation.
        /// </returns>
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

        #endregion // Construction / Disposal

        /// <summary>
        ///     The base address of the mock API.
        /// </summary>
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

#if !NET6_0

        /// <summary>
        ///     Create a custom <see cref="HttpMessageHandler" /> for processing HTTP requests/responses with custom configuration against the mock Kubernetes API.
        /// </summary>
        public HttpMessageHandler CreateHandler(Action<HttpContext> additionalContextConfiguration)
        {
            CheckDisposed();

            return _testServer.CreateHandler(additionalContextConfiguration);
        }

#endif // !NET6_0

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

        /// <summary>
        ///     Get a simple dictionary key to identify a resource.
        /// </summary>
        /// <param name="resourceMetadata">
        ///     The resource metadata.
        /// </param>
        /// <returns>
        ///     A string that can be used as a dictionary key to identify the resource.
        /// </returns>
        public static string GetResourceKey(ObjectMetaV1 resourceMetadata)
        {
            if (resourceMetadata == null)
                throw new ArgumentNullException(nameof(resourceMetadata));

            return GetResourceKey(resourceMetadata.Name, resourceMetadata.Namespace);
        }

        /// <summary>
        ///     Get a simple dictionary key to identify a resource.
        /// </summary>
        /// <param name="resourceName">
        ///     The resource name.
        /// </param>
        /// <param name="resourceNamespace">
        ///     The resource namespace.
        /// </param>
        /// <returns>
        ///     A string that can be used as a dictionary key to identify the resource.
        /// </returns>
        public static string GetResourceKey(string resourceName, string resourceNamespace)
        {
            if (String.IsNullOrWhiteSpace(resourceName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(resourceName)}.", nameof(resourceName));

            if (resourceNamespace != null)
                return $"{resourceNamespace}/{resourceName}";

            return resourceName;
        }

        /// <summary>
        ///     Start a new <see cref="MockKubeApi"/> instance.
        /// </summary>
        /// <param name="testOutput">
        ///     An <see cref="ITestOutputHelper"/> representing the output for the current test.
        /// </param>
        /// <param name="configureApp">
        ///     An optional delegate to configure the request-processing pipeline (i.e. API end-points).
        /// </param>
        /// <param name="configureServices">
        ///     An optional delegate to configure server-side components and services (<see cref="IServiceCollection"/>, etc).
        /// </param>
        /// <param name="configureConfiguration">
        ///     An optional delegate to configure server-side configuration (<see cref="IConfiguration"/>, etc).
        /// </param>
        /// <param name="minLogLevel">
        ///     An optional minimum level to log at (defaults to <see cref="LogLevel.Information"/>).
        /// </param>
        /// <returns>
        ///     The configured (and running) <see cref="MockKubeApi"/>.
        /// </returns>
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

        /// <summary>
        ///     Build a <see cref="WebApplication"/> to host the mock API.
        /// </summary>
        /// <param name="testOutput">
        ///     An <see cref="ITestOutputHelper"/> representing the output for the current test.
        /// </param>
        /// <param name="configureApp">
        ///     An optional delegate to configure the server-side request-processing pipeline (i.e. API end-points).
        /// </param>
        /// <param name="configureServices">
        ///     An optional delegate to configure server-side components and services (<see cref="IServiceCollection"/>, etc).
        /// </param>
        /// <param name="configureConfiguration">
        ///     An optional delegate to configure server-side configuration (<see cref="IConfiguration"/>, etc).
        /// </param>
        /// <param name="minLogLevel">
        ///     An optional minimum level to log at (defaults to <see cref="LogLevel.Information"/>).
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebApplication"/>.
        /// </returns>
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

        /// <summary>
        ///     Default server-side configuration for the mock API.
        /// </summary>
        /// <param name="configuration">
        ///     The <see cref="ConfigurationManager"/> to configure.
        /// </param>
        public static void DefaultConfiguration(ConfigurationManager configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Default server-side dependency-injection configuration for the mock API.
        /// </summary>
        /// <param name="context">
        ///     Contextual information about the web host being build.
        /// </param>
        /// <param name="services">
        ///     The <see cref="IServiceCollection"/> to configure.
        /// </param>
        public static void DefaultServices(WebHostBuilderContext context, IServiceCollection services)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (services == null)
                throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        ///     Default configuration for the server-side request-processing pipeline (i.e. API end-points).
        /// </summary>
        /// <param name="webApp">
        ///     The <see cref="WebApplication"/> to configure.
        /// </param>
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

                    StatusV1 responseBody = StatusV1.Failure($"An unexpected error occurred while handling {context.Request.Method} request on '{context.Request.Path}'.",
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
                ILogger logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("MockKubeApi.FallbackHandler");

                logger.LogWarning("Unhandled {RequestMethod} request to {RequestPath}.", context.Request.Method, context.Request.Path);

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

        /// <summary>
        ///     Synchronous delegate for listing all resources of a given (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model representing a list of <typeparamref name="TResource"/>s (must derive from <see cref="KubeResourceListV1{TResource}"/>).
        /// </typeparam>
        /// <returns>
        ///     The list of resources.
        /// </returns>
        public delegate TResourceList ListResourcesHandler<TResource, TResourceList>()
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>;

        /// <summary>
        ///     Synchronous delegate for listing all resources of a given (namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model representing a list of <typeparamref name="TResource"/>s (must derive from <see cref="KubeResourceListV1{TResource}"/>).
        /// </typeparam>
        /// <returns>
        ///     The list of resources.
        /// </returns>
        public delegate TResourceList ListNamespacedResourcesHandler<TResource, TResourceList>(string resourceNamespace)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>;

        /// <summary>
        ///     Asynchronous delegate for listing all resources of a given (non-namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model representing a list of <typeparamref name="TResource"/>s (must derive from <see cref="KubeResourceListV1{TResource}"/>).
        /// </typeparam>
        /// <returns>
        ///     The list of resources.
        /// </returns>
        public delegate Task<TResourceList> AsyncListResourcesHandler<TResource, TResourceList>()
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>;

        /// <summary>
        ///     Aynchronous delegate for listing all resources of a given (namespaced) resource type.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <typeparam name="TResourceList">
        ///     The type of model representing a list of <typeparamref name="TResource"/>s (must derive from <see cref="KubeResourceListV1{TResource}"/>).
        /// </typeparam>
        /// <param name="resourceNamespace">
        ///     The name of the target namespace.
        /// </param>
        /// <returns>
        ///     The list of resources.
        /// </returns>
        public delegate Task<TResourceList> AsyncListNamespacedResourcesHandler<TResource, TResourceList>(string resourceNamespace)
            where TResource : KubeResourceV1
            where TResourceList : KubeResourceListV1<TResource>;

        /// <summary>
        ///     Synchronous delegate for retrieving persisted state for a single (non-namespaced) resource instance.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <returns>
        ///     The resource, or <c>null</c> if no persisted state exists for the resource with the specified name.
        /// </returns>
        public delegate TResource LoadResourceHandler<TResource>(string resourceName)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Synchronous delegate for retrieving persisted state for a single (namespaced) resource instance.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <param name="resourceNamespace">
        ///     The name of the target resource's namespace.
        /// </param>
        /// <returns>
        ///     The resource, or <c>null</c> if no persisted state exists for the resource with the specified name and namespace.
        /// </returns>
        public delegate TResource LoadNamespacedResourceHandler<TResource>(string resourceName, string resourceNamespace)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Asynchronous delegate for retrieving persisted state for a single (non-namespaced) resource instance.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <returns>
        ///     The resource, or <c>null</c> if no persisted state exists for the resource with the specified name.
        /// </returns>
        public delegate Task<TResource> AsyncLoadResourceHandler<TResource>(string resourceName)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Asynchronous delegate for retrieving persisted state for a single (namespaced) resource instance.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <param name="resourceNamespace">
        ///     The name of the target resource's namespace.
        /// </param>
        /// <returns>
        ///     The resource, or <c>null</c> if no persisted state exists for the resource with the specified name and namespace.
        /// </returns>
        public delegate Task<TResource> AsyncLoadNamespacedResourceHandler<TResource>(string resourceName, string resourceNamespace)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Synchronous delegate for persisting state for a single resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resource">
        ///     A <typeparamref name="TResource"/> representing the resource state to persist.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the persisted resource state.
        /// </returns>
        public delegate TResource SaveResourceHandler<TResource>(TResource resource)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Asynchronous delegate for persisting state for a single resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resource">
        ///     A <typeparamref name="TResource"/> representing the resource state to persist.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the persisted resource state.
        /// </returns>
        public delegate Task<TResource> AsyncSaveResourceHandler<TResource>(TResource resource)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Synchronous delegate for applying a patch request to persisted state for a single (non-namespaced) resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the modified (and persisted) resource state, or <c>null</c> if no persisted state exists for the resource with the specified name.
        /// </returns>
        public delegate TResource PatchResourceHandler<TResource>(string resourceName, JsonPatchOperation[] patchRequest)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Synchronous delegate for applying a patch request to persisted state for a single (namespaced) resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <param name="resourceNamespace">
        ///     The name of the target resource's namespace.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the modified (and persisted) resource state, or <c>null</c> if no persisted state exists for the resource with the specified name and namespace.
        /// </returns>
        public delegate TResource PatchNamespacedResourceHandler<TResource>(string resourceName, string resourceNamespace, JsonPatchOperation[] patchRequest)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Asynchronous delegate for applying a patch request to persisted state for a single (non-namespaced) resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the modified (and persisted) resource state, or <c>null</c> if no persisted state exists for the resource with the specified name.
        /// </returns>
        public delegate Task<TResource> AsyncPatchResourceHandler<TResource>(string resourceName, JsonPatchOperation[] patchRequest)
            where TResource : KubeResourceV1;

        /// <summary>
        ///     Asynchronous delegate for applying a patch request to persisted state for a single (non-namespaced) resource.
        /// </summary>
        /// <typeparam name="TResource">
        ///     The type of model representing the target resource type (must derive from <see cref="KubeResourceV1"/>).
        /// </typeparam>
        /// <param name="resourceName">
        ///     The name of the target resource.
        /// </param>
        /// <param name="resourceNamespace">
        ///     The name of the target resource's namespace.
        /// </param>
        /// <returns>
        ///     A <typeparamref name="TResource"/> representing the modified (and persisted) resource state, or <c>null</c> if no persisted state exists for the resource with the specified name and namespace.
        /// </returns>
        public delegate Task<TResource> AsyncPatchNamespacedResourceHandler<TResource>(string resourceName, string resourceNamespace, JsonPatchOperation[] patchRequest)
            where TResource : KubeResourceV1;
    }
}
