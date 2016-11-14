﻿using OrmBenchmark.Core;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.PetaPoco
{
    public class PetaPocoFetchExecuter : IOrmExecuter
    {
        Database petapoco;

        public string Name
        {
            get
            {
                return "Peta Poco (Fetch)";
            }
        }

        public void Init(string connectionStrong)
        {
            petapoco = new Database(connectionStrong, "System.Data.SqlClient");
            petapoco.OpenSharedConnection();
        }

        public object GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return petapoco.Fetch<Post>("select * from Posts where Id=@0", Id).First();
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return petapoco.Fetch<Post>("select * from Posts where Id=@0", Id).First();
        }

        public IList<object> GetAllItemsAsObject()
        {
            return petapoco.Fetch<Post>("select * from Posts").ToList<object>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return petapoco.Fetch<Post>("select * from Posts").ToList<object>();
        }

        public void Finish()
        {
            //petapoco.Close();
        }

    }
}
