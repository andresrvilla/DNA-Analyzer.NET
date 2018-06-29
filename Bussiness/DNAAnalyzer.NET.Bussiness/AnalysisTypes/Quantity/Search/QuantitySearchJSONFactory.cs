//-----------------------------------------------------------------------
// <copyright file="QuantitySearchJSONFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a construir objetos de Analisis de cantidad a partir de un json
// </summary>
//-----------------------------------------------------------------------
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using Extensions;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search
{
    public class QuantitySearchJSONFactory : IQuantitySearchJSONFactory
    {
        public QuantitySearchJSONFactory(IQuantitySearchFactory quantitySearchFactory, string type)
        {
            this.QuantitySearchFactory = quantitySearchFactory;
            this.Type = type;
        }

        public string Type
        {
            get;
            set;
        }

        public IQuantitySearchFactory QuantitySearchFactory
        {
            get;
            set;
        }

        public IQuantitySearch CreateInstance(dynamic jsonConfig)
        {
            if (this.QuantitySearchFactory == null || string.IsNullOrEmpty(this.Type))
            {
                throw new MissingRequiredDependencyException();
            }

            if (!DynamicExtensions.HasProperty(jsonConfig, "sequence"))
            {
                throw new InvalidJSONException();
            }

            return this.QuantitySearchFactory.CreateInstance(jsonConfig.sequence);
        }
    }
}
