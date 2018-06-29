namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result
{
    public interface IQuantityAnalysisResult : IAnalysisResult
    {
        int Min
        {
            get;
        }

        int Max
        {
            get;
        }

        bool Result
        {
            get;
        }
    }
}
