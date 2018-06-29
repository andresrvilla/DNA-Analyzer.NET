using System;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity.Search
{
    [TestClass]
    public class AllDirectionsSequenceQuantitySearchTest
    {
        [TestMethod]
        public void ShouldConfigurePropertiesProperlyOnConstructor()
        {
            AllDirectionsSequenceQuantitySearch allDirectionsSequenceQuantitySearch = new AllDirectionsSequenceQuantitySearch("SEQUENCE");
            Assert.AreEqual(allDirectionsSequenceQuantitySearch.Type, AllDirectionsSequenceQuantitySearch.ClassType);
            Assert.AreEqual(allDirectionsSequenceQuantitySearch.SequenceToFind, "SEQUENCE");
        }

        [TestMethod]
        public void ShouldReturnCorrectNumberOfSequencesWhenSearch()
        {
            {
                Mock<IDNA> dnaMock = new Mock<IDNA>();
                dnaMock.Setup(m => m.Components).Returns(new string[] { "NOTHING", "NOTHING", "NOTHING" });
                AllDirectionsSequenceQuantitySearch allDirectionsSequenceQuantitySearch = new AllDirectionsSequenceQuantitySearch("SEQUENCE");
                Assert.AreEqual(allDirectionsSequenceQuantitySearch.Search(dnaMock.Object), 0);
            }

            {
                Mock<IDNA> dnaMock = new Mock<IDNA>();
                dnaMock.Setup(m => m.Components).Returns(new string[] { "SEQUENCE", "NOTHING", "NOTHING" });
                AllDirectionsSequenceQuantitySearch allDirectionsSequenceQuantitySearch = new AllDirectionsSequenceQuantitySearch("SEQUENCE");
                Assert.AreEqual(allDirectionsSequenceQuantitySearch.Search(dnaMock.Object), 1);
            }

            {
                Mock<IDNA> dnaMock = new Mock<IDNA>();
                dnaMock.Setup(m => m.Components).Returns(new string[] { "SEQUENCE", "NOTHING", "SEQUENCE" });
                AllDirectionsSequenceQuantitySearch allDirectionsSequenceQuantitySearch = new AllDirectionsSequenceQuantitySearch("SEQUENCE");
                Assert.AreEqual(allDirectionsSequenceQuantitySearch.Search(dnaMock.Object), 2);
            }

            {
                Mock<IDNA> dnaMock = new Mock<IDNA>();
                dnaMock.Setup(m => m.Components).Returns(new string[] { "NOTHING", "SEQUENCE", "SEQUENCE" });
                AllDirectionsSequenceQuantitySearch allDirectionsSequenceQuantitySearch = new AllDirectionsSequenceQuantitySearch("SEQUENCE");
                Assert.AreEqual(allDirectionsSequenceQuantitySearch.Search(dnaMock.Object), 2);
            }

            {
                Mock<IDNA> dnaMock = new Mock<IDNA>();
                dnaMock.Setup(m => m.Components).Returns(new string[] { "SEQUENCE", "SEQUENCE", "SEQUENCE" });
                AllDirectionsSequenceQuantitySearch allDirectionsSequenceQuantitySearch = new AllDirectionsSequenceQuantitySearch("SEQUENCE");
                Assert.AreEqual(allDirectionsSequenceQuantitySearch.Search(dnaMock.Object), 3);
            }
        }
    }
}
