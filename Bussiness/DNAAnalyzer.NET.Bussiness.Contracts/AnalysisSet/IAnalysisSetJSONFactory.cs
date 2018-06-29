using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet
{
    public interface IAnalysisSetJSONFactory
    {
        IAnalysisSetFactory AnalysisSetFactory
        {
            get;
            set;
        }

        IAnalysisTypesFactory AnalysisTypesFactory
        {
            get;
            set;
        }

        IEnumerable<IAnalysisSet> CreateInstance(string json);
    }
}
