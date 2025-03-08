using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace KubeClient.Tests.Http
{
    using KubeClient.Http;
    using KubeClient.Http.Clients;
    using KubeClient.Http.Testability;
    using KubeClient.Http.Testability.MessageHandlers;
    using KubeClient.Http.Testability.Xunit;

    /// <summary>
    ///		Unit-tests for <see cref="ClientBuilder{TContext}"/>.
    /// </summary>
    public class TypedClientBuilderTests
	{
		/// <summary>
		///		The base URI used for requests in <see cref="ClientBuilder{TContext}"/> tests.
		/// </summary>
		static readonly Uri BaseUri = new Uri("http://localhost");

		/// <summary>
		///		The default path used for requests in <see cref="ClientBuilder{TContext}"/> tests.
		/// </summary>
		static readonly string TestRequestPath = "/foo/bar";

		/// <summary>
		///		The default URI used for requests in <see cref="ClientBuilder{TContext}"/> tests.
		/// </summary>
		static readonly Uri TestRequestUri = new Uri(BaseUri, TestRequestPath);

		/// <summary>
		///		The default request used for <see cref="ClientBuilder{TContext}"/> tests.
		/// </summary>
		static readonly HttpRequest TestRequest = HttpRequest.Create(TestRequestPath);

		/// <summary>
		///		An <see cref="Object"/> used as the context for handler creation.
		/// </summary>
		readonly TestBuilderContext _testBuilderContext = new TestBuilderContext();

		/// <summary>
		///		Verify that a <see cref="ClientBuilder{TContext}"/>, configured with the default message pipeline terminus, can build an <see cref="HttpClient"/> with a custom message pipeline terminus.
		/// </summary>
		/// <returns>
		///		A <see cref="Task"/> representing asynchronous test execution.
		/// </returns>
		[Fact]
		public async Task BuilderWithDefaultTerminus_Can_Build_Client_WithCustomTerminus()
		{
			using MockMessageHandler testHandler = TestHandlers.RespondWith(request =>
			{
				MessageAssert.HasRequestUri(request, TestRequestUri);

				return request.CreateResponse(HttpStatusCode.Ambiguous);
			});

			ClientBuilder<TestBuilderContext> builder = new ClientBuilder<TestBuilderContext>();

			using HttpClient client = builder.CreateClient(_testBuilderContext, BaseUri, messagePipelineTerminus: testHandler);
			using HttpResponseMessage response = await client.GetAsync(TestRequest);

			Assert.Equal(HttpStatusCode.Ambiguous, response.StatusCode);
		}

		/// <summary>
		///		Verify that a <see cref="ClientBuilder{TContext}"/>, configured with a custom message pipeline terminus, can build an <see cref="HttpClient"/> with that message pipeline terminus.
		/// </summary>
		/// <returns>
		///		A <see cref="Task"/> representing asynchronous test execution.
		/// </returns>
		[Fact]
		public async Task BuilderWithCustomTerminus_Can_Build_Client()
		{
			using MockMessageHandler testHandler = TestHandlers.RespondWith(request =>
			{
				MessageAssert.HasRequestUri(request, TestRequestUri);

				return request.CreateResponse(HttpStatusCode.Ambiguous);
			});

			ClientBuilder<TestBuilderContext> builder = new ClientBuilder<TestBuilderContext>()
				.WithMessagePipelineTerminus(context =>
				{
					Assert.Same(_testBuilderContext, context);

					return testHandler;
				});

			using HttpClient client = builder.CreateClient(_testBuilderContext, BaseUri);
			using HttpResponseMessage response = await client.GetAsync(TestRequest);

			Assert.Equal(HttpStatusCode.Ambiguous, response.StatusCode);
		}

		/// <summary>
		///		Verify that a <see cref="ClientBuilder{TContext}"/>, configured with a custom message pipeline terminus, can build an <see cref="HttpClient"/> with a different message pipeline terminus.
		/// </summary>
		/// <returns>
		///		A <see cref="Task"/> representing asynchronous test execution.
		/// </returns>
		[Fact]
		public async Task BuilderWithCustomTerminus_Can_Build_Client_WithCustomTerminus()
		{
			using MockMessageHandler testHandler1 = TestHandlers.RespondWith(request =>
			{
				throw EqualException.ForMismatchedValues(
					expected: "testHandler2",
					actual: "testHandler1",
					banner: "Client was created with the wrong message pipeline terminus."
				);
			});
			using MockMessageHandler testHandler2 = TestHandlers.RespondWith(request =>
			{
				MessageAssert.HasRequestUri(request, TestRequestUri);

				return request.CreateResponse(HttpStatusCode.SeeOther);
			});

			ClientBuilder<TestBuilderContext> builder = new ClientBuilder<TestBuilderContext>()
				.WithMessagePipelineTerminus(context =>
				{
					Assert.Same(_testBuilderContext, context);

					return testHandler1;
				});

			using HttpClient client = builder.CreateClient(_testBuilderContext, BaseUri, messagePipelineTerminus: testHandler2);
			using HttpResponseMessage response = await client.GetAsync(TestRequest);

			Assert.Equal(HttpStatusCode.SeeOther, response.StatusCode);
		}

		/// <summary>
		///		Dummy type to represent context for <see cref="ClientBuilder{TContext}"/>.
		/// </summary>
		class TestBuilderContext
		{
			/// <summary>
			///		Create a new <see cref="TestBuilderContext"/>.
			/// </summary>
			public TestBuilderContext()
			{
			}
		}
	}
}
