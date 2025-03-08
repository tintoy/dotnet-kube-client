using System;
using System.Net.Http;
using Xunit;

namespace KubeClient.Tests.Http.BuildMessage
{
    using KubeClient.Http;
    using KubeClient.Http.Testability.Xunit;

    /// <summary>
    ///		Message-building tests for <see cref="HttpRequest"/> (<see cref="HttpRequest.BuildRequestMessage"/>).
    /// </summary>
    public class UntypedRequest
	{
		/// <summary>
		///		A request with an absolute URI.
		/// </summary>
		static readonly HttpRequest AbsoluteRequest = HttpRequest.Create("http://localhost:1234");

		/// <summary>
		///		A request with a relative URI.
		/// </summary>
		static readonly HttpRequest RelativeRequest = HttpRequest.Create("foo/bar");

		/// <summary>
		///		An <see cref="HttpRequest"/> throws <see cref="InvalidOperationException"/>.
		/// </summary>
		[Fact]
		public void Empty_Throws()
		{
			Assert.Throws<InvalidOperationException>(() =>
			{
				HttpRequest.Empty.BuildRequestMessage(HttpMethod.Get);
			});
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with a relative URI throws <see cref="InvalidOperationException"/> if no base URI is supplied.
		/// </summary>
		[Fact]
		public void RelativeUri_NoBaseUri_Throws()
		{
			Assert.Throws<InvalidOperationException>(() =>
			{
				RelativeRequest.BuildRequestMessage(HttpMethod.Get);
			});
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with a relative URI prepends the supplied base URI to the request URI.
		/// </summary>
		[Fact]
		public void RelativeUri_BaseUri_PrependsBaseUri()
		{
			Uri baseUri = new Uri("http://tintoy.io:5678/");

			RequestAssert.MessageHasUri(RelativeRequest, baseUri,
				expectedUri: new Uri(baseUri, RelativeRequest.Uri)
			);
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute URI ignores the lack of a base URI and uses the request URI.
		/// </summary>
		[Fact]
		public void AbsoluteUri_NoBaseUri_UsesRequestUri()
		{
			RequestAssert.MessageHasUri(AbsoluteRequest,
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

			RequestAssert.MessageHasUri(AbsoluteRequest, baseUri,
				expectedUri: AbsoluteRequest.Uri
			);
		}

		#region Template URIs

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute template URI, using statically-bound template parameters.
		/// </summary>
		[Fact]
		public void Absoluteuri_Template()
		{
			HttpRequest request =
				HttpRequest.Factory.Create("http://localhost:1234/{action}/{id}")
					.WithTemplateParameter("action", "foo")
					.WithTemplateParameter("id", "bar");

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar"
			);
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute template URI, using dynamically-bound template parameters.
		/// </summary>
		[Fact]
		public void AbsoluteUri_Template_DeferredValues()
		{
			string action = "foo";
			string id = "bar";

			HttpRequest request =
				HttpRequest.Factory.Create("http://localhost:1234/{action}/{id}")
					.WithTemplateParameter("action", () => action)
					.WithTemplateParameter("id", () => id);

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar"
			);
			
			action = "diddly";
			id = "dee";

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/diddly/dee"
			);
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute template URI that includes a query component, using statically-bound template parameters.
		/// </summary>
		[Fact]
		public void AbsoluteUri_Template_Query()
		{
			HttpRequest request =
				HttpRequest.Factory.Create("http://localhost:1234/{action}/{id}?flag={flag}")
					.WithTemplateParameter("action", "foo")
					.WithTemplateParameter("id", "bar")
					.WithTemplateParameter("flag", true);

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar?flag=True"
			);
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute template URI that includes a query component, using dynamically-bound template parameters.
		/// </summary>
		[Fact]
		public void AbsoluteUri_Template_Query_DeferredValues()
		{
			string action = "foo";
			string id = "bar";
			bool? flag = true;

			HttpRequest request =
				HttpRequest.Factory.Create("http://localhost:1234/")
					.WithRelativeUri("{action}/{id}?flag={flag?}")
					.WithTemplateParameter("action", () => action)
					.WithTemplateParameter("id", () => id)
					.WithTemplateParameter("flag", () => flag);

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar?flag=True"
			);

			action = "diddly";
			id = "dee";
			flag = null;

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/diddly/dee"
			);
		}

		#endregion // Template URIs

		#region Query parameters

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute URI that adds a query component, using statically-bound template parameters.
		/// </summary>
		[Fact]
		public void AbsoluteUri_Query()
		{
			HttpRequest request =
				HttpRequest.Factory.Create("http://localhost:1234/foo/bar")
					.WithQueryParameter("flag", true);

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar?flag=True"
			);
		}

		/// <summary>
		///		An <see cref="HttpRequest"/> with an absolute URI that adds a query component, using dynamically-bound template parameters.
		/// </summary>
		[Fact]
		public void AbsoluteUri_AddQuery_DeferredValues()
		{
			bool? flag = true;

			HttpRequest request =
				HttpRequest.Factory.Create("http://localhost:1234/foo/bar")
					.WithQueryParameter("flag", () => flag);

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar?flag=True"
			);
			
			flag = null;

			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar"
			);
		}

		/// <summary>
		/// 	An <see cref="HttpRequest"/> with an absolute URI that adds a query component, with no additional path component).
		/// </summary>
		[Fact]
		public void AbsoluteUri_AddQuery_EmptyPath()
		{
			HttpRequest request =
				AbsoluteRequest.WithRelativeUri("foo/bar")
					.WithRelativeUri("?baz=bonk");
			
			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar?baz=bonk"
			);
		}

		/// <summary>
		/// 	An <see cref="HttpRequest"/> with an absolute URI that adds a query component, with no additional path component).
		/// </summary>
		[Fact]
		public void AbsoluteUri_WithQuery_AddQuery_EmptyPath()
		{
			HttpRequest request =
				AbsoluteRequest.WithRelativeUri("foo/bar?baz=bonk")
					.WithRelativeUri("?bo=diddly");
			
			RequestAssert.MessageHasUri(request,
				expectedUri: "http://localhost:1234/foo/bar?baz=bonk&bo=diddly"
			);
		}

		#endregion // Query parameters
	}
}
