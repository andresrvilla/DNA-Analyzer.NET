//-----------------------------------------------------------------------
// <copyright file="QuantityAnalysisResultFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a construir objetos de resultados de analisis de cantidad
// </summary>
//-----------------------------------------------------------------------
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using Unity;
using Unity.Resolution;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Result
{
    public class QuantityAnalysisResultFactory : IQuantityAnalysisResultFactory
    {
        private IUnityContainer container;

        public QuantityAnalysisResultFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public IAnalysisResult CreateInstance(int min, int max, bool result)
        {
            return this.container.Resolve<IAnalysisResult>("quantityAnalysisResult", new ParameterOverride("min", min), new ParameterOverride("max", max), new ParameterOverride("result", result));
        }
    }
}
