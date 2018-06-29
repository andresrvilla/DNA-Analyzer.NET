namespace DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes
{
    public interface IAnalysisJSONFactory
    {
        string Type
        {
            get;
            set;
        }

        IAnalysis CreateInstance(dynamic jsonConfig);
    }
}
