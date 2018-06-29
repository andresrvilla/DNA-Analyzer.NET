using System.Threading.Tasks;

namespace DNAAnalyzer.NET.Data.Contracts
{
    public interface IMutantRepository
    {
        Task<bool> InsertDNA(string dnaMutant);

        Task<string> GetMutant(string dnaMutant);

        void Initialize(string connectionString);
    }
}
