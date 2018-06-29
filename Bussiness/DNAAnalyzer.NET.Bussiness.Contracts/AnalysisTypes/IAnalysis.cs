using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes
{
    public interface IAnalysis
    {
        string Type
        {
            get;
        }

        IAnalysisResult Analyze(IDNA dna);
    }
}
