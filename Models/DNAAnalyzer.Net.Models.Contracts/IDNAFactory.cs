namespace DNAAnalyzer.NET.Models.Contracts
{
    public interface IDNAFactory
    {
        IDNAConfiguration DNAConfiguration
        {
            get;
            set;
        }

        IDNA CreateInstance(string[] components);
    }
}
