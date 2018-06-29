//-----------------------------------------------------------------------
// <copyright file="AllDirectionsSequenceQuantitySearchFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a construir objetos de busqueda de secuencia en todas las direcciones
// </summary>
//-----------------------------------------------------------------------
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using Unity;
using Unity.Resolution;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search
{
    public class AllDirectionsSequenceQuantitySearchFactory : IQuantitySearchFactory
    {
        private IUnityContainer container;

        public AllDirectionsSequenceQuantitySearchFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public IQuantitySearch CreateInstance(string sequenceToFind)
        {
            return this.container.Resolve<IQuantitySearch>("allDirectionsSequenceQuantitySearch", new ParameterOverride("sequenceToFind", sequenceToFind));
        }
    }
}
