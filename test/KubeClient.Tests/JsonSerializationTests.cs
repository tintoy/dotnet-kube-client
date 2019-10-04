using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
    using Models;
    using Models.Converters;
    using ResourceClients;
    using TestCommon;

    /// <summary>
    ///     Tests for JSON serialisation of various Kubernetes models.
    /// </summary>
    public class JsonSerializationTests
        : TestBase
    {
        /// <summary>
        ///     Create a new Kubernetes model serialisation test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public JsonSerializationTests(ITestOutputHelper testOutput)
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
        /// Verify that the casing of keys in <see cref="SecretV1.Data"/> are preserved.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        [InlineData("Abc")]
        [InlineData("ABC")]
        public void SecretV1_Data_PreserveKeyCase(string key)
        {
            var model = new SecretV1
            {
                Data =
                {
                    [key] = key
                }
            };

            JObject rootObject;
            using ( JTokenWriter writer = new JTokenWriter() )
            {
                JsonSerializer.Create(KubeResourceClient.SerializerSettings).Serialize(writer, model);
                writer.Flush();

                rootObject = (JObject) writer.Token;
            }

            Log.LogInformation("Serialized:\n{JSON:l}",
                rootObject.ToString(Formatting.Indented)
            );

            JObject data = rootObject.Value<JObject>("data");
            Assert.NotNull(data);

            Assert.Equal(key,
                data.Value<string>(key)
            );
        }

        /// <summary>
        /// Verify that the casing of keys in <see cref="ConfigMapV1.Data"/> are preserved.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        [InlineData("Abc")]
        [InlineData("ABC")]
        public void ConfigMapV1_Data_PreserveKeyCase(string key)
        {
            var model = new ConfigMapV1
            {
                Data =
                {
                    [key] = key
                }
            };

            JObject rootObject;
            using ( JTokenWriter writer = new JTokenWriter() )
            {
                JsonSerializer.Create(KubeResourceClient.SerializerSettings).Serialize(writer, model);
                writer.Flush();

                rootObject = (JObject) writer.Token;
            }

            Log.LogInformation("Serialized:\n{JSON:l}",
                rootObject.ToString(Formatting.Indented)
            );

            JObject data = rootObject.Value<JObject>("data");
            Assert.NotNull(data);

            Assert.Equal(key,
                data.Value<string>(key)
            );
        }

        /// <summary>
        /// Verify that an <see cref="Int32OrStringV1"/> with a <c>null</c> value deserialises correctly.
        /// </summary>
        [Fact]
        public void Int32OrStringWithNullDeserializesCorrectly()
        {
            string json = "{ \"path\": \"/healthz\", \"port\": null, \"scheme\": \"HTTP\" }";
            HTTPGetActionV1 httpGetAction = JsonConvert.DeserializeObject<HTTPGetActionV1>(json);
            
            Assert.NotNull(httpGetAction);
            Assert.Equal("/healthz", httpGetAction.Path);
            Assert.Null(httpGetAction.Port);
            Assert.Equal("HTTP", httpGetAction.Scheme);
        }

        /// <summary>
        /// Verify that an <see cref="Int32OrStringV1"/> with an integer value deserialises correctly.
        /// </summary>
        [Fact]
        public void Int32OrStringWithIntegerDeserializesCorrectly()
        {
            string json = "{ \"path\": \"/healthz\", \"port\": 10254, \"scheme\": \"HTTP\" }";
            HTTPGetActionV1 httpGetAction = JsonConvert.DeserializeObject<HTTPGetActionV1>(json);
            
            Assert.NotNull(httpGetAction);
            Assert.Equal("/healthz", httpGetAction.Path);
            Assert.True(httpGetAction.Port.IsInt32);
            Assert.False(httpGetAction.Port.IsString);
            Assert.Equal(10254, httpGetAction.Port.Int32Value);
            Assert.Equal("HTTP", httpGetAction.Scheme);
        }

        /// <summary>
        /// Verify that an <see cref="Int32OrStringV1"/> with a string value deserialises correctly.
        /// </summary>
        [Fact]
        public void Int32OrStringWithStringDeserializesCorrectly()
        {
            string json = "{ \"path\": \"/healthz\", \"port\": \"http\", \"scheme\": \"HTTP\" }";
            HTTPGetActionV1 httpGetAction = JsonConvert.DeserializeObject<HTTPGetActionV1>(json);
            
            Assert.NotNull(httpGetAction);
            Assert.False(httpGetAction.Port.IsInt32);
            Assert.True(httpGetAction.Port.IsString);
            Assert.Equal("http", httpGetAction.Port.StringValue);
            Assert.Equal("HTTP", httpGetAction.Scheme);
        }

        /// <summary>
        ///     Verify that an <see cref="Int32OrStringV1"/> can be correctly serialised to JSON (regardless of whether it's a number or string).
        /// </summary>
        [InlineData(null,       null)]
        [InlineData(567,        567)]
        [InlineData("567",      567)]
        [InlineData("567tcp",   "567tcp")]
        [InlineData("567 tcp",  "567 tcp")]
        [Theory(DisplayName = "Can serialise Int32OrStringV1 to JSON ")]
        public void Can_Serialize_Int32OrStringV1_Null(object rawValue, object renderedValue)
        {
            Int32OrStringV1 int32OrString;
            if (rawValue is string stringValue)
                int32OrString = stringValue;
            else if (rawValue is int intValue)
                int32OrString = intValue;
            else if (rawValue == null)
                int32OrString = null;
            else
                throw new InvalidOperationException($"Unexpected value type: '{rawValue.GetType().FullName}'.");

            var model = new TestModel
            {
                Number = 123,
                Text = "hello",
                Mixed = int32OrString
            };

            JObject rootObject;
            using (JTokenWriter writer = new JTokenWriter())
            {
                JsonSerializer
                    .Create(new JsonSerializerSettings
                    {
                        Converters =
                        {
                            new Int32OrStringV1Converter()
                        }
                    })
                    .Serialize(writer, model);

                writer.Flush();

                rootObject = (JObject)writer.Token;
            }
            
            string propertyName = GetJsonPropertyName(typeof(TestModel), "Mixed");
            var propertyValue = rootObject.Property(propertyName)?.Value as JValue;
            Assert.NotNull(propertyValue);
            Assert.Equal(renderedValue, propertyValue.Value);
        }

        /// <summary>
        ///     Verify that a <see cref="ResourceEventV1{TResource}"/> can be correctly deserialised from JSON.
        /// </summary>
        /// <param name="resourceType">The type of <see cref="KubeResourceV1"/> to be deserialised via <see cref="ResourceEventV1{TResource}.Resource"/>.</param>
        /// <param name="expectedEventType">The expected event type.</param>
        [InlineData(typeof(PodV1),  ResourceEventType.Added)]
        [InlineData(typeof(PodV1),  ResourceEventType.Deleted)]
        [InlineData(typeof(PodV1),  ResourceEventType.Modified)]
        [InlineData(null,           ResourceEventType.Error)]
        [Theory(DisplayName = "Can deserialise ResourceEventV1 ")]
        public void Can_Deserialize_ResourceEventV1(Type resourceType, ResourceEventType expectedEventType)
        {
            JsonSerializerSettings serializerSettings = KubeResourceClient.SerializerSettings;
            serializerSettings.Formatting = Formatting.Indented;

            JsonSerializer serializer = JsonSerializer.Create(serializerSettings);

            ResourceEventV1<KubeResourceV1> expectedResourceEvent = new ResourceEventV1<KubeResourceV1>
            {
                EventType = expectedEventType,
                Resource = resourceType != null ? (KubeResourceV1)Activator.CreateInstance(resourceType) : null
            };

            StringBuilder buffer = new StringBuilder();

            using (StringWriter bufferWriter = new StringWriter(buffer))
            using (JsonWriter jsonWriter = new JsonTextWriter(bufferWriter))
            {
                serializer.Serialize(jsonWriter, expectedResourceEvent);
                jsonWriter.Flush();
            }

            Log.LogInformation("Serialised event JSON:\n{EventJson:l}", buffer.ToString());

            serializerSettings.Converters.Add(
                new ResourceEventV1Converter(typeof(KubeResourceV1).Assembly)
            );
            serializer = JsonSerializer.Create(serializerSettings);

            ResourceEventV1<KubeResourceV1> actualResourceEvent;
            using (StringReader bufferReader = new StringReader(buffer.ToString()))
            using (JsonReader jsonReader = new JsonTextReader(bufferReader))
            {
                actualResourceEvent = serializer.Deserialize<ResourceEventV1<KubeResourceV1>>(jsonReader);
            }

            Assert.NotNull(actualResourceEvent);
            Assert.Equal(expectedResourceEvent.EventType, actualResourceEvent.EventType);
            
            if (expectedResourceEvent.Resource != null)
            {
                Assert.NotNull(actualResourceEvent.Resource);
                Assert.IsType(expectedResourceEvent.Resource.GetType(), actualResourceEvent.Resource);
            }
            else
                Assert.Null(actualResourceEvent.Resource);
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

        /// <summary>
        ///     Model used for serialisation tests.
        /// </summary>
        class TestModel
        {
            /// <summary>
            ///     A 32-bit integer.
            /// </summary>
            [JsonProperty("number")]
            public int Number { get; set; }
            
            /// <summary>
            ///     Some free-form text.
            /// </summary>
            [JsonProperty("text")]
            public string Text { get; set; }

            /// <summary>
            ///     Either a 32-bit integer or some free-form text.
            /// </summary>
            [JsonProperty("mixed")]
            public Int32OrStringV1 Mixed { get; set; }
        }
    }
}
