using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNAAnalyzer.NET.Data.Contracts
{
    public interface IStatsRepository
    {
        Task InsertStats(Dictionary<string, long> stats);

        Task<Dictionary<string, long>> GetStats();

        void Initialize(string connectionString);

        Task IncrementStat(string humanDnaKey);
    }
}
