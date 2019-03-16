using System;
using System.Linq;
using System.Reflection;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
    using Models;
    using Models.Converters;
    using TestCommon;

    /// <summary>
    ///     Tests for YAML serialisation of various Kubernetes models.
    /// </summary>
    public class YamlSerializationTests
        : TestBase
    {
        /// <summary>
        ///     Create a new Kubernetes model serialisation test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public YamlSerializationTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that an <see cref="Int32OrStringV1"/> can be correctly serialised to YAML (regardless of whether it's a number or string).
        /// </summary>
        [InlineData(null, null)]
        [InlineData(567, "567")]
        [InlineData("567", "567")]
        [InlineData("567tcp", "567tcp")]
        [InlineData("567 tcp", "567 tcp")]
        [Theory(DisplayName = "Can serialise Int32OrStringV1 to YAML")]
        public void Can_Serialize_Int32OrStringV1_Null(object rawValue, string renderedValue)
        {
            Serializer serializer = CreateSerializer();

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

            string expected = "number: 123\ntext: hello\n";
            if (renderedValue != null)
                expected += $"mixed: {renderedValue}\n";

            expected = NormalizeLineEndings(expected);

            string actual = serializer.Serialize(model);
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     Normalise line-endings to match the local environment.
        /// </summary>
        /// <param name="text">The text to normalise.</param>
        /// <returns>The text with line-endings updated to match <see cref="Environment.NewLine"/>.</returns>
        static string NormalizeLineEndings(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            
            return text.Replace("\n", Environment.NewLine);
        }

        /// <summary>
        ///     Create a YAML <see cref="Serializer"/> for use in tests.
        /// </summary>
        /// <returns>
        ///     The configured <see cref="Serializer"/>.
        /// </returns>
        static Serializer CreateSerializer() => new SerializerBuilder()
                .WithTypeConverter(
                    new Int32OrStringV1Converter()
                )
                .Build();

        /// <summary>
        ///     Model used for serialisation tests.
        /// </summary>
        class TestModel
        {
            /// <summary>
            ///     A 32-bit integer.
            /// </summary>
            [YamlMember(Alias = "number")]
            public int Number { get; set; }

            /// <summary>
            ///     Some free-form text.
            /// </summary>
            [YamlMember(Alias = "text")]
            public string Text { get; set; }

            /// <summary>
            ///     Either a 32-bit integer or some free-form text.
            /// </summary>
            [YamlMember(Alias = "mixed")]
            public Int32OrStringV1 Mixed { get; set; }
        }
    }
}
