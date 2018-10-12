using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrmBenchmark.Core;
using PetaPoco;

namespace OrmBenchmark.PetaPoco
{
    public class PetaPocoFetchExecuter : IOrmExecuter
    {
        Database petapoco;

        public string Name
        {
            get
            {
                return "PetaPoco (Fetch)";
            }
        }

        public void Init(string connectionStrong)
        {
            petapoco = new Database(connectionStrong, "System.Data.SqlClient");
            petapoco.OpenSharedConnection();
        }

        public IPost GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return petapoco.Fetch<Post>("select * from Posts where Id=@0", Id).First();
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return petapoco.Fetch<Post>("select * from Posts where Id=@0", Id).First();
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return petapoco.Fetch<Post>("select * from Posts").ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return petapoco.Fetch<dynamic>("select * from Posts");
        }

        public void Finish()
        {
            //petapoco.Close();
        }

    }
}
