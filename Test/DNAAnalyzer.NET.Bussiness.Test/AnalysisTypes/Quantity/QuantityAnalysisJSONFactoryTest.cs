using System.Collections.Generic;
using System.Dynamic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity
{
    [TestClass]
    public class QuantityAnalysisJSONFactoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(MissingRequiredDependencyException))]
        public void ShouldThrowExceptionOnCreateInstanceWhenIsNotConfiguredProperly()
        {
            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory();
            quantityAnalysisJSONFactory.CreateInstance(new ExpandoObject());
        }

        [TestMethod]
        public void ConstructorShouldConfigureObjectProperly()
        {
            Mock<IQuantityAnalysisFactory> quantityAnalysisFactoryMock = new Mock<IQuantityAnalysisFactory>();
            Mock<IQuantitySearchTypesFactory> quantitySearchTypesFactoryMock = new Mock<IQuantitySearchTypesFactory>();
            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory(quantityAnalysisFactoryMock.Object, quantitySearchTypesFactoryMock.Object);

            Assert.AreEqual(quantityAnalysisJSONFactory.QuantityAnalysisFactory, quantityAnalysisFactoryMock.Object);
            Assert.AreEqual(quantityAnalysisJSONFactory.QuantitySearchTypesFactory, quantitySearchTypesFactoryMock.Object);
            Assert.AreEqual(quantityAnalysisJSONFactory.Type, QuantityAnalysisJSONFactory.ClassKey);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void ShouldThrowExceptionInCreateInstanceWhenDynamicDontContainSearchKey()
        {
            Mock<IQuantityAnalysisFactory> quantityAnalysisFactoryMock = new Mock<IQuantityAnalysisFactory>();
            Mock<IQuantitySearchTypesFactory> quantitySearchTypesFactoryMock = new Mock<IQuantitySearchTypesFactory>();
            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory(quantityAnalysisFactoryMock.Object, quantitySearchTypesFactoryMock.Object);

            dynamic json = new ExpandoObject();
            quantityAnalysisJSONFactory.CreateInstance(json);
        }

        [TestMethod]
        public void ShouldReturnProperObjectWithDynamicMinimumConfig()
        {
            Mock<IQuantityAnalysis> quantityAnalysis = new Mock<IQuantityAnalysis>();
            Mock<IQuantityAnalysisFactory> quantityAnalysisFactoryMock = new Mock<IQuantityAnalysisFactory>();
            quantityAnalysisFactoryMock.Setup(m => m.CreateInstance(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<IEnumerable<IQuantitySearch>>())).Returns(quantityAnalysis.Object);

            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            Mock<IQuantitySearchTypesFactory> quantitySearchTypesFactoryMock = new Mock<IQuantitySearchTypesFactory>();
            quantitySearchTypesFactoryMock.Setup(m => m.CreateInstance(It.IsAny<ExpandoObject>())).Returns(quantitySearchMock.Object);

            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory(quantityAnalysisFactoryMock.Object, quantitySearchTypesFactoryMock.Object);

            dynamic json = new ExpandoObject();
            json.search = new List<string>();
            Assert.AreEqual(quantityAnalysisJSONFactory.CreateInstance(json), quantityAnalysis.Object);
        }

        [TestMethod]
        public void ShouldReturnProperObjectWithDynamicWithTwoSearchs()
        {
            Mock<IQuantityAnalysis> quantityAnalysis = new Mock<IQuantityAnalysis>();
            Mock<IQuantityAnalysisFactory> quantityAnalysisFactoryMock = new Mock<IQuantityAnalysisFactory>();
            quantityAnalysisFactoryMock.Setup(m => m.CreateInstance(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<IEnumerable<IQuantitySearch>>())).Returns(quantityAnalysis.Object);

            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            Mock<IQuantitySearchTypesFactory> quantitySearchTypesFactoryMock = new Mock<IQuantitySearchTypesFactory>();
            quantitySearchTypesFactoryMock.Setup(m => m.CreateInstance(It.IsAny<ExpandoObject>())).Returns(quantitySearchMock.Object);

            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory(quantityAnalysisFactoryMock.Object, quantitySearchTypesFactoryMock.Object);

            dynamic json = new ExpandoObject();
            json.search = new List<string>();
            json.search.Add("value1");
            json.search.Add("value2");
            Assert.AreEqual(quantityAnalysisJSONFactory.CreateInstance(json), quantityAnalysis.Object);
        }

        [TestMethod]
        public void ShouldReturnProperObjectWithDynamicWithMinValue()
        {
            Mock<IQuantityAnalysis> quantityAnalysis = new Mock<IQuantityAnalysis>();
            Mock<IQuantityAnalysisFactory> quantityAnalysisFactoryMock = new Mock<IQuantityAnalysisFactory>();
            quantityAnalysisFactoryMock.Setup(m => m.CreateInstance(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<IEnumerable<IQuantitySearch>>())).Returns(quantityAnalysis.Object);

            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            Mock<IQuantitySearchTypesFactory> quantitySearchTypesFactoryMock = new Mock<IQuantitySearchTypesFactory>();
            quantitySearchTypesFactoryMock.Setup(m => m.CreateInstance(It.IsAny<ExpandoObject>())).Returns(quantitySearchMock.Object);

            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory(quantityAnalysisFactoryMock.Object, quantitySearchTypesFactoryMock.Object);

            dynamic json = new ExpandoObject();
            json.search = new List<string>();
            json.min = 0;
            Assert.AreEqual(quantityAnalysisJSONFactory.CreateInstance(json), quantityAnalysis.Object);
        }

        [TestMethod]
        public void ShouldReturnProperObjectWithDynamicWithMaxValue()
        {
            Mock<IQuantityAnalysis> quantityAnalysis = new Mock<IQuantityAnalysis>();
            Mock<IQuantityAnalysisFactory> quantityAnalysisFactoryMock = new Mock<IQuantityAnalysisFactory>();
            quantityAnalysisFactoryMock.Setup(m => m.CreateInstance(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<IEnumerable<IQuantitySearch>>())).Returns(quantityAnalysis.Object);

            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            Mock<IQuantitySearchTypesFactory> quantitySearchTypesFactoryMock = new Mock<IQuantitySearchTypesFactory>();
            quantitySearchTypesFactoryMock.Setup(m => m.CreateInstance(It.IsAny<ExpandoObject>())).Returns(quantitySearchMock.Object);

            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory(quantityAnalysisFactoryMock.Object, quantitySearchTypesFactoryMock.Object);

            dynamic json = new ExpandoObject();
            json.search = new List<string>();
            json.max = 0;
            Assert.AreEqual(quantityAnalysisJSONFactory.CreateInstance(json), quantityAnalysis.Object);
        }
    }
}
