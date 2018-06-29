using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Data.Contracts;
using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Services.Contracts
{
    public interface IDNAAnalyzerService
    {
        IMutantRepository MutantRepository { get; set; }

        IStatsRepository StatsRepository { get; set; }

        IEnumerable<IAnalysisResult> Analyze(string name, IDNA dna);

        Task<IQuantityAnalysisResult> AnalyzeMutant(IDNA dna);

        Task<ExpandoObject> GetMutantsStats();

        void Configure(string json);
    }
}