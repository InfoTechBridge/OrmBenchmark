using Dapper.Contrib.Extensions;
using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OrmBenchmark.Dapper
{
    public class DapperContribExecuter : IOrmExecuter
    {
        SqlConnection conn;

        public string Name
        {
            get
            {
                return "Dapper Contrib";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();
        }

        public IPost GetItemAsObject(int Id)
        {
            return conn.Get<Post>(Id);
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            return null;
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.GetAll<Post>().ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return null;
        }

        public void Finish()
        {
            conn.Close();
        }
    }
}
