//-----------------------------------------------------------------------
// <copyright file="QuantitySearchTypesFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a construir instancias de Analisis de Cantidad
// </summary>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search
{
    public class QuantitySearchTypesFactory : IQuantitySearchTypesFactory
    {
        public QuantitySearchTypesFactory(Dictionary<string, IQuantitySearchJSONFactory> availableQuantityFactories)
        {
            this.AvailableQuantityFactories = availableQuantityFactories;
        }

        public Dictionary<string, IQuantitySearchJSONFactory> AvailableQuantityFactories
        {
            get;
            set;
        }

        public IQuantitySearch CreateInstance(dynamic jsonConfig)
        {
            if (this.AvailableQuantityFactories.ContainsKey(jsonConfig.type))
            {
                return this.AvailableQuantityFactories[jsonConfig.type].CreateInstance(jsonConfig);
            }
            else
            {
                throw new UnknownTypeException();
            }
        }
    }
}
