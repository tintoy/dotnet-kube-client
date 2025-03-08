using System;
using System.Collections.Generic;
using Xunit;

namespace KubeClient.Tests.Http
{
    using KubeClient.Http;
    using KubeClient.Http.Templates;

    /// <summary>
    ///		Unit-tests for URI templating functionality.
    /// </summary>
    public sealed class UriTemplateTests
		: UnitTestBase
	{
		/// <summary>
		///		Create a new URI templating unit-test suite.
		/// </summary>
		public UriTemplateTests()
		{
		}

		/// <summary>
		///		Verify that template segments can be parsed from a URI.
		/// </summary>
		[Fact]
		public void Can_Parse_TemplateSegments_From_Uri()
		{
			IReadOnlyList<TemplateSegment> segments = TemplateSegment.Parse(
				"api/{controller}/{action}/{id?}/properties"
			);
			
			Assert.Equal(6, segments.Count);
			Assert.IsAssignableFrom<RootUriSegment>(segments[0]);
			
			LiteralUriSegment apiSegment = Assert.IsAssignableFrom<LiteralUriSegment>(segments[1]);
			Assert.Equal("api", apiSegment.Value);
			
			ParameterizedUriSegment controllerSegment = Assert.IsAssignableFrom<ParameterizedUriSegment>(segments[2]);
			Assert.True(controllerSegment.IsDirectory);
			Assert.Equal("controller", controllerSegment.TemplateParameterName);
			Assert.False(controllerSegment.IsOptional);

			ParameterizedUriSegment actionSegment = Assert.IsAssignableFrom<ParameterizedUriSegment>(segments[3]);
			Assert.True(actionSegment.IsDirectory);
			Assert.Equal("action", actionSegment.TemplateParameterName);
			Assert.False(actionSegment.IsOptional);

			ParameterizedUriSegment idSegment = Assert.IsAssignableFrom<ParameterizedUriSegment>(segments[4]);
			Assert.True(idSegment.IsDirectory);
			Assert.Equal("id", idSegment.TemplateParameterName);
			Assert.True(idSegment.IsOptional);

			LiteralUriSegment propertiesSegment = Assert.IsAssignableFrom<LiteralUriSegment>(segments[5]);
			Assert.False(propertiesSegment.IsDirectory);
			Assert.Equal("properties", propertiesSegment.Value);
		}

		/// <summary>
		///		Verify that template segments can be parsed from a URI with a query component.
		/// </summary>
		[Fact]
		public void Can_Parse_TemplateSegments_From_Uri_WithQuery()
		{
			IReadOnlyList<TemplateSegment> segments = TemplateSegment.Parse(
				"api/{controller}/{action}/{id?}/properties?propertyIds={propertyGroupIds}&diddly={dee?}&foo=bar"
			);

			Assert.Equal(9, segments.Count);
			Assert.IsAssignableFrom<RootUriSegment>(segments[0]);

			LiteralUriSegment apiSegment = Assert.IsAssignableFrom<LiteralUriSegment>(segments[1]);
			Assert.Equal("api", apiSegment.Value);

			ParameterizedUriSegment controllerSegment = Assert.IsAssignableFrom<ParameterizedUriSegment>(segments[2]);
			Assert.True(controllerSegment.IsDirectory);
			Assert.Equal("controller", controllerSegment.TemplateParameterName);
			Assert.False(controllerSegment.IsOptional);

			ParameterizedUriSegment actionSegment = Assert.IsAssignableFrom<ParameterizedUriSegment>(segments[3]);
			Assert.True(actionSegment.IsDirectory);
			Assert.Equal("action", actionSegment.TemplateParameterName);
			Assert.False(actionSegment.IsOptional);

			ParameterizedUriSegment idSegment = Assert.IsAssignableFrom<ParameterizedUriSegment>(segments[4]);
			Assert.True(idSegment.IsDirectory);
			Assert.Equal("id", idSegment.TemplateParameterName);
			Assert.True(idSegment.IsOptional);

			LiteralUriSegment propertiesSegment = Assert.IsAssignableFrom<LiteralUriSegment>(segments[5]);
			Assert.False(propertiesSegment.IsDirectory);
			Assert.Equal("properties", propertiesSegment.Value);

			ParameterizedQuerySegment propertyIdsSegment = Assert.IsAssignableFrom<ParameterizedQuerySegment>(segments[6]);
			Assert.Equal("propertyIds", propertyIdsSegment.QueryParameterName);
			Assert.Equal("propertyGroupIds", propertyIdsSegment.TemplateParameterName);
			Assert.False(propertyIdsSegment.IsOptional);

			ParameterizedQuerySegment diddlySegment = Assert.IsAssignableFrom<ParameterizedQuerySegment>(segments[7]);
			Assert.Equal("diddly", diddlySegment.QueryParameterName);
			Assert.Equal("dee", diddlySegment.TemplateParameterName);
			Assert.True(diddlySegment.IsOptional);

			LiteralQuerySegment fooSegment = Assert.IsAssignableFrom<LiteralQuerySegment>(segments[8]);
			Assert.Equal("foo", fooSegment.QueryParameterName);
			Assert.Equal("bar", fooSegment.QueryParameterValue);
		}

		/// <summary>
		///		Verify that template with a query component can be populated.
		/// </summary>
		[Fact]
		public void Can_Populate_Template_WithQuery()
		{
			UriTemplate template = new UriTemplate(
				"api/{controller}/{action}/{id?}/properties?propertyIds={propertyGroupIds}&diddly={dee?}&foo=bar"
			);

			Uri generatedUri = template.Populate(
				baseUri: new Uri("http://test-host/"), 
				templateParameters: new Dictionary<string, string>
				{
					{ "controller", "organizations" },
					{ "action", "distinct" },
					{ "propertyGroupIds", "System.OrganizationCommercial;EnterpriseMobility.OrganizationAirwatch" }
				}
			);

			Assert.Equal(
				"http://test-host/api/organizations/distinct/properties?propertyIds=System.OrganizationCommercial%3BEnterpriseMobility.OrganizationAirwatch&foo=bar",
				generatedUri.AbsoluteUri
			);
		}
	}
}
