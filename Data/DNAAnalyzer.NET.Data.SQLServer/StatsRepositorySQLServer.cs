using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DNAAnalyzer.NET.Data.Contracts;
using DNAAnalyzer.NET.Data.SQLServer.Context;
using DNAAnalyzer.NET.Data.SQLServer.Entities;

namespace DNAAnalyzer.NET.Data.SQLServer
{
    public class StatsRepositorySQLServer : IStatsRepository
    {
        private DatabaseContext databaseContext;

        public StatsRepositorySQLServer()
        {
        }

        public StatsRepositorySQLServer(string connectionString)
        {
            this.Initialize(connectionString);
        }

        public void Initialize(string connectionString)
        {
            this.databaseContext = new DatabaseContext(connectionString);
        }

        public async Task<Dictionary<string, long>> GetStats()
        {
            List<Stat> stats = await this.GetStatList();
            Dictionary<string, long> result = new Dictionary<string, long>();
            foreach (var item in stats)
            {
                result.Add(item.Key, item.Value);
            }

            return result;
        }

        public async Task InsertStats(Dictionary<string, long> stats)
        {
            List<Stat> currentStats = await this.GetStatList();

            foreach (var item in stats)
            {
                Stat statFind = currentStats.Where(s => s.Key == item.Key).FirstOrDefault();
                if (statFind == null)
                {
                    Stat stat = new Stat();
                    stat.Key = item.Key;
                    stat.Value = item.Value;
                    this.databaseContext.Stats.Add(stat);
                }
                else
                {
                    statFind.Value = item.Value;
                }
            }

            this.databaseContext.SaveChanges();
        }

        public async Task IncrementStat(string key)
        {
            Dictionary<string, long> stats = new Dictionary<string, long>();
            List<Stat> currentStats = await this.GetStatList();
            foreach (var item in currentStats)
            {
                if (item.Key == key)
                {
                    item.Value++;
                }

                stats.Add(item.Key, item.Value);
            }

            if (!stats.ContainsKey(key))
            {
                stats.Add(key, 1);
            }

            await this.InsertStats(stats);
        }

        private async Task<List<Stat>> GetStatList()
        {
            var queryList = await(from s in this.databaseContext.Stats
                                   select s).ToListAsync();
            return queryList;
        }
    }
}
