using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Models
{
    public class DNAConfiguration : IDNAConfiguration
    {        
        public DNAConfiguration()
        {
            this.ComponentsPattern = string.Empty;
        }

        public DNAConfiguration(string componentsPattern)
        {
            this.ComponentsPattern = componentsPattern;
        }

        public string ComponentsPattern
        {
            get;
            set;
        }
    }
}
