using Dapper;
using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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

        public IPost GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.Query<Post>("select * from Posts where Id=@Id", param, buffered: true).First();
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return conn.Query("select * from Posts where Id=@Id", param, buffered: true).First();
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.Query<Post>("select * from Posts", null, buffered: true).ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return conn.Query("select * from Posts", null, buffered: true).ToList();
        }

        public void Finish()
        {
            conn.Close();
        }
    }
}
