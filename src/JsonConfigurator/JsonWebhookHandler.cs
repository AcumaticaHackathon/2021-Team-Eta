#region #Copyright
//  ----------------------------------------------------------------------------------
// COPYRIGHT (c) 2021 CONTOU CONSULTING
// ALL RIGHTS RESERVED
// AUTHOR: Kyle Vanderstoep
// CREATED DATE: 2021/07/17
// ----------------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using JsonConfigurator.DAC;
using Newtonsoft.Json;
using PX.Api.Webhooks.DAC;
using PX.Common;
using PX.Data;
using PX.Data.Webhooks;

namespace JsonConfigurator
{
    public class JsonWebhookHandler : PXGraph, IWebhookHandler
    {
        public Task<IHttpActionResult> ProcessRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var scope = GetAdminScope())
            {
                _graph = new PXGraph();
                GetIntegrationScenario(request.RequestUri.ToString());

                string body = request.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return Task.FromResult(SendResponse(HttpStatusCode.Accepted, default));
            }
            
        }

        protected IHttpActionResult SendResponse(HttpStatusCode code, object result, string message = "", bool success = true)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(result)),
                StatusCode = code
            };

            return new ResponseMessageResult(response);
        }

        private void GetIntegrationScenario(string url)
        {
            string id = url.Split('/').Last();
            WebHook webhook = PXSelect<WebHook, Where<WebHook.webHookID, Equal<Required<WebHook.webHookID>>>>.Select(
                _graph, new Guid(id));

            _request =
                PXSelect<
                    IntegrationScenarioDetail, 
                    Where<IntegrationScenarioDetail.webhook,
                    Equal<Required<IntegrationScenarioDetail.webhook>>>>
                    .Select(_graph, webhook.WebHookID);

            _response =
                PXSelect<
                        IntegrationScenarioDetail,
                        Where<IntegrationScenarioDetail.scenarioID,
                            Equal<Required<IntegrationScenarioDetail.scenarioID>>,
                            And<IntegrationScenarioDetail.matchingStep,
                                Equal<Required<IntegrationScenarioDetail.lineNbr>>>>>
                    .Select(_graph, _request.ScenarioID, _request.LineNbr);
        }

        private IDisposable GetAdminScope()
        {
            var userName = "admin";
            if (PXDatabase.Companies.Length > 0)
            {
                var company = PXAccess.GetCompanyName();
                if (string.IsNullOrEmpty(company))
                {
                    company = PXDatabase.Companies[0];
                }
                userName = userName + "@" + company;
            }
            return new PXLoginScope(userName);
        }

        private IntegrationScenarioDetail _request;
        private IntegrationScenarioDetail _response;
        private PXGraph _graph;
    }
}