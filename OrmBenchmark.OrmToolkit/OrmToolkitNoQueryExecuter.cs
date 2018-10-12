using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ORMToolkit.Core;
using ORMToolkit.Core.Factories;
using ORMToolkit.Core.CacheProvider;
using OrmBenchmark.Core;

namespace OrmBenchmark.OrmToolkit
{
    public class OrmToolkitNoQueryExecuter : IOrmExecuter
    {
        SqlConnection conn;
        QueryOption option;

        public string Name
        {
            get
            {
                return "OrmToolkit (No Query)";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();

            option = new QueryOption()
            {

            };

            OrmToolkitSettings.ObjectFactory = new ObjectFactory2();

            OrmToolkitSettings.CommandsCache = new HashsetInstanceCache();
            OrmToolkitSettings.TypesCache = new HashsetInstanceCache();
        }

        public IPost GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.Get<Post>(param, option);
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return conn.Get("Posts", param);
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.GetAll<Post>(null, null, option).ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return conn.GetAll("Posts", null, null).ToList();
        }
        public void Finish()
        {
            conn.Close();
        }

    }
}
