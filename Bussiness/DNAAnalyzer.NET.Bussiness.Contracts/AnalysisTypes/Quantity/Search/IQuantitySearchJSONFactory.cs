namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search
{
    public interface IQuantitySearchJSONFactory
    {
        string Type
        {
            get;
        }

        IQuantitySearchFactory QuantitySearchFactory
        {
            get;
            set;
        }

        IQuantitySearch CreateInstance(dynamic jsonConfig);
    }
}
