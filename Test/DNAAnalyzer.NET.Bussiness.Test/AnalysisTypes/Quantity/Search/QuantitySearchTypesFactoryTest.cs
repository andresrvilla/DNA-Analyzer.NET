using System.Collections.Generic;
using System.Dynamic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNAAnalyzer.NET.Bussiness.Test.AnalysisTypes.Quantity.Search
{
    [TestClass]
    public class QuantitySearchTypesFactoryTest
    {
        [TestMethod]
        public void ShouldConfigureObjectProperly()
        {
            Dictionary<string, IQuantitySearchJSONFactory> availableQuantityFactories = new Dictionary<string, IQuantitySearchJSONFactory>();
            QuantitySearchTypesFactory quantitySearchTypesFactory = new QuantitySearchTypesFactory(availableQuantityFactories);
            Assert.AreEqual(quantitySearchTypesFactory.AvailableQuantityFactories, availableQuantityFactories);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownTypeException))]
        public void ShouldThrowExceptionIfQuantitySearchJSONFactoryDoesNotExists()
        {
            Dictionary<string, IQuantitySearchJSONFactory> availableQuantityFactories = new Dictionary<string, IQuantitySearchJSONFactory>();
            QuantitySearchTypesFactory quantitySearchTypesFactory = new QuantitySearchTypesFactory(availableQuantityFactories);

            dynamic json = new ExpandoObject();
            json.type = "anything";
            quantitySearchTypesFactory.CreateInstance(json);
        }
    }
}
