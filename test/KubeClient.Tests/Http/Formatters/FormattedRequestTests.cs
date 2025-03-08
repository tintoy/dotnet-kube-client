using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Tests.Http.Formatters
{
    using KubeClient.Http;
    using KubeClient.Http.Formatters;
    using KubeClient.Http.Testability.Xunit;

    /// <summary>
    ///		Tests for HTTP requests using content formatters.
    /// </summary>
    public class FormattedRequestTests
	{
		/// <summary>
		///		The base URI for requests used by tests.
		/// </summary>
		static readonly Uri BaseUri				= new Uri("http://localhost:1234/");

		/// <summary>
		///		The base <see cref="HttpRequest"/> definition used by tests.
		/// </summary>
		static readonly HttpRequest BaseRequest	= HttpRequest.Factory.Create(BaseUri);

		/// <summary>
		///		Verify that a request builder can build a request with an absolute and then relative template URI, then perform an HTTP GET request.
		/// </summary>
		/// <returns>
		///		A <see cref="Task"/> representing asynchronous test execution.
		/// </returns>
		[Fact]
		public async Task Request_Get_RelativeTemplateUri()
		{
			HttpClient client = JsonTestClients.ExpectJson(
				new Uri(BaseUri, "foo/1234/bar?diddly=bonk"), HttpMethod.Get,
				responseBody: "Success!"
			);
			using (client)
			{
				HttpRequest request =
					BaseRequest.WithRelativeUri("foo/{variable}/bar")
						.WithQueryParameter("diddly", "bonk")
						.WithTemplateParameter("variable", 1234)
						.WithTemplateParameter("diddly", "bonk")
						.UseJson().ExpectJson();

				using (HttpResponseMessage response = await client.GetAsync(request))
				{
					Assert.True(response.IsSuccessStatusCode);
					Assert.NotNull(response.Content);
					Assert.Equal(WellKnownMediaTypes.Json, response.Content.Headers.ContentType.MediaType);

					string responseBody = await response.ReadContentAsAsync<string>();
					Assert.Equal("Success!", responseBody);
				}
			}
		}

		/// <summary>
		///		Verify that a request builder can build a request with an absolute and then relative URI, expecting a JSON response, and then perform an HTTP POST request.
		/// </summary>
		/// <returns>
		///		A <see cref="Task"/> representing asynchronous test execution.
		/// </returns>
		[Fact]
		public async Task Request_Post_RelativeUri_ExpectJson()
		{
			HttpClient client = JsonTestClients.ExpectJson(
				new Uri(BaseUri, "foo/bar"), HttpMethod.Post,
				responseBody: "Success!",
				assertion: async request =>
				{
					MessageAssert.AcceptsMediaType(request, WellKnownMediaTypes.Json);

					await MessageAssert.BodyIsAsync(request,
						"{\"Foo\":\"Bar\",\"Baz\":1234}"
					);
				}
			);
			using (client)
			{
				HttpRequest request =
					BaseRequest.WithRelativeUri("foo/bar")
						.UseJson().ExpectJson();

				HttpResponseMessage response = await
					client.PostAsJsonAsync(request,
						postBody: new
						{
							Foo = "Bar",
							Baz = 1234
						}
					);

				using (response)
				{
					Assert.True(response.IsSuccessStatusCode);
					Assert.NotNull(response.Content?.Headers?.ContentType);
					Assert.Equal(WellKnownMediaTypes.Json, response.Content.Headers.ContentType.MediaType);

					string responseBody = await response.ReadContentAsAsync<string>();
					Assert.Equal("Success!", responseBody);
				}
			}
		}

		/// <summary>
		///		Verify that a request builder can build a request with an absolute and then relative URI, and then perform a JSON-formatted HTTP POST request.
		/// </summary>
		/// <returns>
		///		A <see cref="Task"/> representing asynchronous test execution.
		/// </returns>
		[Fact]
		public async Task Request_PostAsJson_RelativeUri()
		{
			HttpClient client = JsonTestClients.ExpectJson(
				new Uri(BaseUri, "foo/bar"), HttpMethod.Post, responseBody: 1234,
				assertion: async request =>
				{
					MessageAssert.AcceptsMediaType(request, WellKnownMediaTypes.Json);

					await MessageAssert.BodyIsAsync(request, "\"1234\"");
				}
			);
			using (client)
			{
				HttpRequest request =
					BaseRequest.WithRelativeUri("foo/bar")
						.UseJson().ExpectJson();

				int responseBody = await
					client.PostAsJsonAsync(request,
						postBody: 1234.ToString()
					)
					.ReadContentAsAsync<int>();

				Assert.Equal(1234, responseBody);
			}
		}
	}
}
