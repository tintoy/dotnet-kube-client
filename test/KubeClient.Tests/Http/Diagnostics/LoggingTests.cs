using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Tests.Http.Diagnostics
{
    using KubeClient.Http.Clients;
    using KubeClient.Http.Diagnostics;
    using KubeClient.Http.Formatters;
    using KubeClient.Http.Testability;
    using KubeClient.Http.Testability.MessageHandlers;

    /// <summary>
    ///		Tests for the HTTPlease.Diagnostics logging facility.
    /// </summary>
    public sealed class LoggingTests
    {
		/// <summary>
		///		Create a new logging test suite.
		/// </summary>
		public LoggingTests()
		{
		}

		/// <summary>
		///		Verify that BeginRequest / EndRequest log entries are emitted for a successful HTTP GET request.
		/// </summary>
		[Fact(DisplayName = "Emit BeginRequest / EndRequest log entries for successful HTTP GET")]
		public async Task Post_Request_Emits_LogEntries()
		{
			string expectedResponseBody = JsonConvert.ToString("hello test");
			
			var logEntries = new List<LogEntry>();

			TestLogger logger = new TestLogger(LogLevel.Debug);
			logger.LogEntries.Subscribe(
				logEntry => logEntries.Add(logEntry)
			);

			ClientBuilder clientBuilder = new ClientBuilder()
				.WithLogging(logger,
					responseComponents: LogMessageComponents.Basic | LogMessageComponents.Body
				);

			HttpClient client = clientBuilder.CreateClient("http://localhost:1234", new MockMessageHandler(
				request => request.CreateResponse(HttpStatusCode.OK,
                    responseBody: expectedResponseBody,
					mediaType: WellKnownMediaTypes.Json
				)
			));
			using (client)
			using (HttpResponseMessage response = await client.GetAsync("/test"))
			{
				response.EnsureSuccessStatusCode();

				string responseBody = await response.Content.ReadAsStringAsync();
				Assert.Equal(expectedResponseBody, responseBody);
			}

			Assert.Equal(3, logEntries.Count);

			LogEntry beginRequestEntry = logEntries[0];
			Assert.Equal(LogEventIds.BeginRequest, beginRequestEntry.EventId);
			Assert.Equal("Performing GET request to 'http://localhost:1234/test'.",
				beginRequestEntry.Message
			);
			Assert.Equal("GET",
				beginRequestEntry.Properties["Method"]
			);
			Assert.Equal(new Uri("http://localhost:1234/test"),
				beginRequestEntry.Properties["RequestUri"]
			);

			LogEntry responseBodyEntry = logEntries[1];
			Assert.Equal(LogEventIds.ResponseBody, responseBodyEntry.EventId);
			Assert.Equal($"Receive response body for GET request to 'http://localhost:1234/test' (OK):\n{expectedResponseBody}",
				responseBodyEntry.Message
			);
			Assert.Equal("GET",
				responseBodyEntry.Properties["Method"]
			);
			Assert.Equal(new Uri("http://localhost:1234/test"),
				responseBodyEntry.Properties["RequestUri"]
			);
			Assert.Equal(expectedResponseBody,
				responseBodyEntry.Properties["Body"]
			);
			Assert.Equal(HttpStatusCode.OK,
				responseBodyEntry.Properties["StatusCode"]
			);

			LogEntry endRequestEntry = logEntries[2];
			Assert.Equal(LogEventIds.EndRequest, endRequestEntry.EventId);
			Assert.Equal("Completed GET request to 'http://localhost:1234/test' (OK).",
				endRequestEntry.Message
			);
			Assert.Equal("GET",
				endRequestEntry.Properties["Method"]
			);
			Assert.Equal(new Uri("http://localhost:1234/test"),
				endRequestEntry.Properties["RequestUri"]
			);
			Assert.Equal(HttpStatusCode.OK,
				endRequestEntry.Properties["StatusCode"]
			);
		}

		/// <summary>
		///		Verify that <see cref="TestLogger"/> can emit an informational log entry.
		/// </summary>
		[Fact(DisplayName = "TestLogger.LogInformation succeeds")]
		public void TestLogger_LogInformation_Success()
		{
			const string name = "World";
			const LogLevel expectedLogLevel = LogLevel.Information;

			var logEntries = new List<LogEntry>();

			TestLogger logger = new TestLogger(LogLevel.Information);
			logger.LogEntries.Subscribe(
				logEntry => logEntries.Add(logEntry)
			);

			logger.LogInformation("Hello, {Name}!", name);

			Assert.Single(logEntries, logEntry =>
			{
				Assert.Equal(new EventId(), logEntry.EventId);
				Assert.Null(logEntry.EventId.Name);

				Assert.Equal(expectedLogLevel, logEntry.Level);

				Assert.Equal($"Hello, {name}!", logEntry.Message);
				Assert.True(
					logEntry.Properties.ContainsKey("Name")
				);
				Assert.Equal(name, logEntry.Properties["Name"]);

				return true;
			});
		}
    }
}
