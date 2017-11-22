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
using System.Collections.Generic;


namespace FunctionsExample.functions
{
    public static class ReadDB
    {
        [FunctionName("readDB")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {

            //connect to the database
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["exampleDB"].ConnectionString);
            sqlConnection.Open();

            //excute sql command (get all user)
            SqlCommand cmd = new SqlCommand(SQLQuery.getAllUser(), sqlConnection);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            List<UserInfoModel> userInfos = new List<UserInfoModel>();
            while (reader.Read())
            {
                UserInfoModel userInfo = new UserInfoModel() { name = (string)reader["Name"], email = (string)reader["Email"] };
                userInfos.Add(userInfo);
            }

            sqlConnection.Close();

            //reponse
            Response response = new Response() { status = true, data = userInfos };
            return req.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
