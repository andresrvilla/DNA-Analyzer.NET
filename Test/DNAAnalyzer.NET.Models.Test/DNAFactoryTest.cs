using DNAAnalyzer.NET.Exceptions;
using DNAAnalyzer.NET.Models.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Models.Test
{
    [TestClass]
    public class DNAFactoryTest
    {
        [TestMethod]
        public void DNAFactoryTestCreateInstanceShouldReturnObject()
        {
            Mock<IDNAConfiguration> dnaConfigMock = new Mock<IDNAConfiguration>();
            dnaConfigMock.Setup(m => m.ComponentsPattern).Returns("[ACTG]+");
            DNAFactory factory = new DNAFactory();
            factory.DNAConfiguration = dnaConfigMock.Object;
            Assert.AreEqual(factory.CreateInstance(null).GetType(), typeof(DNA));
        }

        [TestMethod]
        [ExpectedException(typeof(MissingRequiredDependencyException))]
        public void DNAFactoryTestCreateInstanceShouldThrowExceptionWhenIsNotConfiguredProperly()
        {
            DNAFactory factory = new DNAFactory();
            factory.CreateInstance(null);
        }
    }
}
