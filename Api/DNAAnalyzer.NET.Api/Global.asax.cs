using System;
using System.Configuration;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DNAAnalyzer.NET.Api.App_Start;
using DNAAnalyzer.NET.Data.Contracts;
using DNAAnalyzer.NET.Models.Contracts;
using DNAAnalyzer.NET.Services.Contracts;
using Unity;

namespace DNAAnalyzer.NET.Api
{
    public class Global : System.Web.HttpApplication
    {
        private UnityContainer container;
        private IDNAAnalyzerService analyzerService;
        private IDNAFactory dnaFactory;

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            this.container = new UnityContainer();
            UnityConfig.RegisterComponents(this.container);

            /*
             *  No podemos inyectar dependencias en el Global.asax
             *  Ver:
             *  https://stackoverflow.com/questions/7752023/how-to-inject-dependencies-into-the-global-asax-cs
             *  http://blog.ploeh.dk/2011/07/28/CompositionRoot/
             */
            string jsonConfiguration = File.ReadAllText(Server.MapPath("~") + "\\mutantConfiguration.json");
            this.analyzerService = this.container.Resolve<IDNAAnalyzerService>();
            this.analyzerService.Configure(jsonConfiguration);

            var mutantRepository = this.container.Resolve<IMutantRepository>();
            mutantRepository.Initialize(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

            var statsRepository = this.container.Resolve<IStatsRepository>();
            statsRepository.Initialize(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

            this.analyzerService.MutantRepository = mutantRepository;
            this.analyzerService.StatsRepository = statsRepository;

            ///Configure IDNAConfiguration singleton
            var dnaConfiguration = this.container.Resolve<IDNAConfiguration>();
            dnaConfiguration.ComponentsPattern = ConfigurationManager.AppSettings["DnaComponentsPattern"];
            ///Configure IDNAFactory singleton
            this.dnaFactory = this.container.Resolve<IDNAFactory>();
            this.dnaFactory.DNAConfiguration = dnaConfiguration;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}