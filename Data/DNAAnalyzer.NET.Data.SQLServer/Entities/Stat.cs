using System.ComponentModel.DataAnnotations;

namespace DNAAnalyzer.NET.Data.SQLServer.Entities
{
    public class Stat
    {
        [Key]
        public string Key
        {
            get;
            set;
        }

        public long Value
        {
            get;
            set;
        }
    }
}
