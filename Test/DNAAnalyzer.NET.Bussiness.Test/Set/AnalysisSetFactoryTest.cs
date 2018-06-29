using DNAAnalyzer.NET.Bussiness.Set;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNAAnalyzer.NET.Bussiness.Test.Set
{
    [TestClass]
    public class AnalysisSetFactoryTest
    {
        [TestMethod]
        public void AnalysisSetFactoryShouldReturnNewAnalysisSetObject()
        {
            AnalysisSetFactory analysisSetFactory = new AnalysisSetFactory();
            Assert.AreEqual(analysisSetFactory.CreateInstance("NAME", null).GetType().FullName, typeof(AnalysisSet).FullName);
        }
    }
}
