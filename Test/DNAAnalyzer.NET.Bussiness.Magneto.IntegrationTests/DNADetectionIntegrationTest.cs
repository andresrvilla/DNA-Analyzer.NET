using System;
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using DNAAnalyzer.NET.Models;
using DNAAnalyzer.NET.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Unity;
using Unity.Resolution;

namespace DNAAnalyzer.NET.Bussiness.Magneto.IntegrationTests
{
    [TestClass]
    public class DNADetectionIntegrationTest
    {
        [TestMethod]
        public void PositiveIsMutantIntegrationTestsShouldReturnTrueEach()
        {
            IQuantityAnalysis quantityAnalysis = this.GetQuantityAnalysis();
            IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();

            {
                /// Positive result from MELI Instructions
                string[] components = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturn6x6TrueAscendentVerticalCase
                string[] components = new string[] { "ATGAGA", "CAATGC", "TAATTT", "AGACTG", "GCGTCA", "TCTCTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturn6x6TrueAscendentVerticalCase
                string[] components = new string[] { "ATGAGA", "CAATGC", "TAATTT", "AGACTG", "GCGTCA", "TCTCTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6DescendentVerticalCase
                string[] components = new string[] { "ATGAGA", "CATGGC", "TTATGT", "ATACTG", "GCTTCA", "TCTTTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6DescendentAndAscendantVerticalCase
                string[] components = new string[] { "ATGTGA", "CATGGC", "TTATGT", "TTACTG", "GCATCA", "TCTTTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue7X7DescendentAndAscendantVerticalCase
                string[] components = new string[] { "ATGTGAA", "CATGGCC", "TTATGTT", "TTACTGG", "GCATCAA", "TCTTTGG", "ATCCATA" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6HorizontalAndDescendentVerticalCase
                string[] components = new string[] { "ATGCGA", "CAGTAC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6VerticalAndDescendentVerticalCase
                string[] components = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CACCTA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6HorizontalAndAscendantVerticalCase
                string[] components = new string[] { "ATGAGA", "CAATAC", "TATTGT", "AGAGAG", "CCCCTA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6VerticalAndAscendantVerticalCase
                string[] components = new string[] { "CTGCGA", "CCGAGC", "TTATGT", "TAATGG", "AACCTA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6VerticalOnLimits
                string[] components = new string[] { "CTGCAG", "CCGACG", "CTATTG", "CCATGG", "AACCTA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }

            {
                /// IsMutantShouldReturnTrue6x6HorizontalOnLimits
                string[] components = new string[] { "CCCCAG", "CCGACC", "TTATTT", "TCATGG", "AACCTA", "TCAAAA" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsTrue(result.Result);
            }
        }

        [TestMethod]
        public void NegativeIsMutantIntegrationTestsShouldReturnFalseEach()
        {
            IQuantityAnalysis quantityAnalysis = this.GetQuantityAnalysis();
            IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();

            {
                /// Negative result from MELI Instructions
                string[] components = new string[] { "ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsFalse(result.Result);
            }

            {
                /// IsMutantShouldReturnFalse6x6VerticalOnlyCase
                string[] components = new string[] { "CTGCGA", "CCGAGC", "TTATGT", "TCATGG", "AACCTA", "TCACTG" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
                Assert.IsFalse(result.Result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDNAException))]
        public void IsMutantIntegrationTestsShouldThrowExceptionWhenDNAComponentsIsNull()
        {
            IQuantityAnalysis quantityAnalysis = this.GetQuantityAnalysis();
            IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();

            {
                /// Negative result from MELI Instructions
                string[] components = null;
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDNAException))]
        public void IsMutantIntegrationTestsShouldThrowExceptionWhenDNAComponentsHaveInvalidSizeCaseOne()
        {
            IQuantityAnalysis quantityAnalysis = this.GetQuantityAnalysis();
            IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();

            {
                /// Negative result from MELI Instructions
                string[] components = new string[] { "XXX", "XX", "XXX" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDNAException))]
        public void IsMutantIntegrationTestsShouldThrowExceptionWhenDNAComponentsHaveInvalidSizeCaseTwo()
        {
            IQuantityAnalysis quantityAnalysis = this.GetQuantityAnalysis();
            IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();

            {
                /// Negative result from MELI Instructions
                string[] components = new string[] { "XX", "XXX", "XXX" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDNAException))]
        public void IsMutantIntegrationTestsShouldThrowExceptionWhenDNAComponentsHaveInvalidSizeCaseThree()
        {
            IQuantityAnalysis quantityAnalysis = this.GetQuantityAnalysis();
            IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();

            {
                /// Negative result from MELI Instructions
                string[] components = new string[] { "XXX", "XXX", "XX" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDNAException))]
        public void IsMutantIntegrationTestsShouldThrowExceptionWhenDNAComponentsHaveInvalidSizeCaseFour()
        {
            IQuantityAnalysis quantityAnalysis = this.GetQuantityAnalysis();
            IDNAConfiguration dnaConfiguration = this.GetDNAConfiguration();

            {
                /// Negative result from MELI Instructions
                string[] components = new string[] { "XXX", "XXX" };
                IDNA dna = new DNA(dnaConfiguration, components);
                IQuantityAnalysisResult result = (IQuantityAnalysisResult)quantityAnalysis.Analyze(dna);
            }
        }

        private IQuantityAnalysis GetQuantityAnalysis()
        {
            int minMutantTest = 2;
            int maxMutantTest = int.MaxValue;

            /*
             * Para la prueba de integración necesito que devuelva objetos reales, por lo que voy a tener que crear instancias
             * de las clases de resultado
             */
            Mock<IQuantitySearch> quantitySearchMock = new Mock<IQuantitySearch>();
            QuantityAnalysisResult positiveQuantityAnalysisResult = new QuantityAnalysisResult(0, 1, true);
            QuantityAnalysisResult negativeQuantityAnalysisResult = new QuantityAnalysisResult(0, 1, false);
            Mock<IUnityContainer> containerMock = new Mock<IUnityContainer>();

            containerMock.Setup(m => m.Resolve(typeof(IAnalysisResult), "quantityAnalysisResult", It.IsAny<ParameterOverride>(), It.IsAny<ParameterOverride>(), new ParameterOverride("result", true))).Returns(positiveQuantityAnalysisResult);
            containerMock.Setup(m => m.Resolve(typeof(IAnalysisResult), "quantityAnalysisResult", It.IsAny<ParameterOverride>(), It.IsAny<ParameterOverride>(), new ParameterOverride("result", false))).Returns(negativeQuantityAnalysisResult);

            IQuantityAnalysisResultFactory quantityAnalysisResultFactory = new QuantityAnalysisResultFactory(containerMock.Object);

            List<IQuantitySearch> quantitySearchList = new List<IQuantitySearch>()
            {
                new AllDirectionsSequenceQuantitySearch("AAAA"),
                new AllDirectionsSequenceQuantitySearch("TTTT"),
                new AllDirectionsSequenceQuantitySearch("CCCC"),
                new AllDirectionsSequenceQuantitySearch("GGGG")
            };

            IQuantityAnalysis quantityAnalysis = new QuantityAnalysis(minMutantTest, maxMutantTest, quantityAnalysisResultFactory, quantitySearchList);

            return quantityAnalysis;
        }

        private IDNAConfiguration GetDNAConfiguration()
        {
            IDNAConfiguration dnaConfiguration = new DNAConfiguration("[ACGT]+");
            return dnaConfiguration;
        }
    }
}
