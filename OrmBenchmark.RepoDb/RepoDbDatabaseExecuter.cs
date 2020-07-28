using OrmBenchmark.Core;
using RepoDb;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace OrmBenchmark.RepoDb
{
    public class RepoDbDatabaseExecuter : IOrmExecuter
    {
        private DbConnection connection;

        public string Name => "RepoDb";

        public void Finish()
        {
            connection.Close();
        }

        public IList<dynamic> GetAllItemsAsDynamic() => connection.ExecuteQuery<Post>("SELECT * FROM Posts").ToList<dynamic>();

        public IList<IPost> GetAllItemsAsObject() => connection.QueryAll<Post>().ToList<IPost>();

        public dynamic GetItemAsDynamic(int Id) => connection.Query<Post>(x => x.Id == Id).First();

        public IPost GetItemAsObject(int Id) => connection.Query<Post>(x => x.Id == Id).First();

        public void Init(string connectionStrong)
        {
            connection = new SqlConnection(connectionStrong);
            SqlServerBootstrap.Initialize();
        }
    }
}