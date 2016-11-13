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
    public class DapperBufferedExecuter : IOrmExecuter
    {
        SqlConnection conn;

        public string Name
        {
            get
            {
                return "Dapper Query (Buffered)";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();
        }

        public object GetItem(int Id)
        {
            object param = new { Id = Id };
            return conn.Query<Post>("select * from Posts where Id=@Id", param, buffered: true).First();
        }

        public object GetItems(string Id)
        {
            return conn.Query<Post>("select * from Posts", null, buffered: true).ToList();
        }

        public void Finish()
        {
            conn.Close();
        }
    }
}
