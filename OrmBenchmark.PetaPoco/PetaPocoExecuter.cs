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
    public class PetaPocoExecuter : IOrmExecuter
    {
        Database petapoco;

        public string Name
        {
            get
            {
                return "PetaPoco";
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
            return petapoco.Query<Post>("select * from Posts where Id=@0", Id).First();
        }
        
        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return petapoco.Fetch<dynamic>("select * from Posts where Id=@0", Id).First();
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return petapoco.Query<Post>("select * from Posts").ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return petapoco.Query<dynamic>("select * from Posts").ToList();
        }

        public void Finish()
        {
            //petapoco.Close();
        }

    }
}
