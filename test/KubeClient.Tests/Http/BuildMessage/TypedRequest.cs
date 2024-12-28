using System;
using System.Net.Http;
using Xunit;

namespace KubeClient.Tests.Http.BuildMessage
{
    using KubeClient.Http;
    using KubeClient.Http.Testability.Xunit;

    /// <summary>
    ///		Message-building tests for an <see cref="HttpRequest{TContext}"/> (<see cref="HttpRequest{TContext}.BuildRequestMessage"/>).
    /// </summary>
    public class TypedRequest
    {
		/// <summary>
		///		The default context for requests used by tests.
		/// </summary>
		static readonly string DefaultContext = "Hello World";

		/// <summary>
		///		An empty request.
		/// </summary>
		static readonly HttpRequest<string> EmptyRequest = HttpRequest<string>.Empty;

		/// <summary>
		///		A request with an absolute URI.
		/// </summary>
		static readonly HttpRequest<string> AbsoluteRequest = HttpRequest<string>.Create("http://localhost:1234");

		/// <summary>
		///		A request with a relative URI.
		/// </summary>
		static readonly HttpRequest<string> RelativeRequest = HttpRequest<string>.Create("foo/bar");

		#region Empty requests

		/// <summary>
		///		An <see cref="HttpRequest"/> throws <see cref="InvalidOperationException"/>.
		/// </summary>
		[Fact]
		public void Empty_Throws()
		{
			Assert.Throws<InvalidOperationException>(() =>
			{
				EmptyRequest.BuildRequestMessage(HttpMethod.Get, DefaultContext);
			});
		}

		#endregion Empty requests

		#region Relative URIs

		/// <summary>
		///		An <see cref="HttpRequest"/> with a relative URI throws <see cref="InvalidOperationException"/> if no base URI is supplied.
		/// </summary>
		[Fact]
		public void RelativeUri_NoBaseUri_Throws()
		{
			Assert.Throws<InvalidOperationException>(() =>
			{
				RelativeRequest.BuildRequestMessage(HttpMethod.Get, DefaultContext);
			});
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with a relative URI prepends the supplied base URI to the request URI.
		/// </summary>
		[Fact]
		public void RelativeUri_BaseUri_PrependsBaseUri()
		{
			Uri baseUri = new Uri("http://tintoy.io:5678/");
			
			RequestAssert.MessageHasUri(RelativeRequest, DefaultContext, baseUri,
				expectedUri: new Uri(baseUri, RelativeRequest.Uri)
			);
		}

		#endregion // Relative URIs

		#region Absolute URIs

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute URI ignores the lack of a base URI and uses the request URI.
		/// </summary>
		[Fact]
		public void AbsoluteUri_NoBaseUri_UsesRequestUri()
		{
			RequestAssert.MessageHasUri(AbsoluteRequest, DefaultContext,
				expectedUri: AbsoluteRequest.Uri
			);
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute URI ignores the supplied base URI and uses the request URI.
		/// </summary>
		[Fact]
		public void AbsoluteUri_BaseUri_UsesRequestUri()
		{
			Uri baseUri = new Uri("http://tintoy.io:5678/");

			RequestAssert.MessageHasUri(AbsoluteRequest, DefaultContext, baseUri,
				expectedUri: AbsoluteRequest.Uri
			);
		}

		#endregion // Absolute URIs
	}
}
