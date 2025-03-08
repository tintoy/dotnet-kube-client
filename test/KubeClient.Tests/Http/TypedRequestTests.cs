using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Tests.Http
{
    using KubeClient.Http;
    using KubeClient.Http.Testability.Xunit;

    /// <summary>
    ///		Unit-tests for <see cref="HttpRequest{TContext}"/> that use a context for resolving deferred parameters.
    /// </summary>
    public class TypedRequestTests
	{
		/// <summary>
		///		Verify that a request can build and then invoke request with an absolute and then relative template URI (with query parameters) with deferred values that come from a supplied context value.
		/// </summary>
		/// <returns>
		///		A <see cref="Task"/> representing asynchronous test execution.
		/// </returns>
		[Fact]
		public async Task Request_RelativeTemplateUriWithQuery_DeferredValues_FromContext_Get()
		{
			Uri baseUri = new Uri("http://localhost:1234/");

			TestParameterContext testParameterContext = new TestParameterContext();

			Uri expectedUri = null;
			HttpClient client = TestClients.Expect(requestMessage =>
			{
				Assert.Equal(expectedUri, requestMessage.RequestUri);
			});
			using (client)
			{
				HttpRequest<TestParameterContext> requestBuilder =
					HttpRequest<TestParameterContext>.Factory.Create(baseUri)
						.WithRelativeUri("{action}/{id}?flag={flag?}")
						.WithTemplateParameter("action", context => context.Action)
						.WithTemplateParameter("id", context => context.Id)
						.WithTemplateParameter("flag", context => context.Flag);

				testParameterContext.Action = "foo";
				testParameterContext.Id = 1;
				testParameterContext.Flag = true;

				expectedUri = new Uri(baseUri, "foo/1?flag=True");
				await client.GetAsync(requestBuilder, testParameterContext);

				testParameterContext.Flag = false;

				expectedUri = new Uri(baseUri, "foo/1?flag=False");
				await client.GetAsync(requestBuilder, testParameterContext);

				testParameterContext.Action = "diddly";
				testParameterContext.Id = -17;
				testParameterContext.Flag = null;

				expectedUri = new Uri(baseUri, "diddly/-17");
				await client.GetAsync(requestBuilder, testParameterContext);
			}
		}

		/// <summary>
		///		Verify that a request can build a request with an absolute and then relative template URI (with query parameters) with deferred values that come from a supplied context value.
		/// </summary>
		[Fact]
		public void Request_RelativeTemplateUriWithQuery_DeferredValues_FromContext_Build()
		{
			Uri baseUri = new Uri("http://localhost:1234/");

			HttpRequest<TestParameterContext> requestBuilder =
				HttpRequest<TestParameterContext>.Factory.Create(baseUri)
					.WithRelativeUri("{action}/{id}?flag={flag?}")
					.WithTemplateParameter("action", context => context.Action)
					.WithTemplateParameter("id", context => context.Id)
					.WithTemplateParameter("flag", context => context.Flag);

			TestParameterContext testParameterContext = new TestParameterContext
			{
				Action = "foo",
				Id = 1,
				Flag = true
			};
			using (HttpRequestMessage requestMessage = requestBuilder.BuildRequestMessage(HttpMethod.Get, testParameterContext))
			{
				Assert.Equal(
					new Uri(baseUri, "foo/1?flag=True"),
					requestMessage.RequestUri
				);
			}

			testParameterContext.Flag = false;
			using (HttpRequestMessage requestMessage = requestBuilder.BuildRequestMessage(HttpMethod.Get, testParameterContext))
			{
				Assert.Equal(
					new Uri(baseUri, "foo/1?flag=False"),
					requestMessage.RequestUri
				);
			}

			testParameterContext.Action = "diddly";
			testParameterContext.Id = -17;
			testParameterContext.Flag = null;
			using (HttpRequestMessage requestMessage = requestBuilder.BuildRequestMessage(HttpMethod.Get, testParameterContext))
			{
				Assert.Equal(
					new Uri(baseUri, "diddly/-17"),
					requestMessage.RequestUri
				);
			}
		}

		/// <summary>
		///		Verify that a request can build a request with an absolute and then relative template URI (with query parameters) with deferred values that come from the request's default (intrinsic) context.
		/// </summary>
		[Fact]
		public void Request_RelativeTemplateUriWithQuery_DeferredValues_FromDefaultContext_Build()
		{
			Uri baseUri = new Uri("http://localhost:1234/");

			TestParameterContext testParameterContext = new TestParameterContext
			{
				Action = "foo",
				Id = 1,
				Flag = true
			};

			HttpRequest<TestParameterContext> requestBuilder =
				HttpRequest<TestParameterContext>.Factory.Create(baseUri)
					.WithRelativeUri("{action}/{id}?flag={flag?}")
					.WithTemplateParameter("action", context => context.Action)
					.WithTemplateParameter("id", context => context.Id)
					.WithTemplateParameter("flag", context => context.Flag);

			using (HttpRequestMessage requestMessage = requestBuilder.BuildRequestMessage(HttpMethod.Get, testParameterContext))
			{
				Assert.Equal(
					new Uri(baseUri, "foo/1?flag=True"),
					requestMessage.RequestUri
				);
			}

			testParameterContext.Flag = false;
			using (HttpRequestMessage requestMessage = requestBuilder.BuildRequestMessage(HttpMethod.Get, testParameterContext))
			{
				Assert.Equal(
					new Uri(baseUri, "foo/1?flag=False"),
					requestMessage.RequestUri
				);
			}

			testParameterContext.Action = "diddly";
			testParameterContext.Id = -17;
			testParameterContext.Flag = null;
			using (HttpRequestMessage requestMessage = requestBuilder.BuildRequestMessage(HttpMethod.Get, testParameterContext))
			{
				Assert.Equal(
					new Uri(baseUri, "diddly/-17"),
					requestMessage.RequestUri
				);
			}
		}

		/// <summary>
		///		A parameter-resolution context class used for tests.
		/// </summary>
		class TestParameterContext
		{
			/// <summary>
			///		The "Action" parameter.
			/// </summary>
			public string Action
			{
				get;
				set;
			}

			/// <summary>
			///		The "Id" parameter.
			/// </summary>
			public int Id
			{
				get;
				set;
			}

			/// <summary>
			///		The "Flag" parameter.
			/// </summary>
			public bool? Flag
			{
				get;
				set;
			}
		}
	}
}
