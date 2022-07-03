using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace MvcDapperNew.Models
{
    public static class DapperORM
    {
        //use Your system Connection String Settings in line no 10
        private static string connectionString = @"Data Source=DESKTOP-M997L96;Initial Catalog = DapperDb; User ID = sa; Password=admin;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void ExecuteWithourReturn(string procedureName,DynamicParameters param = null) 
        { 
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            { 
                sqlcon.Open();
                sqlcon.Execute(procedureName, param,commandType :CommandType.StoredProcedure);
            }
        }

        public static T ExecuteReturnScalar <T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return(T) Convert.ChangeType (sqlcon.Execute(procedureName, param, commandType: CommandType.StoredProcedure),typeof(T));
            }
        }

        public static IEnumerable <T> ReturnList <T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                return sqlcon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
