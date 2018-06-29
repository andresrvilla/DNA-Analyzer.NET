using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.Set;
using DNAAnalyzer.NET.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.Set
{
    [TestClass]
    public class AnalysisSetTest
    {
        [TestMethod]
        public void AnalysisSetConstructorShouldConfigureNameAndAnalysesList()
        {
            IEnumerable<IAnalysis> analyses = new List<IAnalysis>();
            AnalysisSet analysisSet = new AnalysisSet("CONFIGUREDNAME", analyses);
            Assert.AreEqual(analysisSet.Name, "CONFIGUREDNAME");
            Assert.AreEqual(analysisSet.ConfiguredAnalyses, analyses);
        }

        [TestMethod]
        public void AnalysisSetShouldReturnAnalysisResultObjectOnEveryConfiguredAnalysis()
        {
            List<IAnalysis> analyses = new List<IAnalysis>();
            Mock<IDNA> mockedDNA = new Mock<IDNA>();
            Mock<IAnalysisResult> mockedResult = new Mock<IAnalysisResult>();

            for (var i = 0; i < 10; i++)
            {
                Mock<IAnalysis> mock = new Mock<IAnalysis>();
                mock.Setup(t => t.Analyze(mockedDNA.Object)).Returns(mockedResult.Object);
                analyses.Add(mock.Object);
            }

            AnalysisSet analysisSet = new AnalysisSet("CONFIGUREDNAME", analyses);

            foreach (var analysisResult in analysisSet.Analyze(mockedDNA.Object))
            {
                Assert.AreEqual(mockedResult.Object, analysisResult);
            }
        }
    }
}
