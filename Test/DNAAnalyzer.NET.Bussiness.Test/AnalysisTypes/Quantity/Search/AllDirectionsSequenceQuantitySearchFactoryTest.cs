using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unity;
using Unity.Resolution;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity.Search
{
    [TestClass]
    public class AllDirectionsSequenceQuantitySearchFactoryTest
    {
        [TestMethod]
        public void ShouldReturnsObjectOnCreateInstance()
        {
            AllDirectionsSequenceQuantitySearch validSearch = new AllDirectionsSequenceQuantitySearch("SEQUENCE");
            AllDirectionsSequenceQuantitySearch invalidSearch = new AllDirectionsSequenceQuantitySearch("INVALID");
            Mock<IUnityContainer> containerMock = new Mock<IUnityContainer>();

            containerMock.Setup(m => m.Resolve(typeof(IQuantitySearch), "allDirectionsSequenceQuantitySearch", It.IsAny<ParameterOverride>())).Returns(invalidSearch);
            containerMock.Setup(m => m.Resolve(typeof(IQuantitySearch), "allDirectionsSequenceQuantitySearch", new ParameterOverride("sequenceToFind", "SEQUENCE"))).Returns(validSearch);

            AllDirectionsSequenceQuantitySearchFactory allDirectionsSequenceQuantitySearchFactory = new AllDirectionsSequenceQuantitySearchFactory(containerMock.Object);
            AllDirectionsSequenceQuantitySearch allDirectionsSequenceQuantitySearch = (AllDirectionsSequenceQuantitySearch)allDirectionsSequenceQuantitySearchFactory.CreateInstance("SEQUENCE");
            Assert.AreEqual(allDirectionsSequenceQuantitySearch.SequenceToFind, "SEQUENCE");
        }
    }
}
