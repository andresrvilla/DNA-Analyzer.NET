namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search
{
    public interface IQuantitySearchFactory
    {
        IQuantitySearch CreateInstance(string sequenceToFind);
    }
}
