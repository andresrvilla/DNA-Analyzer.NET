using System;
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using DNAAnalyzer.NET.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity
{
    [TestClass]
    public class QuantityAnalysisTest
    {
        [TestMethod]
        public void ShouldConfigurePropertiesProperlyOnConstrutor()
        {
            int min = 100;
            int max = 50;
            Mock<IQuantityAnalysisResultFactory> quantityAnalysisResultFactoryMock = new Mock<IQuantityAnalysisResultFactory>();
            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            List<IQuantitySearch> listOfQuantitySearch = new List<IQuantitySearch>()
            {
                quantitySearchMock.Object
            };

            QuantityAnalysis quantityAnalysis = new QuantityAnalysis(min, max, quantityAnalysisResultFactoryMock.Object, listOfQuantitySearch);
            Assert.AreEqual(min, quantityAnalysis.Min);
            Assert.AreEqual(max, quantityAnalysis.Max);
            Assert.AreEqual(listOfQuantitySearch, quantityAnalysis.QuantitySearchList);
            Assert.AreEqual(QuantityAnalysis.ClassKey, quantityAnalysis.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDNAException))]
        public void ShouldThrowExceptionWhenDNAIsInvalid()
        {
            int min = 100;
            int max = 50;
            Mock<IQuantityAnalysisResultFactory> quantityAnalysisResultFactoryMock = new Mock<IQuantityAnalysisResultFactory>();
            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            List<IQuantitySearch> listOfQuantitySearch = new List<IQuantitySearch>()
            {
                quantitySearchMock.Object
            };

            QuantityAnalysis quantityAnalysis = new QuantityAnalysis(min, max, quantityAnalysisResultFactoryMock.Object, listOfQuantitySearch);

            Mock<IDNA> dnaMock = new Mock<IDNA>();
            dnaMock.Setup(m => m.IsValid()).Returns(false);
            quantityAnalysis.Analyze(dnaMock.Object);
        }

        [TestMethod]
        public void ShouldReturnResultOnValidAnalyzeCall()
        {
            int min = 100;
            int max = 50;
            Mock<IAnalysisResult> analysisResultMock = new Mock<IAnalysisResult>();

            Mock<IQuantityAnalysisResultFactory> quantityAnalysisResultFactoryMock = new Mock<IQuantityAnalysisResultFactory>();
            quantityAnalysisResultFactoryMock.Setup(m => m.CreateInstance(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(analysisResultMock.Object);
            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            quantitySearchMock.Setup(m => m.Search(It.IsAny<IDNA>())).Returns(100);
            List<IQuantitySearch> listOfQuantitySearch = new List<IQuantitySearch>()
            {
                quantitySearchMock.Object
            };

            QuantityAnalysis quantityAnalysis = new QuantityAnalysis(min, max, quantityAnalysisResultFactoryMock.Object, listOfQuantitySearch);

            Mock<IDNA> dnaMock = new Mock<IDNA>();
            dnaMock.Setup(m => m.IsValid()).Returns(true);
            IAnalysisResult analysisResult = quantityAnalysis.Analyze(dnaMock.Object);
            Assert.AreEqual(analysisResult, analysisResultMock.Object);
        }

        [TestMethod]
        public void ShouldConfigureMinAndMaxDefaultValuesOnConstructorIfParametersAreNull()
        {
            QuantityAnalysis quantityAnalysis = new QuantityAnalysis(null, null, null, null);
            Assert.AreEqual(quantityAnalysis.Min, int.MinValue);
            Assert.AreEqual(quantityAnalysis.Max, int.MaxValue);
        }
    }
}
