using System;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
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
    }
}
