using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity.Result
{
    [TestClass]
    public class QuantityAnalysisResultTest
    {
        [TestMethod]
        public void ShouldReturnObjectWhenIsConfiguredProperly()
        {
            {
                QuantityAnalysisResult quantityAnalysisResult = new QuantityAnalysisResult(10, 20, false);
                Assert.AreEqual(quantityAnalysisResult.Min, 10);
                Assert.AreEqual(quantityAnalysisResult.Max, 20);
                Assert.AreEqual(quantityAnalysisResult.Result, false);
            }

            {
                QuantityAnalysisResult quantityAnalysisResult = new QuantityAnalysisResult(-10, 50, true);
                Assert.AreEqual(quantityAnalysisResult.Min, -10);
                Assert.AreEqual(quantityAnalysisResult.Max, 50);
                Assert.AreEqual(quantityAnalysisResult.Result, true);
            }
        }
    }
}
