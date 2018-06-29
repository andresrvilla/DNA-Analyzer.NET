using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Data.Contracts;
using DNAAnalyzer.NET.Exceptions;
using DNAAnalyzer.NET.Models.Contracts;
using DNAAnalyzer.NET.Services.Contracts;
using Unity;
using Unity.Resolution;

namespace DNAAnalyzer.NET.Services
{
    public class DNAAnalyzerService : IDNAAnalyzerService
    {
        private const string MutantDnaKey = "count_mutant_dna";

        private const string HumanDnaKey = "count_human_dna";

        private IUnityContainer container;

        private IEnumerable<IAnalysisSet> analysisSets;

        public DNAAnalyzerService(IUnityContainer container)
        {
            this.container = container;
        }

        public DNAAnalyzerService(IUnityContainer container, IMutantRepository mutantRepository, IStatsRepository statsRepository)
        {
            this.container = container;
            this.MutantRepository = mutantRepository;
            this.StatsRepository = statsRepository;
        }
        
        public IMutantRepository MutantRepository
        {
            get;
            set;
        }

        public IStatsRepository StatsRepository
        {
            get;
            set;
        }

        public void Configure(string json)
        {
            /// Factory Principal, que va a crear la lista de analysis set
            IAnalysisSetFactory analysisSetFactory = this.container.Resolve<IAnalysisSetFactory>();

            /// Factory de resultados de analisis de cantidad
            IQuantityAnalysisResultFactory quantityAnalysisResultFactory = this.container.Resolve<IQuantityAnalysisResultFactory>();
            IQuantityAnalysisFactory quantityAnalysisFactory = this.container.Resolve<IQuantityAnalysisFactory>();
            quantityAnalysisFactory.QuantityAnalysisResultFactory = quantityAnalysisResultFactory;

            /// Lista de busquedas por cantidad disponibles
            Dictionary<string, IQuantitySearchJSONFactory> availableSearchFactories = new Dictionary<string, IQuantitySearchJSONFactory>();

            /// Busqueda por todas las direcciones
            IQuantitySearchFactory allDirectionsSequenceQuantitySearchFactory = this.container.Resolve<IQuantitySearchFactory>("AllDirectionsSequenceQuantitySearchFactory");
            IQuantitySearchJSONFactory allDirectionsQuantityJSONFactory = this.container.Resolve<IQuantitySearchJSONFactory>(new ParameterOverride("quantitySearchFactory", allDirectionsSequenceQuantitySearchFactory), new ParameterOverride("type", "alldirectionssequencequantity"));
            availableSearchFactories.Add(allDirectionsQuantityJSONFactory.Type, allDirectionsQuantityJSONFactory);

            /// Factory de busquedas por cantidad, con las busquedas por cantidad disponibles
            IQuantitySearchTypesFactory quantitySearchTypesFactory = this.container.Resolve<IQuantitySearchTypesFactory>(new ParameterOverride("availableQuantityFactories", availableSearchFactories));
            IAnalysisJSONFactory quantityAnalysisJSONFactory = this.container.Resolve<IAnalysisJSONFactory>(
                "QuantityAnalysisJSONFactory",
                new ParameterOverride("quantityAnalysisFactory", quantityAnalysisFactory),
                new ParameterOverride("quantitySearchTypesFactory", quantitySearchTypesFactory));

            Dictionary<string, IAnalysisJSONFactory> availableAnalysisFactories = new Dictionary<string, IAnalysisJSONFactory>();
            availableAnalysisFactories.Add(quantityAnalysisJSONFactory.Type, quantityAnalysisJSONFactory);

            IAnalysisTypesFactory analysisTypesFactory = this.container.Resolve<IAnalysisTypesFactory>(new ParameterOverride("availableAnalysisFactories", availableAnalysisFactories));

            IAnalysisSetJSONFactory factory = this.container.Resolve<IAnalysisSetJSONFactory>(new ParameterOverride("analysisSetFactory", analysisSetFactory), new ParameterOverride("analysisTypesFactory", analysisTypesFactory));

            this.analysisSets = factory.CreateInstance(json);
        }

        public IEnumerable<IAnalysisResult> Analyze(string name, IDNA dna)
        {
            List<IAnalysisResult> result = new List<IAnalysisResult>();
            IAnalysisSet analysisSet = this.analysisSets.Where(a => a.Name == name).FirstOrDefault();
            if (analysisSet == null)
            {
                throw new UnknownTypeException();
            }

            return analysisSet.Analyze(dna);
        }

        public async Task<IQuantityAnalysisResult> AnalyzeMutant(IDNA dna)
        {
            List<IAnalysisResult> analysisResult = new List<IAnalysisResult>(this.Analyze("mutant", dna));
            IQuantityAnalysisResult result = (IQuantityAnalysisResult)analysisResult[0];

            if (await this.MutantRepository.InsertDNA(dna.StringRepresentation()))
            {
                if (result.Result)
                {
                    await this.StatsRepository.IncrementStat(MutantDnaKey);
                }
                else
                {
                    await this.StatsRepository.IncrementStat(HumanDnaKey);
                }
            }

            return result;
        }

        public async Task<ExpandoObject> GetMutantsStats()
        {
            Dictionary<string, long> stats = await this.StatsRepository.GetStats();
            ExpandoObject result = new ExpandoObject();
            var resultAlias = result as IDictionary<string, object>;

            long mutantValue = 0;
            long humanValue = 0;

            foreach (var item in stats)
            {
                resultAlias.Add(item.Key, item.Value);
                switch (item.Key)
                {
                    case MutantDnaKey:
                    mutantValue = item.Value;
                    break;
                    case HumanDnaKey:
                    humanValue = item.Value;
                    break;
                }
            }

            if (mutantValue != 0 && humanValue != 0)
            {
                resultAlias.Add("ratio", mutantValue / humanValue);
            }
            else
            {
                if (!resultAlias.ContainsKey(MutantDnaKey))
                {
                    resultAlias.Add(MutantDnaKey, 0);
                }

                if (!resultAlias.ContainsKey(HumanDnaKey))
                {
                    resultAlias.Add(HumanDnaKey, 0);
                }

                resultAlias.Add("ratio", "undefined");
            }

            return result;
        }
    }
}
