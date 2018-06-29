using System;
using System.Dynamic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity.Search
{
    [TestClass]
    public class QuantitySearchJSONFactoryTest
    {
        [TestMethod]
        public void ShouldConfigureProperlyOnConstructor()
        {
            Mock<IQuantitySearchFactory> quantitySearchFactoryMock = new Mock<IQuantitySearchFactory>();
            QuantitySearchJSONFactory quantitySearchJSONFactory = new QuantitySearchJSONFactory(quantitySearchFactoryMock.Object, "alldirectionssequencequantity");
            Assert.AreEqual(quantitySearchJSONFactory.QuantitySearchFactory, quantitySearchFactoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingRequiredDependencyException))]
        public void CreateInstanceShouldThrowExceptionWhenIsNotConfiguredTypeProperly()
        {
            Mock<IQuantitySearchFactory> quantitySearchFactoryMock = new Mock<IQuantitySearchFactory>();
            QuantitySearchJSONFactory quantitySearchJSONFactory = new QuantitySearchJSONFactory(quantitySearchFactoryMock.Object, string.Empty);
            quantitySearchJSONFactory.CreateInstance(new ExpandoObject());
        }

        [TestMethod]
        [ExpectedException(typeof(MissingRequiredDependencyException))]
        public void CreateInstanceShouldThrowExceptionWhenIsNotConfiguredSearchFactoryProperly()
        {
            QuantitySearchJSONFactory quantitySearchJSONFactory = new QuantitySearchJSONFactory(null, "alldirectionssequencequantity");
            quantitySearchJSONFactory.CreateInstance(new ExpandoObject());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidJSONException))]
        public void CreateInstanceShouldThrowExceptionWhenDynamicDoesNotHaveSequenceProperty()
        {
            Mock<IQuantitySearchFactory> quantitySearchFactoryMock = new Mock<IQuantitySearchFactory>();
            QuantitySearchJSONFactory quantitySearchJSONFactory = new QuantitySearchJSONFactory(quantitySearchFactoryMock.Object, "alldirectionssequencequantity");
            quantitySearchJSONFactory.CreateInstance(new ExpandoObject());
        }

        [TestMethod]
        public void ShouldReturnObject()
        {
            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            Mock<IQuantitySearchFactory> quantitySearchFactoryMock = new Mock<IQuantitySearchFactory>();
            quantitySearchFactoryMock.Setup(m => m.CreateInstance(It.IsAny<string>())).Returns(quantitySearchMock.Object);
            QuantitySearchJSONFactory quantitySearchJSONFactory = new QuantitySearchJSONFactory(quantitySearchFactoryMock.Object, "alldirectionssequencequantity");
            dynamic obj = new ExpandoObject();
            obj.sequence = "SEQUENCE";
            Assert.AreEqual(quantitySearchJSONFactory.CreateInstance(obj), quantitySearchMock.Object);
        }
    }
}
