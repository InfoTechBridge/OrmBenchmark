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

        public object GetItem(int Id)
        {
            return sdb.Posts.FindById(Id);
        }

        public object GetItems(string Id)
        {
            return sdb.Posts.All().ToList();
        }

        public void Finish()
        {
            //petapoco.Close();
        }

    }
}
