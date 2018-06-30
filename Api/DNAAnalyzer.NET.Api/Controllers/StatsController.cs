using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DNAAnalyzer.NET.Services.Contracts;
using log4net;

namespace DNAAnalyzer.NET.Api.Controllers
{
    public class StatsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public StatsController(IDNAAnalyzerService dnaAnalyzerService)
        {
            this.DNAAnalyzerService = dnaAnalyzerService;
        }

        public IDNAAnalyzerService DNAAnalyzerService { get; }

        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                Log.Debug("Request received");
                return Request.CreateResponse(await this.DNAAnalyzerService.GetMutantsStats());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
