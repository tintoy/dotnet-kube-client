namespace KubeClient.Extensions.WebSockets.Tests
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Disposables;
    using System.Reflection;
    using System.Threading;
    using Logging;
    using Microsoft.Extensions.Logging;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    ///     The base class for test suites.
    /// </summary>
    public abstract class TestBase
        : IDisposable
    {
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

            Disposal.Add(TestCancellationSource);

            TestOutput = testOutput;
            LoggerFactory = new LoggerFactory().AddTestOutput(TestOutput, MinLogLevel);
            Log = LoggerFactory.CreateLogger("CurrentTest");

            // Ugly hack to get access to the current test.
            CurrentTest = (ITest)
                TestOutput.GetType()
                    .GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(TestOutput);

            Assert.True(CurrentTest != null, "Cannot retrieve current test from ITestOutputHelper.");

            Disposal.Add(
                Log.BeginScope("CurrentTest", CurrentTest.DisplayName)
            );
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
                try
                {
                    Disposal.Dispose();
                }
                finally
                {
                    if (Log is IDisposable logDisposal)
                        logDisposal.Dispose();
                }
            }
        }

        /// <summary>
        ///     A <see cref="CompositeDisposable"/> representing resources used by the test.
        /// </summary>
        protected CompositeDisposable Disposal { get; } = new CompositeDisposable();

        /// <summary>
        ///     The source for cancellation tokens used by the test.
        /// </summary>
        protected CancellationTokenSource TestCancellationSource { get; } = new CancellationTokenSource();

        /// <summary>
        ///     A <see cref="CancellationToken"/> that can be used to cancel asynchronous operations.
        /// </summary>
        /// <seealso cref="TestCancellationSource"/>
        protected CancellationToken TestCancellation => TestCancellationSource.Token;

        /// <summary>
        ///     Output for the current test.
        /// </summary>
        protected ITestOutputHelper TestOutput { get; }

        /// <summary>
        ///     A <see cref="ITest"/> representing the current test.
        /// </summary>
        protected ITest CurrentTest { get; }

        /// <summary>
        ///     The logger for the current test.
        /// </summary>
        protected ILogger Log { get; }

        /// <summary>
        ///     The logger factory for the current test.
        /// </summary>
        protected ILoggerFactory LoggerFactory { get; }

        /// <summary>
        ///     The logging level for the current test.
        /// </summary>
        protected virtual LogLevel MinLogLevel => LogLevel.Information;

        /// <summary>
        ///     Cancel the test cancellation token.
        /// </summary>
        protected void CancelTest() => TestCancellationSource.Cancel();

        /// <summary>
        ///     Cancel the test after the specified timeout.
        /// </summary>
        /// <param name="timeout">
        ///     A <see cref="TimeSpan"/> representing the timeout period.
        /// </param>
        /// <param name="whenDebugging">
        ///     Schedule cancellation, even if a debugger is attached?
        /// </param>
        protected void TestTimeout(TimeSpan timeout, bool whenDebugging = false)
        {
            if (whenDebugging || !Debugger.IsAttached)
                TestCancellationSource.CancelAfter(timeout);
        }
    }
}