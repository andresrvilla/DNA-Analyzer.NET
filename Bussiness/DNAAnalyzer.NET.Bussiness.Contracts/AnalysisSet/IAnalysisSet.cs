using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet
{
    public interface IAnalysisSet
    {
        string Name
        {
            get;
            set;
        }

        IEnumerable<IAnalysis> ConfiguredAnalyses
        {
            get;
            set;
        }

        IEnumerable<IAnalysisResult> Analyze(IDNA dna);
    }
}
