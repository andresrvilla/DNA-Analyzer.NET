using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DNAAnalyzer.NET.Data.Contracts;
using DNAAnalyzer.NET.Data.SQLServer.Context;
using DNAAnalyzer.NET.Data.SQLServer.Entities;

namespace DNAAnalyzer.NET.Data.SQLServer
{
    public class MutantRepositorySQLServer : IMutantRepository
    {
        private DatabaseContext databaseContext;

        public MutantRepositorySQLServer()
        {
        }

        public MutantRepositorySQLServer(string connectionString)
        {
            this.Initialize(connectionString);
        }

        public void Initialize(string connectionString)
        {
            this.databaseContext = new DatabaseContext(connectionString);
        }

        public async Task<string> GetMutant(string dnaMutant)
        {
            var query = await(from m in this.databaseContext.Mutants
                               where m.Dna == dnaMutant
                               select m).ToListAsync();
            if (query.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return dnaMutant;
            }
        }

        public async Task<bool> InsertDNA(string dnaMutant)
        {
            if (string.IsNullOrEmpty(await this.GetMutant(dnaMutant)))
            {
                Mutant mutant = new Mutant();
                mutant.Dna = dnaMutant;
                this.databaseContext.Mutants.Add(mutant);
                await this.databaseContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
