using Insight.Database;
using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OrmBenchmark.InsightDatabase
{
    public class InsightDatabaseExecuter : IOrmExecuter
    {
        private SqlConnection conn;

        public string Name => "Insight Database";

        public void Finish()
        {
            conn.Close();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return conn.QuerySql<dynamic>("select * from Posts");
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.QuerySql<Post>("select * from Posts").ToList<IPost>();
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return conn.QuerySql<dynamic>("select * from Posts where Id=@Id", param).FirstOrDefault();
        }

        public IPost GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.QuerySql<Post>("select * from Posts where Id=@Id", param).FirstOrDefault();
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            SqlInsightDbProvider.RegisterProvider();
        }
    }
}
