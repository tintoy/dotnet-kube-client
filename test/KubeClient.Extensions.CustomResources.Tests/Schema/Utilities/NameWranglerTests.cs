using KubeClient.Extensions.CustomResources.Schema.Utilities;

namespace KubeClient.Extensions.CustomResources.Tests.Schema.Utilities
{
    public class NameWranglerTests
    {
        [Theory]
        [InlineData("v1", "V1")]
        [InlineData("v1beta1", "V1Beta1")]
        [InlineData("v12beta34", "V12Beta34")]
        [InlineData("v12etaBetaPi34", "V12EtaBetaPi34")]
        [InlineData("v12EtaBetaPi34", "V12EtaBetaPi34")]
        [InlineData("v12etabetaPi34", "V12EtabetaPi34")]
        public void Can_Capitalize_Name(string name, string expected)
        {
            string actual = NameWrangler.CapitalizeName(name);

            Assert.Equal(expected, actual);
        }
    }
}