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
    public static class RestfulExample
    {
        [FunctionName("restfulExample")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "patch", "delete", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            Response response = new Response();
            response.status = true;
            switch (req.Method.Method)
            {
                case "GET":
                    response.data = "Method : GET";
                    break;
                case "POST":
                    response.data = "Method : POST";
                    break;
                case "PUT":
                    response.data = "Method : PUT";
                    break;
                case "PATCH":
                    response.data = "Method : PATCH";
                    break;
                case "DELETE":
                    response.data = "Method : DELETE";
                    break;
            }

            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
