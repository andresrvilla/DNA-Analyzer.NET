using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DNAAnalyzer.NET.Services.Contracts;

namespace DNAAnalyzer.NET.Api.Controllers
{
    public class StatsController : ApiController
    {
        public StatsController(IDNAAnalyzerService dnaAnalyzerService)
        {
            this.DNAAnalyzerService = dnaAnalyzerService;
        }

        public IDNAAnalyzerService DNAAnalyzerService { get; }

        public async Task<HttpResponseMessage> Get()
        {
            return Request.CreateResponse(await this.DNAAnalyzerService.GetMutantsStats());
        }
    }
}
