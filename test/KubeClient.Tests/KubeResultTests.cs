using System;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
    using Models;
    using TestCommon;

    /// <summary>
    ///     Tests for <see cref="KubeResultV1"/> implementations.
    /// </summary>
    public class KubeResultTests
        : TestBase
    {
        /// <summary>
        ///     Create a new <see cref="KubeResultTests"/> test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public KubeResultTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that a PodV1 resource result, when constructed from a PodV1, can be implicitly cast to a PodV1.
        /// </summary>
        [Fact(DisplayName = "PodV1 resource result from PodV1 can be cast to PodV1")]
        public void PodV1Result_From_PodV1_ImplicitCast_Resource()
        {
            PodV1 expected = new PodV1
            {
                Metadata = new ObjectMetaV1
                {
                    Name = "my-pod",
                    Namespace = "my-namespace"
                }
            };
            
            var result = new KubeResourceResultV1<PodV1>(expected);
            
            PodV1 actual = result;
            Assert.NotNull(actual);
            Assert.Same(expected, actual);
        }

        /// <summary>
        ///     Verify that a PodV1 resource result, when constructed from a failing StatusV1 throws a KubeApiException when implicitly cast to a PodV1.
        /// </summary>
        [Fact(DisplayName = "PodV1 resource result from failing StatusV1 throws when cast to PodV1")]
        public void PodV1Result_From_FailureStatus_ImplicitCast_Resource()
        {
            PodV1 pod = null;

            KubeApiException exception = Assert.Throws<KubeApiException>(() =>
            {
                pod = new KubeResourceResultV1<PodV1>(new StatusV1
                {
                    Status = "Failure",
                    Message = "Most definitely not a success."
                });
            });
            Assert.Null(pod);
            Assert.True(exception.HasStatus);
            Assert.Equal("Failure", exception.Status.Status);
            Assert.Equal("Most definitely not a success.", exception.Status.Message);
        }

        /// <summary>
        ///     Verify that a PodV1 resource result, when constructed from a successful StatusV1, can be implicitly cast to a StatusV1.
        /// </summary>
        [Fact(DisplayName = "PodV1 resource result from successful StatusV1 can be cast to PodV1")]
        public void PodV1Result_From_SuccessStatus_ImplicitCast_Resource()
        {
            var result = new KubeResourceResultV1<PodV1>(new StatusV1
            {
                Status = "Success",
                Message = "Most definitely a success."
            });

            PodV1 pod = result;
            Assert.Null(pod);
        }

        /// <summary>
        ///     Verify that a PodV1 resource result, when constructed from a failing StatusV1, can be implicitly cast to a <see cref="StatusV1"/>.
        /// </summary>
        [Fact(DisplayName = "PodV1 resource result from failing StatusV1 can be cast to StatusV1")]
        public void PodV1Result_From_FailureStatus_ImplicitCast_Status()
        {
            var result = new KubeResourceResultV1<PodV1>(new StatusV1
            {
                Status = "Failure",
                Message = "Most definitely not a success."
            });

            StatusV1 status = result;
            Assert.NotNull(status);
            Assert.Equal("Failure", status.Status);
            Assert.Equal("Most definitely not a success.", status.Message);
        }
    }
}
