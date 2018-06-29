//-----------------------------------------------------------------------
// <copyright file="AnalysisSet.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a contener distintos tipos de analisis
// </summary>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Bussiness.Set
{
    public class AnalysisSet : IAnalysisSet
    {
        public const string ClassType = "analysisset";        

        public AnalysisSet(string name, IEnumerable<IAnalysis> configuredAnalyses)
        {
            this.Name = name;
            this.ConfiguredAnalyses = configuredAnalyses;
        }

        public IEnumerable<IAnalysis> ConfiguredAnalyses
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IEnumerable<IAnalysisResult> Analyze(IDNA dna)
        {
            List<IAnalysisResult> result = new List<IAnalysisResult>();
            foreach (var analysis in this.ConfiguredAnalyses)
            {
                result.Add(analysis.Analyze(dna));
            }

            return result;
        }
    }
}
