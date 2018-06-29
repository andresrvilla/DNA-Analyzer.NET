using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity
{
    [TestClass]
    public class QuantityAnalysisFactoryTest
    {
        [TestMethod]
        public void ShouldConfigurePropertiesProperlyOnConstructorAndReturnsObjectOnCreateInstance()
        {
            Mock<IQuantityAnalysisResultFactory> quantityAnalysisResultFactoryMock = new Mock<IQuantityAnalysisResultFactory>();
            QuantityAnalysisFactory quantityAnalysisFactory = new QuantityAnalysisFactory(quantityAnalysisResultFactoryMock.Object);

            int min = 100;
            int max = 200;

            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            List<IQuantitySearch> quantitySearchList = new List<IQuantitySearch>() { quantitySearchMock.Object };

            IQuantityAnalysis quantityAnalysis = quantityAnalysisFactory.CreateInstance(min, max, quantitySearchList);
            Assert.AreEqual(quantityAnalysis.QuantitySearchList, quantitySearchList);
            Assert.AreEqual(quantityAnalysis.Min, min);
            Assert.AreEqual(quantityAnalysis.Max, max);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingRequiredDependencyException))]
        public void ShouldThrowExceptionWhenIsNotConfiguredProperly()
        {
            QuantityAnalysisFactory quantityAnalysisFactory = new QuantityAnalysisFactory();
            quantityAnalysisFactory.CreateInstance(null, null, null);
        }
    }
}
