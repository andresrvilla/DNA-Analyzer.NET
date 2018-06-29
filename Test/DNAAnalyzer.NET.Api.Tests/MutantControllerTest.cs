using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DNAAnalyzer.NET.Api.Controllers;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Models.Contracts;
using DNAAnalyzer.NET.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Api.Tests
{
    [TestClass]
    public class MutantControllerTest
    {
        [TestMethod]
        public void ShouldReturnProperlyOnConstructor()
        {
            Mock<IDNAAnalyzerService> dnaAnalyzerService = new Mock<IDNAAnalyzerService>();
            Mock<IDNAFactory> dnaFactory = new Mock<IDNAFactory>();
            MutantController controller = new MutantController(dnaAnalyzerService.Object, dnaFactory.Object);
            Assert.AreEqual(controller.DNAAnalyzerService, dnaAnalyzerService.Object);
            Assert.AreEqual(controller.DNAFactory, dnaFactory.Object);
        }

        [TestMethod]
        public void ShouldReturn200WhenAnalyzeMutantReturnsPositiveResult()
        {
            Mock<IQuantityAnalysisResult> quantityAnalysisResult = new Mock<IQuantityAnalysisResult>();
            quantityAnalysisResult.Setup(m => m.Result).Returns(true);
            Mock<IDNAAnalyzerService> dnaAnalyzerService = new Mock<IDNAAnalyzerService>();
            var quantityAnalysisResultTask = Task.FromResult(quantityAnalysisResult.Object);
            dnaAnalyzerService.Setup(m => m.AnalyzeMutant(It.IsAny<IDNA>())).Returns(quantityAnalysisResultTask);

            Mock<IDNAFactory> dnaFactory = new Mock<IDNAFactory>();
            MutantController controller = new MutantController(dnaAnalyzerService.Object, dnaFactory.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            var responseMessage = controller.Post(new Request.MutantRequest() { Dna = new string[] { } });
            Assert.AreEqual(responseMessage.Result.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public void ShouldReturn403WhenAnalyzeMutantReturnsPositiveResult()
        {
            Mock<IQuantityAnalysisResult> quantityAnalysisResult = new Mock<IQuantityAnalysisResult>();
            quantityAnalysisResult.Setup(m => m.Result).Returns(false);
            Mock<IDNAAnalyzerService> dnaAnalyzerService = new Mock<IDNAAnalyzerService>();
            var quantityAnalysisResultTask = Task.FromResult(quantityAnalysisResult.Object);
            dnaAnalyzerService.Setup(m => m.AnalyzeMutant(It.IsAny<IDNA>())).Returns(quantityAnalysisResultTask);

            Mock<IDNAFactory> dnaFactory = new Mock<IDNAFactory>();
            MutantController controller = new MutantController(dnaAnalyzerService.Object, dnaFactory.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            var responseMessage = controller.Post(new Request.MutantRequest() { Dna = new string[] { } });
            Assert.AreEqual(responseMessage.Result.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void ShouldReturn500WhenExceptionOcursOnService()
        {
            Mock<IQuantityAnalysisResult> quantityAnalysisResult = new Mock<IQuantityAnalysisResult>();
            quantityAnalysisResult.Setup(m => m.Result).Returns(false);
            Mock<IDNAAnalyzerService> dnaAnalyzerService = new Mock<IDNAAnalyzerService>();
            dnaAnalyzerService.Setup(m => m.AnalyzeMutant(It.IsAny<IDNA>())).Throws(new System.Exception());

            Mock<IDNAFactory> dnaFactory = new Mock<IDNAFactory>();
            MutantController controller = new MutantController(dnaAnalyzerService.Object, dnaFactory.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            var responseMessage = controller.Post(new Request.MutantRequest() { Dna = new string[] { } });
            Assert.AreEqual(responseMessage.Result.StatusCode, System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
