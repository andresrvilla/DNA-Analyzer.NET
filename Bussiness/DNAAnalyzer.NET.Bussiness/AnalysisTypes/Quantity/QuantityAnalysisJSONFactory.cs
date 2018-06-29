//-----------------------------------------------------------------------
// <copyright file="QuantityAnalysisJSONFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a la creación de objetos de Analisis de Cantidad a partir de un objeto de configuracion
// </summary>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using Extensions;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity
{
    public class QuantityAnalysisJSONFactory : IAnalysisJSONFactory
    {
        public const string ClassKey = "quantity";

        public QuantityAnalysisJSONFactory()
        {
        }

        public QuantityAnalysisJSONFactory(IQuantityAnalysisFactory quantityAnalysisFactory, IQuantitySearchTypesFactory quantitySearchTypesFactory)
        {
            this.QuantityAnalysisFactory = quantityAnalysisFactory;
            this.QuantitySearchTypesFactory = quantitySearchTypesFactory;
            this.Type = ClassKey;
        }

        public string Type
        {
            get;
            set;
        }

        public IQuantityAnalysisFactory QuantityAnalysisFactory
        {
            get;
            set;
        }

        public IQuantitySearchTypesFactory QuantitySearchTypesFactory
        {
            get;
            set;
        }

        public IAnalysis CreateInstance(dynamic jsonConfig)
        {
            if (this.QuantityAnalysisFactory == null || this.QuantitySearchTypesFactory == null || string.IsNullOrEmpty(this.Type))
            {
                throw new MissingRequiredDependencyException();
            }

            int? min = null;
            int? max = null;

            if (!DynamicExtensions.HasProperty(jsonConfig, "search"))
            {
                throw new InvalidJSONException();
            }

            if (DynamicExtensions.HasProperty(jsonConfig, "min"))
            {
                min = DynamicExtensions.AsInt(jsonConfig.min);
            }

            if (DynamicExtensions.HasProperty(jsonConfig, "max"))
            {
                max = DynamicExtensions.AsInt(jsonConfig.max);
            }

            List<IQuantitySearch> quantitySearchList = new List<IQuantitySearch>();
            foreach (var searchItem in jsonConfig.search)
            {
                quantitySearchList.Add(this.QuantitySearchTypesFactory.CreateInstance(searchItem));
            }

            return this.QuantityAnalysisFactory.CreateInstance(min, max, quantitySearchList);
        }
    }
}
