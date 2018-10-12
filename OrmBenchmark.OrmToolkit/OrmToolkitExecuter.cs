using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ORMToolkit.Core;
using ORMToolkit.Core.Factories;
using ORMToolkit.Core.CacheProvider;
using System.Linq;

namespace OrmBenchmark.OrmToolkit
{
    public class OrmToolkitExecuter : IOrmExecuter
    {
        SqlConnection conn;
        QueryOption option;

        public string Name
        {
            get
            {
                return "OrmToolkit";
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
            // Use fresh caches
            OrmToolkitSettings.CommandsCache = new HashsetInstanceCache();
            OrmToolkitSettings.TypesCache = new HashsetInstanceCache();
        }

        public IPost GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.Query<Post>("select * from Posts where Id=@Id", param, option).First();
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return conn.Query("select * from Posts where Id=@Id", param).First();
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.Query<Post>("select * from Posts", null, option).ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return conn.Query("select * from Posts", null).ToList();
        }
        public void Finish()
        {
            conn.Close();
        }

    }
}
