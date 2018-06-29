using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unity;
using Unity.Resolution;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity.Result
{
    [TestClass]
    public class QuantityAnalysisResultFactoryTest
    {
        [TestMethod]
        public void ShouldReturnObjectWhenIsConfiguredProperly()
        {
            QuantityAnalysisResult validQuantityAnalysisResult = new QuantityAnalysisResult(10, 20, false);
            QuantityAnalysisResult invalidQuantityAnalysisResult = new QuantityAnalysisResult(0, 0, true);
            Mock<IUnityContainer> containerMock = new Mock<IUnityContainer>();

            containerMock.Setup(m => m.Resolve(typeof(IAnalysisResult), "quantityAnalysisResult", It.IsAny<ParameterOverride>(), It.IsAny<ParameterOverride>(), It.IsAny<ParameterOverride>())).Returns(invalidQuantityAnalysisResult);
            containerMock.Setup(m => m.Resolve(typeof(IAnalysisResult), "quantityAnalysisResult", new ParameterOverride("min", 10), new ParameterOverride("max", 20), new ParameterOverride("result", false))).Returns(validQuantityAnalysisResult);

            QuantityAnalysisResultFactory quantityAnalysisResultFactory = new QuantityAnalysisResultFactory(containerMock.Object);
            QuantityAnalysisResult quantityAnalysisResult = (QuantityAnalysisResult)quantityAnalysisResultFactory.CreateInstance(10, 20, false);
            Assert.AreEqual(quantityAnalysisResult.Min, 10);
            Assert.AreEqual(quantityAnalysisResult.Max, 20);
            Assert.AreEqual(quantityAnalysisResult.Result, false);
        }
    }
}
