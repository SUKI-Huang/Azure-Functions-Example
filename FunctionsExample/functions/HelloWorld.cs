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
    public static class HelloWorld
    {
        [FunctionName("helloWorld")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            Response response = new Response() { status = true, data = "Hello world" };
            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
