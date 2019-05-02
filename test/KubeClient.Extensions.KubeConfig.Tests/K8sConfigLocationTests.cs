using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using KubeClient.TestCommon;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Extensions.KubeConfig.Tests
{
    //TODO: A better pattern here would probably be to use https://github.com/AArnott/Xunit.SkippableFact so tests for the wrong OS get marked as Inconclusive?
    // Not sure how CI is set up and if that would cause build failures
    public class K8sConfigLocationTests : TestBase
    {
        const string UserProfile = @"C:\Users\me";
        const string HomeDrive = @"Z:";
        const string HomePath = @"\";
        const string Home = @"G:\";

        [Fact]
        public void LinuxUsesHomeEnvironmentVariable()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Environment.SetEnvironmentVariable("USERPROFILE", UserProfile);
                var home = Environment.GetEnvironmentVariable("HOME");
                var config = K8sConfig.Locate();
                Assert.Equal(Path.Combine(home, ".kube", "config"), config);
            }
        }

        [Fact]
        public void WindowsUsesHomeFirst()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Environment.SetEnvironmentVariable("USERPROFILE", UserProfile);
                Environment.SetEnvironmentVariable("HOMEDRIVE", HomeDrive);
                Environment.SetEnvironmentVariable("HOMEPATH", HomePath);
                Environment.SetEnvironmentVariable("HOME", Home);
                var config = K8sConfig.Locate();
                Assert.Equal(Path.Combine(Home, ".kube", "config"), config);
            }
        }

        [Fact]
        public void WindowsUsesHomeDriveAndPathBeforeUserProfile()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Environment.SetEnvironmentVariable("HOME", String.Empty);

                Environment.SetEnvironmentVariable("USERPROFILE", UserProfile);
                Environment.SetEnvironmentVariable("HOMEDRIVE", HomeDrive);
                Environment.SetEnvironmentVariable("HOMEPATH", HomePath);
                var config = K8sConfig.Locate();
                Assert.Equal(Path.Combine(HomeDrive + HomePath, ".kube", "config"), config);
            }
        }

        [Fact]
        public void WindowsUsesUserProfileAsLastOption()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Environment.SetEnvironmentVariable("HOME", String.Empty);
                Environment.SetEnvironmentVariable("HOMEDRIVE", String.Empty);
                Environment.SetEnvironmentVariable("HOMEPATH", String.Empty);

                Environment.SetEnvironmentVariable("USERPROFILE", UserProfile);
                var config = K8sConfig.Locate();
                Assert.Equal(Path.Combine(UserProfile, ".kube", "config"), config);
            }
        }

        public K8sConfigLocationTests(ITestOutputHelper testOutput) : base(testOutput)
        {
        }
    }
}
