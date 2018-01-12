using Insight.Database;
using OrmBenchmark.Core;
using OrmBenchmark.Insight.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace OrmBenchmark.InsightDatabase
{
    public class InsightSingleDatabaseExecuter : IOrmExecuter
    {
        private SqlConnection conn;

        public string Name => "Insight Database (Single)";

        public void Finish()
        {
            conn.Close();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return null;
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return null;
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return conn.SingleSql<dynamic>("select * from Posts where Id=@Id", param);
        }

        public IPost GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.SingleSql<Post>("select * from Posts where Id=@Id", param);
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            SqlInsightDbProvider.RegisterProvider();
        }
    }
}