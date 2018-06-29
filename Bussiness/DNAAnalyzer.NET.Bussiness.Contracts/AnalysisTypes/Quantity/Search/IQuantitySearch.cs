using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search
{
    public interface IQuantitySearch
    {
        string Type
        {
            get;
        }

        int Search(IDNA dna);
    }
}
