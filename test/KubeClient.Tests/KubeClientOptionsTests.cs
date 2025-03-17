using KubeClient.TestCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
    /// <summary>
    ///     Tests for <see cref="KubeClientOptions"/>.
    /// </summary>
    public class KubeClientOptionsTests
        : TestBase
    {
        /// <summary>
        ///     Create a new <see cref="KubeClientOptions"/> test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     The output for the current test.
        /// </param>
        public KubeClientOptionsTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that <see cref="KubeClientOptions.LoggerFactory"/> is never null.
        /// </summary>
        [Fact(DisplayName = "LoggerFactory is never null")]
        public void Logger_Factory_Never_Null()
        {

        }
    }
}
