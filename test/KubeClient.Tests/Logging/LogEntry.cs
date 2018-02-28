using System;
using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace KubeClient.Tests.Logging
{
    /// <summary>
    ///		Represents a log entry captured and republished by a <see cref="TestLogger"/>.
    /// </summary>
    public class LogEntry
	{
		/// <summary>
		///		Create a new <see cref="LogEntry"/>.
		/// </summary>
		/// <param name="level">
		///		The log entry's level (severity).
		/// </param>
		/// <param name="eventId">
		///		The log entry's event Id.
		/// </param>
		/// <param name="message">
		///		The log message.
		/// </param>
		/// <param name="exception">
		///		The log entry's associated exception (if any).
		/// </param>
		/// <param name="state">
		///		State data associated with the log entry.
		/// </param>
		/// <param name="properties">
		///		Properties (if any) associated with the log entry.
		/// </param>
		public LogEntry(LogLevel level, EventId eventId, string message, Exception exception, object state, ImmutableDictionary<string, object> properties)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			if (properties == null)
				throw new ArgumentNullException(nameof(properties));

			Level = level;
			EventId = eventId;
			Message = message;
			Exception = exception;
			State = state;
			Properties = properties;
		}

		/// <summary>
		///		The log entry's level (severity).
		/// </summary>
		public LogLevel Level { get; }

		/// <summary>
		///		The log entry's event Id.
		/// </summary>
		public EventId EventId { get; }

		/// <summary>
		///		The log message.
		/// </summary>
		public string Message { get; }

		/// <summary>
		///		The log entry's associated exception (if any).
		/// </summary>
		public Exception Exception { get; }

		/// <summary>
		///		State data associated with the log entry.
		/// </summary>
		public object State { get; }

		/// <summary>
		///		Properties associated with the log entry (if any).
		/// </summary>
		public ImmutableDictionary<string, object> Properties { get; }
	}
}