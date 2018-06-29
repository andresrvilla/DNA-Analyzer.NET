namespace DNAAnalyzer.NET.Models.Contracts
{
    public interface IDNA
    {
        string[] Components
        {
            get;
            set;
        }

        bool IsValid();

        string StringRepresentation();
    }
}
