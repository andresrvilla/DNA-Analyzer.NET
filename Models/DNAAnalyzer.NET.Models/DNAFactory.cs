using DNAAnalyzer.NET.Exceptions;
using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Models
{
    public class DNAFactory : IDNAFactory
    {
        public IDNAConfiguration DNAConfiguration
        {
            get;
            set;
        }

        public IDNA CreateInstance(string[] components)
        {
            if (this.DNAConfiguration == null)
            {
                throw new MissingRequiredDependencyException();
            }

            return new DNA(this.DNAConfiguration, components);
        }
    }
}
