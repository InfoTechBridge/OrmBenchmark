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
    public class PetaPocoFetchFastExecuter : IOrmExecuter
    {
        Database petapoco;

        public string Name
        {
            get
            {
                return "Peta Poco (Fetch Fast)";
            }
        }

        public void Init(string connectionStrong)
        {
            petapoco = new Database(connectionStrong, "System.Data.SqlClient");
            petapoco.OpenSharedConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            //petapoco.ForceDateTimesToUtc = false;
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
