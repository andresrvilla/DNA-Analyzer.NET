using System.Web.Http;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Contracts;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Result;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes.Quantity.Search;
using DNAAnalyzer.NET.Bussiness.Set;
using DNAAnalyzer.NET.Data.Contracts;
using DNAAnalyzer.NET.Data.SQLServer;
using DNAAnalyzer.NET.Models;
using DNAAnalyzer.NET.Models.Contracts;
using DNAAnalyzer.NET.Services;
using DNAAnalyzer.NET.Services.Contracts;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace DNAAnalyzer.NET.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            container.RegisterType<IAnalysisSetFactory, AnalysisSetFactory>();
            container.RegisterType<IQuantityAnalysisResultFactory, QuantityAnalysisResultFactory>(new InjectionConstructor(container));
            container.RegisterType<IQuantityAnalysisFactory, QuantityAnalysisFactory>();
            container.RegisterType<IQuantitySearchFactory, AllDirectionsSequenceQuantitySearchFactory>("AllDirectionsSequenceQuantitySearchFactory", new InjectionConstructor(container));
            container.RegisterType<IQuantitySearchJSONFactory, QuantitySearchJSONFactory>();
            container.RegisterType<IQuantitySearchTypesFactory, QuantitySearchTypesFactory>();
            container.RegisterType<IAnalysisJSONFactory, QuantityAnalysisJSONFactory>("QuantityAnalysisJSONFactory");
            container.RegisterType<IAnalysisTypesFactory, AnalysisTypesFactory>();
            container.RegisterType<IAnalysisSetJSONFactory, AnalysisSetJSONFactory>();

            container.RegisterType<IAnalysisResult, QuantityAnalysisResult>("quantityAnalysisResult");
            container.RegisterType<IQuantitySearch, AllDirectionsSequenceQuantitySearch>("allDirectionsSequenceQuantitySearch");

            /* Singleton Variables */
            container.RegisterType<IDNAConfiguration, DNAConfiguration>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IDNAFactory, DNAFactory>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IDNAAnalyzerService, DNAAnalyzerService>(new ContainerControlledLifetimeManager(), new InjectionConstructor(container));
            container.RegisterType<IMutantRepository, MutantRepositorySQLServer>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IStatsRepository, StatsRepositorySQLServer>(new ContainerControlledLifetimeManager(), new InjectionConstructor());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}