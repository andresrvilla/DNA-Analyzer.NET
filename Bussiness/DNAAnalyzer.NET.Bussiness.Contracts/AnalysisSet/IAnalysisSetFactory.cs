using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet
{
    public interface IAnalysisSetFactory
    {
        IAnalysisSet CreateInstance(string name, IEnumerable<IAnalysis> analyses);
    }
}
