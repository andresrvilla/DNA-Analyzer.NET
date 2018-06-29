using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity
{
    public interface IQuantityAnalysisFactory
    {
        IQuantityAnalysisResultFactory QuantityAnalysisResultFactory
        {
            get;
            set;
        }

        IQuantityAnalysis CreateInstance(int? min, int? max, IEnumerable<IQuantitySearch> quantitySearchList);
    }
}
