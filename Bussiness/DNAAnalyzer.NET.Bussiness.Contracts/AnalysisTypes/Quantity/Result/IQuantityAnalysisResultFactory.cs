namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result
{
    public interface IQuantityAnalysisResultFactory
    {
        IAnalysisResult CreateInstance(int min, int max, bool result);
    }
}
