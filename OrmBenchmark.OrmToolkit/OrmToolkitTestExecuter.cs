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
    public class OrmToolkitTestExecuter : IOrmExecuter
    {
        SqlConnection conn;
        QueryOption option;

        public string Name
        {
            get
            {
                return "OrmToolkit (Beta)";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();

            option = new QueryOption()
            {

            };

            OrmToolkitSettings.ObjectFactory = new ObjectFactory3();
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
            return null;
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.Query<Post>("select * from Posts", null, option).ToList<IPost>();
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
