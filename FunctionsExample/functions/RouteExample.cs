using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using FunctionsExample.http;

namespace FunctionsExample.functions
{
    public static class RouteExample
    {
        [FunctionName("routeExample")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "routeExample/{userName:alpha}/{id:int}")]HttpRequestMessage req,string userName, int? id, TraceWriter log)
        {

            log.Info("routeExample\tuserName:"+ userName);
            log.Info("routeExample\tid:" + id);

            Response response = new Response() { status = true, data = "this is routing example" };
            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
