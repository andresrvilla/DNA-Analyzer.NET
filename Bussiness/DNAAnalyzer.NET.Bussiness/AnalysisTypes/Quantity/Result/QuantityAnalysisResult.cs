//-----------------------------------------------------------------------
// <copyright file="QuantityAnalysisResult.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a contener resultados de analisis de cantidad
// </summary>
//-----------------------------------------------------------------------
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Result
{
    public class QuantityAnalysisResult : IQuantityAnalysisResult, IAnalysisResult
    {
        private int min;

        private int max;

        private bool result;

        public QuantityAnalysisResult(int min, int max, bool result)
        {
            this.min = min;
            this.max = max;
            this.result = result;
        }

        public bool Result
        {
            get
            {
                return this.result;
            }
        }

        public int Max
        {
            get
            {
                return this.max;
            }
        }

        public int Min
        {
            get
            {
                return this.min;
            }
        }
    }
}
