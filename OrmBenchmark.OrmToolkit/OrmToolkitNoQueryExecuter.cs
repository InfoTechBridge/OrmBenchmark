using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMToolkit.Core;
using ORMToolkit.Core.Factories;

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
                DataProviderFactory = new ORMToolkit.Core.Factories.SqlServer.SqlServerFactory(),
                ObjectFactoryType = ObjectFactoryType.ReflectionFastCache,
                DynamicObjectFactory = new ExpandoDynamicObjectFactory(),
                //DynamicObjectFactory = new RickExpandoDynamicObjectFactory();// slower
                ParameterNamePrefixInQuery = '@',

                ObjectFactory = typeof(Reflection1ObjectFactory<>),
                ObjectFactory1 = new ObjectFactory2()
            };
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
