using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsExample.sql
{
    static class SQLQuery
    {
        public static string addUser(string name, string email) {
            return "INSERT INTO [dbo].[User] (Name, Email) VALUES ('"+ name + "', '"+ email + "');";
        }

        public static string getAllUser()
        {
            return "SELECT * FROM [dbo].[User];";
        }
    }
}
