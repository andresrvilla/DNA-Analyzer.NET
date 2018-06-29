//-----------------------------------------------------------------------
// <copyright file="AnalysisSetJSONFactory.cs" company="MercadoLibre">
//     MercadoLibre. Todos los derechos Reservados
// </copyright>
// <summary>
// Clase destinada a construir objetos de set de analisis a partir de un json
// </summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Dynamic;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisSet;
using DNAAnalyzer.NET.Bussiness.Contracts.AnalysisTypes;
using DNAAnalyzer.NET.Exceptions;
using Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DNAAnalyzer.NET.Bussiness.Set
{
    public class AnalysisSetJSONFactory : IAnalysisSetJSONFactory
    {
        public AnalysisSetJSONFactory(IAnalysisSetFactory analysisSetFactory, IAnalysisTypesFactory analysisTypesFactory)
        {
            this.AnalysisSetFactory = analysisSetFactory;
            this.AnalysisTypesFactory = analysisTypesFactory;
        }

        public IAnalysisSetFactory AnalysisSetFactory
        {
            get;
            set;
        }

        public IAnalysisTypesFactory AnalysisTypesFactory
        {
            get;
            set;
        }

        public IEnumerable<IAnalysisSet> CreateInstance(string json)
        {
            ///TODO: separar la conversion de json con la generacion del objeto
            List<IAnalysisSet> result = new List<IAnalysisSet>();

            ExpandoObjectConverter converter = new ExpandoObjectConverter();
            dynamic jsonObject;
            try
            {
                jsonObject = JsonConvert.DeserializeObject<ExpandoObject>(json, converter);
            }
            catch (ArgumentNullException e)
            {
                result = null;
                throw new InvalidJSONException(e.Message, e.InnerException);
            }
            catch (JsonSerializationException e)
            {
                result = null;
                throw new InvalidJSONException(e.Message, e.InnerException);
            }

            if (jsonObject == null || !DynamicExtensions.HasProperty(jsonObject, "configuration") || !DynamicExtensions.HasProperty(jsonObject.configuration, "Count"))
            {
                throw new InvalidJSONException();
            }

            foreach (var config in jsonObject.configuration)
            {
                if (!DynamicExtensions.HasProperty(config, "type"))
                {
                    throw new InvalidJSONException();
                }

                switch (config.type)
                {
                    case AnalysisSet.ClassType:
                    List<IAnalysis> analysis = new List<IAnalysis>();

                    if (!DynamicExtensions.HasProperty(config, "name") || !DynamicExtensions.HasProperty(config, "analyses") || !DynamicExtensions.HasProperty(config.analyses, "Count"))
                    {
                        throw new InvalidJSONException();
                    }

                    foreach (var analisysConfig in config.analyses)
                    {
                        analysis.Add(this.AnalysisTypesFactory.CreateInstance(analisysConfig));
                    }

                    IAnalysisSet analysisSet = this.AnalysisSetFactory.CreateInstance(config.name, analysis);
                    result.Add(analysisSet);
                    break;
                    default:
                    throw new InvalidJSONException();
                }
            }

            return result;
        }
    }
}
