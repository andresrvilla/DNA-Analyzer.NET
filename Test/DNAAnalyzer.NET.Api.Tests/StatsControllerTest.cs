using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DNAAnalyzer.NET.Api.Controllers;
using DNAAnalyzer.NET.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DNAAnalyzer.NET.Api.Tests
{
    [TestClass]
    public class StatsControllerTest
    {
        [TestMethod]
        public void ShouldConfigureAndReturnResponseProperly()
        {
            Mock<IDNAAnalyzerService> dnaAnalyzerService = new Mock<IDNAAnalyzerService>();
            ExpandoObject obj = new ExpandoObject();
            var objTask = Task.FromResult(obj);
            dnaAnalyzerService.Setup(m => m.GetMutantsStats()).Returns(objTask);

            StatsController controller = new StatsController(dnaAnalyzerService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());

            var responseMessage = controller.Get();
            Assert.AreEqual(responseMessage.Result.StatusCode, System.Net.HttpStatusCode.OK);
            ExpandoObject outObj;
            Assert.IsTrue(responseMessage.Result.TryGetContentValue(out outObj));
            Assert.AreEqual(outObj, obj);
        }
    }
}
