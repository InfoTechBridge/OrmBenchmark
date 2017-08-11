using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMToolkit.Core;
using ORMToolkit.Core.Factories;
using ORMToolkit.Core.CacheProvider;

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
                return "OrmToolkit (Test)";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();

            option = new QueryOption()
            {
                //DataProviderFactory = new ORMToolkit.Core.Factories.SqlServer.SqlServerFactory(),
                //ObjectFactoryType = ObjectFactoryType.ReflectionFastCache,
                //DynamicObjectFactory = new ExpandoDynamicObjectFactory(),
                ////DynamicObjectFactory = new RickExpandoDynamicObjectFactory();// slower
                //ParameterNamePrefixInQuery = '@',

                //ObjectFactory = typeof(Reflection1ObjectFactory<>),
                //ObjectFactory1 = new ObjectFactory3()
            };

            OrmToolkitSettings.ObjectFactory = new ObjectFactory3();

            OrmToolkitSettings.CommandsCache = new HashsetInstanceCache(); //ObjectInstanceCache();
            OrmToolkitSettings.TypesCache = new HashsetInstanceCache(); //ObjectInstanceCache();

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
