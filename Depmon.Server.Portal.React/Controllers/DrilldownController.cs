using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Depmon.Server.Database;
using Newtonsoft.Json;
using Dapper;
using Depmon.Server.Database.Queries;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Depmon.Server.Portal.React.Controllers
{
    public class DrilldownController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DrilldownController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public HttpResponseMessage IsNewReportExist(DateTime dateTime)
        {
            var sql = QueryStore.IsNewReportExist();

            var data = _unitOfWork.Session.Query(sql, new { dateTime }).Single();
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JObject.FromObject(data, serializer).ToString(), Encoding.UTF8,
                "application/json");
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Sources()
        {
            var sql = QueryStore.SourceQuery();

            var data = _unitOfWork.Session.Query(sql);
             var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JArray.FromObject(data, serializer).ToString(), Encoding.UTF8,
                "application/json");
            return response;
        }

        [HttpGet]
        public HttpResponseMessage SourceInfo(string sourceCode = null)
        {
            string sql = string.Empty;

            sql = string.IsNullOrWhiteSpace(sourceCode)
                ? QueryStore.SourceInfoQuery()
                : QueryStore.SourceInfoByCodeQuery();

            var data = _unitOfWork.Session.Query(sql, new {sourceCode});
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });


            var list =
                data.GroupBy(entity => entity.SourceCode).Select(sources => new
                {
                    sources,
                    groups = sources.GroupBy(source => source.GroupCode).Select(groups => new
                    {
                        groups,
                        resources = groups.GroupBy(group => group.ResourceCode).Select(resources => new
                        {
                            resources,
                            indicators = resources.GroupBy(resource => resource.IndicatorCode).Select(indicators => new
                            {
                                indicators
                            }).Select(@t => new { Code = @t.indicators.Key, Values = @t.indicators.Select(s=> new
                            {
                               Value = s.IndicatorValue,
                               Description = s.IndicatorDescription,
                               Level = s.Level
                            }) })

                        }).Select(@t => new { Code = @t.resources.Key, Indicators = @t.indicators })
                    }).Select(@t => new {Code = @t.groups.Key, Resources = @t.resources})
                }).Select(@t => new {Code = @t.sources.Key, Groups = @t.groups});

            
            JObject root = new JObject();
            root.Add("sources", JArray.FromObject(list, serializer));
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(root.ToString(), Encoding.UTF8,
                "application/json");
            return response;
        }
    }
}
