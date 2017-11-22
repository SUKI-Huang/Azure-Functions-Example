using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using FunctionsExample.http;
using FunctionsExample.model;

namespace FunctionsExample.functions
{
    public static class BodyExample
    {
        [FunctionName("bodyExample")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {

            string name = null;
            string email = null;

            //GET
            name = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0).Value;
            email = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0).Value;

            //Other Method (POST PUT PATCH ...)
            //dynamic data = await req.Content.ReadAsAsync<object>();
            //name = data?.name;
            //email = data?.email;

            //Other Method (POST PUT PATCH ...)
            //Read as object
            UserInfoModel userInfoModel = await req.Content.ReadAsAsync<UserInfoModel>();
            name = userInfoModel.name;
            email = userInfoModel.email;

            log.Info("bodyExample\tname:"+ name);
            log.Info("bodyExample\temail:" + email);

            Response response = new Response() { status = true, data = "this is body parameters exmaple" };
            return req.CreateResponse(HttpStatusCode.OK, response);
        }

    }

}
