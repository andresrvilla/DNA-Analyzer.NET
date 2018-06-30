using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DNAAnalyzer.NET.Api.Request;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Models.Contracts;
using DNAAnalyzer.NET.Services.Contracts;
using log4net;

namespace DNAAnalyzer.NET.Api.Controllers
{
    public class MutantController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MutantController(IDNAAnalyzerService dnaAnalyzerService, IDNAFactory dnaFactory)
        {
            this.DNAAnalyzerService = dnaAnalyzerService;
            this.DNAFactory = dnaFactory;
        }

        public IDNAAnalyzerService DNAAnalyzerService { get; }

        public IDNAFactory DNAFactory { get; }

        public async Task<HttpResponseMessage> Post([FromBody]MutantRequest request)
        {
            try
            {
                Log.Debug("Request received");
                IDNA dna = this.DNAFactory.CreateInstance(request.Dna);
                IQuantityAnalysisResult result = await this.DNAAnalyzerService.AnalyzeMutant(dna);

                if (result.Result)
                {
                    return Request.CreateResponse();
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Is not a mutant");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
