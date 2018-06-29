//-----------------------------------------------------------------------
// <copyright file="QuantityAnalysisFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a la creación de objetos Analisis de Cantidad dependiendo de sub analisis de cantidad
// </summary>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity
{
    public class QuantityAnalysisFactory : IQuantityAnalysisFactory
    {
        public QuantityAnalysisFactory()
        {
        }

        public QuantityAnalysisFactory(IQuantityAnalysisResultFactory quantityAnalysisResultFactory)
        {
            this.QuantityAnalysisResultFactory = quantityAnalysisResultFactory;
        }

        public IQuantityAnalysisResultFactory QuantityAnalysisResultFactory
        {
            get;
            set;
        }

        public IQuantityAnalysis CreateInstance(int? min, int? max, IEnumerable<IQuantitySearch> quantitySearchList)
        {
            if (this.QuantityAnalysisResultFactory == null)
            {
                throw new MissingRequiredDependencyException();
            }

            return new QuantityAnalysis(min, max, this.QuantityAnalysisResultFactory, quantitySearchList);
        }
    }
}
