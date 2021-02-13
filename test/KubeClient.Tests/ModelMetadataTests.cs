using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
    using KubeClient.ApiMetadata;
    using Models;
    using TestCommon;

    /// <summary>
    ///     Tests for the <see cref="ModelMetadata"/> base class.
    /// </summary>
    public class ModelMetadataTests
        : TestBase
    {
        /// <summary>
        ///     Create a new <see cref="ModelMetadata"/> test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public ModelMetadataTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that the typed ModelMetadata API correctly indicates that ComponentStatusV1.Conditions is a merge property.
        /// </summary>
        [Fact(DisplayName = "ComponentStatusV1.Conditions is a Merge property (typed)")]
        public void IsMergeProperty_ComponentStatusV1_Conditions_Typed()
        {
            bool isMergeProperty = ModelMetadata.StrategicPatch.For<ComponentStatusV1>.IsMergeStrategy(status => status.Conditions);
            Assert.True(isMergeProperty);
        }

        /// <summary>
        ///     Verify that the untyped ModelMetadata API correctly indicates that ComponentStatusV1.Conditions is a merge property.
        /// </summary>
        [Fact(DisplayName = "ComponentStatusV1.Conditions is a Merge property (untyped)")]
        public void IsMergeProperty_ComponentStatusV1_Conditions_Untyped()
        {
            bool isMergeProperty = ModelMetadata.StrategicPatch.IsMergeStrategy(
                typeof(ComponentStatusV1).GetProperty("Conditions")
            );
            Assert.True(isMergeProperty);
        }

        /// <summary>
        ///     Verify that API metadata can be statically discovered from resource models.
        /// </summary>
        [Fact(DisplayName = "Discover static API metadata from resource models")]
        public void Discover_ApiMetadata_Static_From_ResourceModels()
        {
            KubeApiMetadataCache metadataCache = new KubeApiMetadataCache();
            metadataCache.LoadFromMetadata(
                typeof(PodV1).Assembly,
                clearExisting: true
            );
        }

        /// <summary>
        ///     Verify that API metadata can be statically discovered from a write-only resource model.
        /// </summary>
        [Fact(DisplayName = "Discover static API metadata from write-only resource model")]
        public void Discover_ApiMetadata_Static_From_ResourceModels_WriteOnly()
        {
            KubeApiMetadataCache metadataCache = new KubeApiMetadataCache();
            metadataCache.LoadFromMetadata(
                typeof(TokenReviewV1).Assembly,
                clearExisting: true
            );

            KubeApiMetadata apiMetadata = metadataCache.Get<TokenReviewV1>();
            Assert.NotNull(apiMetadata);
            Assert.Equal(1, apiMetadata.PathMetadata.Count);

            KubeApiPathMetadata pathMetadata = apiMetadata.PathMetadata[0];
            Assert.False(pathMetadata.IsNamespaced);
            Assert.Equal("apis/authentication.k8s.io/v1/tokenreviews", pathMetadata.Path);
            Assert.Equal(1, pathMetadata.Verbs.Count);
            Assert.Equal("create",
                pathMetadata.Verbs.First()
            );
        }
    }
}
