using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Set;
using DNAAnalyzer.NET.Models;
using DNAAnalyzer.NET.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unity;
using Unity.Resolution;

namespace DNAAnalyzer.NET.Bussiness.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MeliIntegrationTestShouldWorksWhenConfigurationIsCreatedFromJSON()
        {
            string json = @"
                {
                  'configuration': [
                    {
                      'type': 'analysisset',
                      'name': 'mutant',
                      'analyses': [
                        {
                          'type': 'quantity',
                          'min': 2,
                          'search': [
                            {
                              'type': 'alldirectionssequencequantity',
                              'sequence': 'AAAA'
                            },
                            {
                              'type': 'alldirectionssequencequantity',
                              'sequence': 'TTTT'
                            },
                            {
                              'type': 'alldirectionssequencequantity',
                              'sequence': 'CCCC'
                            },
                            {
                              'type': 'alldirectionssequencequantity',
                              'sequence': 'GGGG'
                            }
                          ]
                        }
                      ]
                    }
                  ]
                }
            ";

            /// Creo el Factory Principal, que va a crear la lista de analysis set
            IAnalysisSetFactory analysisSetFactory = new AnalysisSetFactory();

            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            Mock<IAnalysisResult> resultMock = new Mock<IAnalysisResult>();
            Mock<IUnityContainer> containerMock = new Mock<IUnityContainer>();
            containerMock.Setup(m => m.Resolve(typeof(IAnalysisResult), "quantityAnalysisResult", It.IsAny<ParameterOverride>(), It.IsAny<ParameterOverride>(), It.IsAny<ParameterOverride>())).Returns(resultMock.Object);
            containerMock.Setup(m => m.Resolve(typeof(IQuantitySearch), "allDirectionsSequenceQuantitySearch", It.IsAny<ParameterOverride>())).Returns(quantitySearchMock.Object);

            /// Creo el Factory de resultados de analisis de cantidad
            IQuantityAnalysisResultFactory quantityAnalysisResultFactory = new QuantityAnalysisResultFactory(containerMock.Object);
            IQuantityAnalysisFactory quantityAnalysisFactory = new QuantityAnalysisFactory(quantityAnalysisResultFactory);

            /// Creo la lista de busquedas por cantidad disponibles
            Dictionary<string, IQuantitySearchJSONFactory> availableSearchFactories = new Dictionary<string, IQuantitySearchJSONFactory>();
            
            /// Agrego la busqueda por todas las direcciones
            AllDirectionsSequenceQuantitySearchFactory allDirectionsSequenceQuantitySearchFactory = new AllDirectionsSequenceQuantitySearchFactory(containerMock.Object);
            IQuantitySearchJSONFactory allDirectionsQuantityJSONFactory = new QuantitySearchJSONFactory(allDirectionsSequenceQuantitySearchFactory, AllDirectionsSequenceQuantitySearch.ClassType);
            availableSearchFactories.Add(AllDirectionsSequenceQuantitySearch.ClassType, allDirectionsQuantityJSONFactory);

            /// Creo el Factory de busquedas por cantidad, con las busquedas por cantidad disponibles
            IQuantitySearchTypesFactory quantitySearchTypesFactory = new QuantitySearchTypesFactory(availableSearchFactories);
            QuantityAnalysisJSONFactory quantityAnalysisJSONFactory = new QuantityAnalysisJSONFactory(quantityAnalysisFactory, quantitySearchTypesFactory);

            Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories = new Dictionary<string, IAnalysisJSONFactory>();
            availableAnalysisFactories.Add(QuantityAnalysis.ClassKey, quantityAnalysisJSONFactory);

            IAnalysisTypesFactory analysisTypesFactory = new AnalysisTypesFactory(availableAnalysisFactories);

            AnalysisSetJSONFactory factory = new AnalysisSetJSONFactory(analysisSetFactory, analysisTypesFactory);

            IEnumerable<IAnalysisSet> analysisSets = factory.CreateInstance(json);

            Assert.IsTrue(analysisSets != null);

            foreach (var analysisSet in analysisSets)
            {
                {
                    IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();
                    string[] components = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
                    IDNA dna = new DNA(dnaConfiguration, components);
                    List<IAnalysisResult> results = new List<IAnalysisResult>(analysisSet.Analyze(dna));
                    Assert.AreEqual(resultMock.Object, results[0]);
                }
            }
        }

        private IDNAConfiguration GetDNAConfiguration()
        {
            IDNAConfiguration dnaConfiguration = new DNAConfiguration("[ACGT]+");
            return dnaConfiguration;
        }
    }
}
