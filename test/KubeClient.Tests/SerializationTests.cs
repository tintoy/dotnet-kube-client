using System;
using Xunit;

namespace KubeClient.Tests
{
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using TestCommon;
    using Xunit.Abstractions;

    /// <summary>
    ///     Tests for serialisation of various Kubernetes models.
    /// </summary>
    public class SerializationTests
        : TestBase
    {
        /// <summary>
        ///     Create a new Kubernetes model serialisation test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public SerializationTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that KubeObjectV1 correctly populates Kind and ApiVersion when constructed.
        /// </summary>
        [InlineData(typeof(PodV1),                  JTokenType.Object)]
        [InlineData(typeof(PodListV1),              JTokenType.Object)]
        [InlineData(typeof(DeploymentListV1Beta1),  JTokenType.Object)]
        [InlineData(typeof(DeploymentV1Beta1),      JTokenType.Object)]
        [Theory(DisplayName = "Serialised KubeObjectV1 and inheritors have correct root token type ")]
        public void KubeObject_JTokenType(Type kubeObjectType, JTokenType expectedTokenType)
        {
            KubeObjectV1 kubeObject = (KubeObjectV1)Activator.CreateInstance(kubeObjectType);

            JToken rootToken;
            using (JTokenWriter writer = new JTokenWriter())
            {
                new JsonSerializer().Serialize(writer, kubeObject);
                writer.Flush();

                rootToken = writer.Token;
            }
            
            Assert.NotNull(rootToken);
            Assert.Equal(rootToken.Type, expectedTokenType);
        }
    }
}
