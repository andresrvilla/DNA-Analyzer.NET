using System;
using DNAAnalyzer.NET.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Models.Test
{
    [TestClass]
    public class DNATest
    {
        [TestMethod]
        public void DNAShouldHaveTheConstructorParameterComponents()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            string[] components = new string[] { "AA", "BB" };
            DNA dna = new DNA(dnaConfigMock.Object, components);
            Assert.AreEqual(dna.Components.Length, components.Length);
            Assert.AreEqual(dna.Components[0], components[0]);
            Assert.AreEqual(dna.Components[1], components[1]);
        }

        [TestMethod]
        public void DNAIsValidShouldReturnTrueWhenConfigurationAndComponentsAreValid()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            string[] components = new string[] { "AAAA", "CCCC", "TTTT", "GGGG" };
            DNA dna = new DNA(dnaConfigMock.Object, components);
            Assert.IsTrue(dna.IsValid());
        }

        [TestMethod]
        public void DNAIsValidShouldReturnFalseWhenComponentsDontMatchWhenPattern()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            string[] components = new string[] { "QQQQ", "CCCC", "TTTT", "GGGG" };
            DNA dna = new DNA(dnaConfigMock.Object, components);
            Assert.IsFalse(dna.IsValid());
        }

        [TestMethod]
        public void DNAIsValidShouldReturnFalseWhenComponentsIsNotSquare()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            string[] components = new string[] { "QQQQ", "CCCC", "TTTT" };
            DNA dna = new DNA(dnaConfigMock.Object, components);
            Assert.IsFalse(dna.IsValid());
        }

        [TestMethod]
        public void DNAIsValidShouldReturnFalseWhenComponentsIsNull()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            string[] components = null;
            DNA dna = new DNA(dnaConfigMock.Object, components);
            Assert.IsFalse(dna.IsValid());
        }

        [TestMethod]
        public void DNAStringRepresentationShouldReturnCorrectValue()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            string[] components = new string[] { "ACTG", "GTCA", "AACC", "TTGG" };
            DNA dna = new DNA(dnaConfigMock.Object, components);
            Assert.AreEqual(dna.StringRepresentation(), "ACTG-GTCA-AACC-TTGG");
        }

        [TestMethod]
        public void DNAStringRepresentationShouldReturnEmptyWhenComponentsAreNull()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            string[] components = null;
            DNA dna = new DNA(dnaConfigMock.Object, components);
            Assert.AreEqual(dna.StringRepresentation(), string.Empty);
        }
    }
}
