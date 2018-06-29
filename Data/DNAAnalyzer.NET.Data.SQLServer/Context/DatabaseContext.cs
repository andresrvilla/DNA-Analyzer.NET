using System.Data.Entity;
using DNAAnalyzer.NET.Data.SQLServer.Entities;

namespace DNAAnalyzer.NET.Data.SQLServer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Mutant> Mutants { get; set; }

        public DbSet<Stat> Stats { get; set; }
    }
}
