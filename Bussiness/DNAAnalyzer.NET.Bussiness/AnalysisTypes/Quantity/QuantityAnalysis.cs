//-----------------------------------------------------------------------
// <copyright file="QuantityAnalysis.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a agrupar analisis de cantidad
// </summary>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Exceptions;
using DNAAnalyzer.NET.Models.Contracts;

namespace DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity
{
    public class QuantityAnalysis : IQuantityAnalysis
    {
        public const string ClassKey = "quantity";       

        public QuantityAnalysis(int? min, int? max, IQuantityAnalysisResultFactory quantityAnalysisResultFactory, IEnumerable<IQuantitySearch> quantitySearchList)
        {
            this.Min = min ?? int.MinValue;
            this.Max = max ?? int.MaxValue;
            this.QuantitySearchList = quantitySearchList;
            this.QuantityAnalysisResultFactory = quantityAnalysisResultFactory;
        }

        public string Type => ClassKey;

        public int Min
        {
            get;
            set;
        }

        public int Max
        {
            get;
            set;
        }

        public IEnumerable<IQuantitySearch> QuantitySearchList
        {
            get;
            set;
        }

        public IQuantityAnalysisResultFactory QuantityAnalysisResultFactory
        {
            get;
            set;
        }

        public IAnalysisResult Analyze(IDNA dna)
        {
            if (!dna.IsValid())
            {
                throw new InvalidDNAException();
            }

            int quantity = 0;
            foreach (var quantitySearch in this.QuantitySearchList)
            {
                quantity += quantitySearch.Search(dna);
            }

            bool analysisThresholdTest = quantity <= this.Max && quantity >= this.Min;

            IAnalysisResult result = this.QuantityAnalysisResultFactory.CreateInstance(this.Min, this.Max, analysisThresholdTest);

            return result;
        }
    }
}
