using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

using BCX509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace KubeClient.Extensions.KubeConfig.Tests
{
    using TestCommon;

    /// <summary>
    ///     Tests for <see cref="CryptoHelper"/>.
    /// </summary>
    public class CryptoHelperTests
        : TestBase
    {
        /// <summary>
        ///     Create a new <see cref="CryptoHelper"/> test-suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public CryptoHelperTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that <see cref="CryptoHelper"/> can enumerate the contents of PEM blocks.
        /// </summary>
        /// <param name="pemBaseName">
        ///     The base name for the test's PEM files.
        /// </param>
        /// <param name="pemPassword">
        ///     The password for the test's PEM files.
        /// </param>
        /// <param name="expectedCertificateThumbprints">
        ///     The thumbprints of certificates that are expected to be found in the test's PEM files.
        /// </param>
        /// <param name="expectedKeyCount">
        ///     The number of asymmetric crypto key-pairs that are expected to be contained in the test's PEM files.
        /// </param>
        [Theory(DisplayName = "Can enumerate certificate PEM contents ")]
        [InlineData("test-cert-01", "simple_password", new string[] { "5374FA8B5EF2B321059442791FE3CF091BB175BF" }, 1)]
        public void CanEnumerateCertificatePemContents(string pemBaseName, string pemPassword, string[] expectedCertificateThumbprints, int expectedKeyCount)
        {
            string certPemFilePath = Path.Combine("Certificates", $"{pemBaseName}.pem");
            string certPemData = File.ReadAllText(certPemFilePath);
            
            string keyPemFilePath = Path.Combine("Certificates", $"{pemBaseName}-key.pem");
            string keyPemData = File.ReadAllText(keyPemFilePath);

            HashSet<string> remainingCertificateThumbprints = new HashSet<string>(expectedCertificateThumbprints);
            int remainingKeyCount = expectedKeyCount;

            foreach (object pemObject in CryptoHelper.EnumeratePemObjects(pemPassword, certPemData, keyPemData))
            {
                if (pemObject is BCX509Certificate certificate)
                {
                    string thumbprint = CryptoHelper.GetThumbprint(certificate);
                    TestOutput.WriteLine($"Found certificate in PEM data: '{thumbprint}'");

                    remainingCertificateThumbprints.Remove(thumbprint);
                }
                else if (pemObject is AsymmetricCipherKeyPair keyPair)
                {
                    TestOutput.WriteLine($"Found asymmetric key-pair in PEM data: Public='{keyPair.Public}', Private='{keyPair.Private}'");

                    --remainingKeyCount;
                }
                else
                    throw new Exception($"Unexpected PEM object: '{pemObject}'");
            }

            Assert.Empty(remainingCertificateThumbprints);
            Assert.Equal(0, remainingKeyCount);
        }
    }
}
