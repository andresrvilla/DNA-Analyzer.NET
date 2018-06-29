using System.Collections.Generic;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search
{
    public interface IQuantitySearchTypesFactory
    {
        Dictionary<string, IQuantitySearchJSONFactory> AvailableQuantityFactories
        {
            get;
            set;
        }

        IQuantitySearch CreateInstance(dynamic jsonConfig);
    }
}
