using System.Collections.Generic;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes
{
    public interface IAnalysisTypesFactory
    {
        Dictionary<string, IAnalysisJSONFactory> AvailableAnalysisFactories
        {
            get;
            set;
        }

        IAnalysis CreateInstance(dynamic jsonConfig);
    }
}
