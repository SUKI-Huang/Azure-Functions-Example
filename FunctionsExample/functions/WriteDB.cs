using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using FunctionsExample.functions;
using System.Configuration;
using System.Data.SqlClient;
using FunctionsExample.model;
using FunctionsExample.http;
using FunctionsExample.sql;

namespace FunctionsExample.functions
{
    public static class WriteDB
    {
        [FunctionName("writeDB")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            UserInfoModel userInfoModel = await req.Content.ReadAsAsync<UserInfoModel>();
            string name = userInfoModel.name;
            string email = userInfoModel.email;

            log.Info("name : " + name);
            log.Info("email : " + email);

            //connect to the database
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["exampleDB"].ConnectionString);
            sqlConnection.Open();

            //excute sql command (add new user)
            SqlCommand cmd = new SqlCommand(SQLQuery.addUser(name, email), sqlConnection);
            var rows = await cmd.ExecuteNonQueryAsync();

            sqlConnection.Close();

            //reponse
            Response response = new Response() { status = true, data = "write SQL database example" };
            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
