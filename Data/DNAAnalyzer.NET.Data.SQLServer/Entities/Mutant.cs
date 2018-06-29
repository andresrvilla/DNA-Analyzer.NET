using System.ComponentModel.DataAnnotations;

namespace DNAAnalyzer.NET.Data.SQLServer.Entities
{
    public class Mutant
    {
        [Key]
        public string Dna
        {
            get;
            set;
        }
    }
}
