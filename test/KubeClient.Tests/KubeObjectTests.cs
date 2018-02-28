using System;
using Xunit;

namespace KubeClient.Tests
{
    using Models;

    /// <summary>
    ///     Tests for the <see cref="KubeObjectV1"/> base class.
    /// </summary>
    public class KubeObjectTests
    {
        /// <summary>
        ///     Verify that KubeObjectV1 correctly populates Kind and ApiVersion when constructed.
        /// </summary>
        [InlineData(typeof(DeleteOptionsV1), "DeleteOptions", "v1")]
        [InlineData(typeof(DeploymentV1Beta1), "Deployment", "apps/v1beta1")]
        [Theory(DisplayName = "KubeObjectV1 correctly populates Kind and ApiVersion ")]
        public void KubeObjectV1_Kind_Success(Type kubeObjectType, string expectedKind, string expectedApiVersion)
        {
            KubeObjectV1 kubeObject = (KubeObjectV1)Activator.CreateInstance(kubeObjectType);
            Assert.Equal(expectedKind, kubeObject.Kind);
            Assert.Equal(expectedApiVersion, kubeObject.ApiVersion);
        }
    }
}
