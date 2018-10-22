using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
    
    using Models;
    using TestCommon;

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

        /// <summary>
        ///     Verify that serialised models correctly include or omit required/optional empty collection properties.
        /// </summary>
        [InlineData(typeof(ObjectMetaV1), nameof(ObjectMetaV1.Annotations),  false)]
        [InlineData(typeof(ObjectMetaV1), nameof(ObjectMetaV1.Labels),       false)]
        [InlineData(typeof(PodSpecV1),    nameof(PodSpecV1.Containers),      true)]
        [InlineData(typeof(PodSpecV1),    nameof(PodSpecV1.HostAliases),     false)]
        [Theory(DisplayName = "Serialised models correctly include or omit required/optional empty collection properties ")]
        public void KubeObjectV1_Empty_CollectionProperty(Type modelType, string targetPropertyName, bool shouldBeSerialized)
        {
            object model = Activator.CreateInstance(modelType);

            JObject rootObject;
            using (JTokenWriter writer = new JTokenWriter())
            {
                new JsonSerializer().Serialize(writer, model);
                writer.Flush();

                rootObject = (JObject)writer.Token;
            }

            string expectedPropertyName = GetJsonPropertyName(modelType, targetPropertyName);
            string[] actualPropertyNames = rootObject.Properties().Select(property => property.Name).ToArray();

            if (shouldBeSerialized)
                Assert.Contains(expectedPropertyName, actualPropertyNames);
            else
                Assert.DoesNotContain(expectedPropertyName, actualPropertyNames);
        }

        /// <summary>
        ///     Get the JSON property name corresponding to the specified model property.
        /// </summary>
        /// <param name="modelType">
        ///     The model <see cref="Type"/>.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the target property.
        /// </param>
        /// <returns>
        ///     The JSON property name.
        /// </returns>
        static string GetJsonPropertyName(Type modelType, string propertyName)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));
            
            if (String.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'propertyName'.", nameof(propertyName));
            
            PropertyInfo targetProperty = modelType.GetProperty(propertyName);
            Assert.NotNull(targetProperty);

            JsonPropertyAttribute jsonPropertyAttribute = targetProperty.GetCustomAttribute<JsonPropertyAttribute>();
            Assert.NotNull(jsonPropertyAttribute);

            return jsonPropertyAttribute.PropertyName;
        }
    }
}
