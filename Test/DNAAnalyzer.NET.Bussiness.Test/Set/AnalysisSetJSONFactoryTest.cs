using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.Set;
using DNAAnalyzer.NET.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.Set
{
    [TestClass]
    public class AnalysisSetJSONFactoryTest
    {
        [TestMethod]
        public void AnalysisSetJSONFactoryConstructorShouldConfigureProperlyRequiredFactories()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            Assert.AreEqual(analysisSetJSONFactory.AnalysisSetFactory, analysisSetFactoryMock.Object);
            Assert.AreEqual(analysisSetJSONFactory.AnalysisTypesFactory, analysisTypesFactoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONStringIsNull()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONStringIsEmpty()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONisEmpty()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONisInvalid()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONHaveConfigurationButIsNotAnList()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{'configuration':{}}");
        }

        [TestMethod]
        public void AnalysisSetJSONFactoryCreateInstanceShouldReturnEmptyCreatedListWhenJSONHaveEmptyConfigurationNode()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            List<IAnalysisSet> listOfCreatedSets = new List<IAnalysisSet>(analysisSetJSONFactory.CreateInstance("{'configuration':[]}"));
            Assert.AreEqual(listOfCreatedSets.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONHaveConfigurationWithChildObjectsWithoutType()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{'configuration':[{},{}]}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONHaveConfigurationWithChildObjectsWithEmptyTypes()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{'configuration':[{'type':''},{'type':''}]}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONHaveConfigurationWithChildObjectsWithInvalidTypes()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{'configuration':[{'type':'invalid'},{'type':'moreinvalid'}]}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONHaveCorrectConfigurationTypesButWithoutAnalysesProperty()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{'configuration':[{'type':'analysisset'},{'type':'analysisset'}]}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONHaveCorrectConfigurationTypesButWithAnalysesAsObject()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();
            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            analysisSetJSONFactory.CreateInstance("{'configuration':[{'type':'analysisset','analyses':{}},{'type':'analysisset','analyses':{}}]}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void AnalysisSetJSONFactoryCreateInstanceShouldThrowExceptionWhenJSONHaveCorrectTypesButWithoutName()
        {
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();
            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();

            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            List<IAnalysisSet> listOfCreatedSets = new List<IAnalysisSet>(analysisSetJSONFactory.CreateInstance("{'configuration':[{'type':'analysisset','analyses':[]},{'type':'analysisset','name':'name2','analyses':[]}]}"));
        }

        [TestMethod]
        public void AnalysisSetJSONFactoryCreateInstanceShouldReturnCreatedListWithAnalysesWhenJSONHaveCorrectTypes()
        {
            Mock<IAnalysis> analysisMock = new Mock<IAnalysis>();
            Mock<IAnalysisSetFactory> analysisSetFactoryMock = new Mock<IAnalysisSetFactory>();

            Mock<IAnalysisSet> analysisSetMock = new Mock<IAnalysisSet>();
            analysisSetMock.Setup(m => m.ConfiguredAnalyses).Returns(new List<IAnalysis>() { analysisMock.Object });
            analysisSetMock.Setup(m => m.Name).Returns("SETNAME");

            analysisSetFactoryMock.Setup(m => m.CreateInstance(It.IsAny<string>(), It.IsAny<IEnumerable<IAnalysis>>())).Returns(analysisSetMock.Object);

            Mock<IAnalysisTypesFactory> analysisTypesFactoryMock = new Mock<IAnalysisTypesFactory>();

            AnalysisSetJSONFactory analysisSetJSONFactory = new AnalysisSetJSONFactory(analysisSetFactoryMock.Object, analysisTypesFactoryMock.Object);
            List<IAnalysisSet> listOfCreatedSets = new List<IAnalysisSet>(analysisSetJSONFactory.CreateInstance("{'configuration':[{'type':'analysisset','name':'name1','analyses':[{},{}]},{'type':'analysisset','name':'name2','analyses':[{},{}]}]}"));
            Assert.AreEqual(listOfCreatedSets.Count, 2);
            foreach (var item in listOfCreatedSets)
            {
                Assert.AreEqual(item, analysisSetMock.Object);
                List<IAnalysis> analises = new List<IAnalysis>(item.ConfiguredAnalyses);
                foreach (var analysis in analises)
                {
                    Assert.AreEqual(analysis, analysisMock.Object);
                }
            }
        }
    }
}
