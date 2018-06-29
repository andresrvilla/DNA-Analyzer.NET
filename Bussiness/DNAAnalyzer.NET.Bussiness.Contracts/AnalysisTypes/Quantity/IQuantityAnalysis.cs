using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity
{
    public interface IQuantityAnalysis : IAnalysis
    {
        int Min
        {
            get;
            set;
        }

        int Max
        {
            get;
            set;
        }

        IEnumerable<IQuantitySearch> QuantitySearchList
        {
            get;
            set;
        }
    }
}
