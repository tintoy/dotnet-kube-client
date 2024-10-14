using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reactive.Disposables;
using System.Reflection;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.TestCommon
{
    using Logging;

    /// <summary>
    ///     The base class for test suites.
    /// </summary>
    public abstract class TestBase
        : IDisposable
    {
        /// <summary>
        ///     The test-level service provider.
        /// </summary>
        ServiceProvider _serviceProvider;

        /// <summary>
        ///     The test-level logger.
        /// </summary>
        ILogger _testLogger;

        /// <summary>
        ///     Create a new test-suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        protected TestBase(ITestOutputHelper testOutput)
        {
            if (testOutput == null)
                throw new ArgumentNullException(nameof(testOutput));

            // We *must* have a synchronisation context for the test, or we'll see random deadlocks.
            if (SynchronizationContext.Current == null)
            {
                SynchronizationContext.SetSynchronizationContext(
                    new SynchronizationContext()
                );
            }

            TestOutput = testOutput;

            // Ugly hack to get access to the current test.
            CurrentTest = (ITest)
                TestOutput.GetType()
                    .GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(TestOutput);

            Assert.True(CurrentTest != null, "Cannot retrieve current test from ITestOutputHelper.");
        }

        /// <summary>
        ///     Finaliser for <see cref="TestBase"/>.
        /// </summary>
        ~TestBase()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Dispose of resources being used by the test suite.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Dispose of resources being used by the test suite.
        /// </summary>
        /// <param name="disposing">
        ///     Explicit disposal?
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                using (Disposal)
                {
                    if (_serviceProvider != null)
                    {
                        _serviceProvider.Dispose();
                        _serviceProvider = null;
                    }
                }
            }
        }

        /// <summary>
        ///     A <see cref="CompositeDisposable"/> representing resources used by the test.
        /// </summary>
        protected CompositeDisposable Disposal { get; } = new CompositeDisposable();

        /// <summary>
        ///     Output for the current test.
        /// </summary>
        protected ITestOutputHelper TestOutput { get; }

        /// <summary>
        ///     A <see cref="ITest"/> representing the current test.
        /// </summary>
        protected ITest CurrentTest { get; }

        /// <summary>
        ///     The logger factory for the current test.
        /// </summary>
        protected ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();

        /// <summary>
        ///     The logger for the current test.
        /// </summary>
        protected ILogger Log
        {
            get
            {
                if (_testLogger == null)
                {
                    _testLogger = LoggerFactory.CreateLogger("CurrentTest");

                    Disposal.Add(
                        Log.BeginScope("{CurrentTest}", CurrentTest.DisplayName)
                    );
                }

                return _testLogger;
            }
        }

        /// <summary>
        ///     The logging level for the current test.
        /// </summary>
        protected virtual LogLevel MinLogLevel => LogLevel.Information;

        /// <summary>
        ///     The test-level service provider.
        /// </summary>
        protected IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                    _serviceProvider = BuildServiceProvider();

                return _serviceProvider;
            }
        }

        /// <summary>
        ///     Create and use a scoped <see cref="IServiceProvider"/> (via <see cref="IServiceScope"/>).
        /// </summary>
        /// <param name="action">
        ///     A delegate that uses the scoped service provider.
        /// </param>
        protected void UseScopedServiceProvider(Action<IServiceProvider> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            using (IServiceScope serviceScope = ServiceProvider.CreateScope())
            {
                action(serviceScope.ServiceProvider);
            }
        }

        /// <summary>
        ///     Create and use a scoped <see cref="IServiceProvider"/> (via <see cref="IServiceScope"/>).
        /// </summary>
        /// <typeparam name="TResult">
        ///     The action result type.
        /// </typeparam>
        /// <param name="action">
        ///     A delegate that uses the scoped service provider and returns a <typeparamref name="TResult"/>.
        /// </param>
        /// <returns>
        ///     The result.
        /// </returns>
        protected TResult UseScopedServiceProvider<TResult>(Func<IServiceProvider, TResult> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            using (IServiceScope serviceScope = ServiceProvider.CreateScope())
            {
                return action(serviceScope.ServiceProvider);
            }
        }

        /// <summary>
        ///     Build a service provider using the test-level configuration.
        /// </summary>
        /// <returns>
        ///     The configured <see cref="ServiceProvider"/>.
        /// </returns>
        protected ServiceProvider BuildServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();

            ConfigureServices(services);

            return services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true,
                ValidateScopes = true,
            });
        }

        /// <summary>
        ///     Configure services for dependency-injection.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        protected virtual void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddLogging(logging =>
            {
                logging.SetMinimumLevel(MinLogLevel);
                logging.AddDebug();

                logging.AddTestOutput(TestOutput, MinLogLevel);
            });
        }
    }
}