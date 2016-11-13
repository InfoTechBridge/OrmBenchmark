﻿using OrmBenchmark.Core;
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
                DataProviderFactory = new ORMToolkit.Core.Factories.SqlServer.SqlServerFactory(),
                ObjectFactoryType = ObjectFactoryType.ReflectionFast,
                DynamicObjectFactory = new ExpandoDynamicObjectFactory(),
                //DynamicObjectFactory = new RickExpandoDynamicObjectFactory();// slower
                ParameterNamePrefixInQuery = '@',
            };
        }

        public object GetItem(int Id)
        {
            object param = new { Id = Id };
            return conn.Query<Post>("select * from Posts where Id=@Id", param, option).First();
        }

        public object GetItems(string Id)
        {
            object param = new { Id = Id };
            return conn.Query<Post>("select * from Posts", null, option).ToList();
        }

        public void Finish()
        {
            conn.Close();
        }

    }
}