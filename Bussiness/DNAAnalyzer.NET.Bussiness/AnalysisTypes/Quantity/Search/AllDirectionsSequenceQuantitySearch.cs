//-----------------------------------------------------------------------
// <copyright file="AllDirectionsSequenceQuantitySearch.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a buscar secuencias en todas las direcciones (Horizontal, Vertical, Diagonal)
// </summary>
//-----------------------------------------------------------------------
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Models.Contracts;
using Extensions;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search
{
    public class AllDirectionsSequenceQuantitySearch : ISequenceQuantitySearch
    {
        public const string ClassType = "alldirectionssequencequantity";

        public AllDirectionsSequenceQuantitySearch(string sequenceToFind)
        {
            this.SequenceToFind = sequenceToFind;
        }

        public string SequenceToFind
        {
            get;
            set;
        }

        public string Type => ClassType;

        public int Search(IDNA dna)
        {
            int result = 0;

            result += dna.Components.CountHorizontalOcurrences(this.SequenceToFind);
            result += dna.Components.CountVerticalOcurrences(this.SequenceToFind);
            result += dna.Components.CountDiagonalOcurrences(this.SequenceToFind);

            return result;
        }
    }
}
