using OrmBenchmark.Core;
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

        public object GetItem(int Id)
        {
            object param = new { Id = Id };
            return petapoco.Fetch<Post>("select * from Posts where Id=@0", Id).First();
        }

        public object GetItems(string Id)
        {
            return petapoco.Fetch<Post>("select * from Posts");
        }

        public void Finish()
        {
            //petapoco.Close();
        }

    }
}
