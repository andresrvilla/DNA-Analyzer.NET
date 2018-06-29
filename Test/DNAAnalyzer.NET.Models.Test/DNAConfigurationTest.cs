using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNAAnalyzer.NET.Models.Test
{
    [TestClass]
    public class DNAConfigurationTest
    {
        [TestMethod]
        public void DNAConfigurationShouldConfigurePatternProperly()
        {
            DNAConfiguration dnaConfiguration = new DNAConfiguration("PATTERN");
            Assert.AreEqual(dnaConfiguration.ComponentsPattern, "PATTERN");
        }

        [TestMethod]
        public void DNAConfigurationShouldConfigureEmptyPatternOnParameterlessConstructor()
        {
            DNAConfiguration dnaConfiguration = new DNAConfiguration();
            Assert.AreEqual(dnaConfiguration.ComponentsPattern, string.Empty);
        }
    }
}
