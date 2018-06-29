//-----------------------------------------------------------------------
// <copyright file="AnalysisTypesFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a la creación de Analisis dependiendo de ciertos analisis configurados
// </summary>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Exceptions;
using Extensions;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes
{
    public class AnalysisTypesFactory : IAnalysisTypesFactory
    {
        public AnalysisTypesFactory()
        {
        }

        public AnalysisTypesFactory(Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories)
        {
            this.AvailableAnalysisFactories = availableAnalysisFactories;
        }
        
        public Dictionary<string, IAnalysisJSONFactory> AvailableAnalysisFactories
        {
            get;
            set;
        }

        public IAnalysis CreateInstance(dynamic jsonConfig)
        {
            if (this.AvailableAnalysisFactories == null)
            {
                throw new MissingRequiredDependencyException();
            }

            if (DynamicExtensions.HasProperty(jsonConfig, "type") && this.AvailableAnalysisFactories.ContainsKey(jsonConfig.type))
            {
                return this.AvailableAnalysisFactories[jsonConfig.type].CreateInstance(jsonConfig);
            }
            else
            {
                throw new UnknownTypeException();
            }
        }
    }
}
