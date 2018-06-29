using System.Collections.Generic;
using System.Dynamic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes
{
    [TestClass]
    public class AnalysisTypesFactoryTest
    {
        [TestMethod]
        public void ConstructorShouldConfigurePropertiesProperly()
        {
            Mock<IAnalysisJSONFactory> analysisJSONFactoryMock = new Mock<IAnalysisJSONFactory>();
            Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories = new Dictionary<string, IAnalysisJSONFactory>();
            availableAnalysisFactories.Add("key1", analysisJSONFactoryMock.Object);
            AnalysisTypesFactory analysisTypesFactory = new AnalysisTypesFactory(availableAnalysisFactories);
            Assert.AreEqual(analysisTypesFactory.AvailableAnalysisFactories, availableAnalysisFactories);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownTypeException))]
        public void CreateInstanceShouldThrowExceptionWhenParameterIsNull()
        {
            Mock<IAnalysisJSONFactory> analysisJSONFactoryMock = new Mock<IAnalysisJSONFactory>();
            Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories = new Dictionary<string, IAnalysisJSONFactory>();
            availableAnalysisFactories.Add("key1", analysisJSONFactoryMock.Object);
            AnalysisTypesFactory analysisTypesFactory = new AnalysisTypesFactory(availableAnalysisFactories);

            analysisTypesFactory.CreateInstance(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownTypeException))]
        public void CreateInstanceShouldThrowExceptionWhenParameterDoesNotHaveTypeProperty()
        {
            Mock<IAnalysisJSONFactory> analysisJSONFactoryMock = new Mock<IAnalysisJSONFactory>();
            Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories = new Dictionary<string, IAnalysisJSONFactory>();
            availableAnalysisFactories.Add("key1", analysisJSONFactoryMock.Object);
            AnalysisTypesFactory analysisTypesFactory = new AnalysisTypesFactory(availableAnalysisFactories);

            dynamic parameter = new ExpandoObject();
            analysisTypesFactory.CreateInstance(parameter);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownTypeException))]
        public void CreateInstanceShouldThrowExceptionWhenParameterHaveInvalidType()
        {
            Mock<IAnalysisJSONFactory> analysisJSONFactoryMock = new Mock<IAnalysisJSONFactory>();
            Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories = new Dictionary<string, IAnalysisJSONFactory>();
            availableAnalysisFactories.Add("key1", analysisJSONFactoryMock.Object);
            AnalysisTypesFactory analysisTypesFactory = new AnalysisTypesFactory(availableAnalysisFactories);

            dynamic parameter = new ExpandoObject();
            parameter.type = "unknown";

            analysisTypesFactory.CreateInstance(parameter);
        }

        [TestMethod]
        public void CreateInstanceShouldReturnIAnalysisObjectWhenParameterIsConfiguredProperly()
        {
            Mock<IAnalysis> analysisMock = new Mock<IAnalysis>();

            Mock<IAnalysisJSONFactory> analysisJSONFactoryMock = new Mock<IAnalysisJSONFactory>();
            analysisJSONFactoryMock.Setup(m => m.CreateInstance(It.IsAny<ExpandoObject>())).Returns(analysisMock.Object);
            Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories = new Dictionary<string, IAnalysisJSONFactory>();
            availableAnalysisFactories.Add("key1", analysisJSONFactoryMock.Object);
            AnalysisTypesFactory analysisTypesFactory = new AnalysisTypesFactory(availableAnalysisFactories);

            dynamic parameter = new ExpandoObject();
            parameter.type = "key1";

            IAnalysis analysis = analysisTypesFactory.CreateInstance(parameter);
            Assert.AreEqual(analysis, analysisMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingRequiredDependencyException))]
        public void CreateInstanceShouldThrowExceptionIfFactoryIsNotConfiguredProperly()
        {
            AnalysisTypesFactory analysisTypesFactory = new AnalysisTypesFactory();
            analysisTypesFactory.CreateInstance(new ExpandoObject());
        }
    }
}
