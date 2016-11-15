using OrmBenchmark.Core;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.SimpleData
{
    public class SimpleDataExecuter : IOrmExecuter
    {
        dynamic sdb;

        public string Name
        {
            get
            {
                return "Simple.Data";
            }
        }

        public void Init(string connectionStrong)
        {
            sdb = Database.OpenConnection(connectionStrong);//, "System.Data.SqlClient");
        }

        public IPost GetItemAsObject(int Id)
        {
            return null;
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            return sdb.Posts.FindById(Id);
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return null;
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return sdb.Posts.All().ToList();
        }

        public void Finish()
        {
            //petapoco.Close();
        }

    }
}
