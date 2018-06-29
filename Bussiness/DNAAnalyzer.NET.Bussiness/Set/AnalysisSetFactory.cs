//-----------------------------------------------------------------------
// <copyright file="AnalysisSetFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a construir sets de analisis
// </summary>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;

namespace DNAAnalyzer.NET.Bussiness.Set
{
    public class AnalysisSetFactory : IAnalysisSetFactory
    {
        public IAnalysisSet CreateInstance(string name, IEnumerable<IAnalysis> analyses)
        {
            return new AnalysisSet(name, analyses);
        }
    }
}
