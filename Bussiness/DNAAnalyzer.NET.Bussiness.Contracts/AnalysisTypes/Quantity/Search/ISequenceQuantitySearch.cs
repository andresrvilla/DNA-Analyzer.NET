namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search
{
    public interface ISequenceQuantitySearch : IQuantitySearch
    {
        string SequenceToFind
        {
            get;
            set;
        }
    }
}
