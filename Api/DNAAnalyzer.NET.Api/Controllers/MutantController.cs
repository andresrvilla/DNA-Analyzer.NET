using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DNAAnalyzer.NET.Api.Request;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Models.Contracts;
using DNAAnalyzer.NET.Services.Contracts;

namespace DNAAnalyzer.NET.Api.Controllers
{
    public class MutantController : ApiController
    {
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
