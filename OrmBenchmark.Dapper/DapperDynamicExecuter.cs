using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace OrmBenchmark.Dapper
{
    public class DapperDynamicExecuter : IOrmExecuter
    {
        SqlConnection conn;

        public string Name
        {
            get
            {
                return "Dapper Dynamic Query (Non Buffered)";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();
        }

        public object GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.Query("select * from Posts where Id=@Id", param, buffered: false).First();
        }

        public object GetItems(string Id)
        {
            return conn.Query("select * from Posts", null, buffered: false).ToList();
        }

        public void Finish()
        {
            conn.Close();
        }
    }
}
