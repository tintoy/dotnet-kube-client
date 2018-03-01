using System;
using System.Collections.Immutable;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;

namespace KubeClient.Tests.Logging
{
    /// <summary>
    /// 	A stub implementation of <see cref="ILogger"/> for use in tests.
    /// </summary>
    public sealed class TestLogger
		: ILogger, IDisposable
	{
        /// <summary>
        ///     The subject used to publish log entries.
        /// </summary>
		readonly Subject<LogEntry> _logEntries = new Subject<LogEntry>();

		/// <summary>
		/// 	Create a new <see cref="TestLogger"/>.
		/// </summary>
		/// <param name="minLogLevel">
		/// 	The logger's minimum log level.
		/// </param>
		public TestLogger(LogLevel minLogLevel)
		{
			MinLogLevel = minLogLevel;
		}

		/// <summary>
		///		Dispose of resources being used by the <see cref="TestLogger"/>.
		/// </summary>
		public void Dispose() => _logEntries.Dispose();

		/// <summary>
		/// 	The logger's minimum log level.
		/// </summary>
		public LogLevel MinLogLevel { get; }

		/// <summary>
		///		An observable sequence of log entries emitted by the <see cref="TestLogger"/>.
		/// </summary>
		public IObservable<LogEntry> LogEntries => _logEntries;

		/// <summary>
        /// 	Emit a log entry.
        /// </summary>
        /// <param name="level">
		/// 	The log entry's level.
		/// </param>
        /// <param name="eventId">
		/// 	The log entry's associated event Id.
		/// </param>
        /// <param name="state">
		/// 	The log entry to be written. Can be also an object.
		/// </param>
        /// <param name="exception">
		/// 	The exception (if any) related to the log entry.
		/// </param>
        /// <param name="formatter">
		/// 	A function that creates a <c>string</c> log message from the <paramref name="state"/> and <paramref name="exception"/>.
		/// </param>
        public void Log<TState>(LogLevel level, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (formatter == null)
				throw new ArgumentNullException(nameof(formatter));

			ImmutableDictionary<string, object> properties =
				(state is FormattedLogValues formattedLogValues)
					? ImmutableDictionary.CreateRange(formattedLogValues)
					: ImmutableDictionary<string, object>.Empty;

			_logEntries.OnNext(new LogEntry(
				level,
				eventId,
				formatter(state, exception),
				exception,
				state,
				properties
			));
		}

        /// <summary>
        /// 	Check if the given <paramref name="logLevel"/> is enabled.
        /// </summary>
        /// <param name="logLevel">
		/// 	The level to be checked.
		/// </param>
        /// <returns>
		/// 	<c>true</c> if enabled; otherwise, <c>false</c>.
		/// </returns>
        public bool IsEnabled(LogLevel logLevel) => logLevel >= MinLogLevel;

        /// <summary>
        /// 	Begin a logical operation scope.
        /// </summary>
        /// <param name="state">
		/// 	An identifier for the scope.
		/// </param>
        /// <returns>
		/// 	An <see cref="IDisposable"/> that ends the logical operation scope when disposed.
		/// </returns>
		public IDisposable BeginScope<TState>(TState state) => Disposable.Empty;
	}
}