using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMToolkit.Core;

namespace OrmBenchmark.OrmToolkit
{
    public class OrmToolkitDynamicExecuter : IOrmExecuter
    {
        SqlConnection conn;

        public string Name
        {
            get
            {
                return "OrmToolkit Dynamic";
            }
        }

        public void Init(string connectionStrong)
        {
            conn = new SqlConnection(connectionStrong);
            conn.Open();
        }

        public object GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.Query("select * from Posts where Id=@Id", param).First();
        }

        public object GetItems(string Id)
        {
            object param = new { Id = Id };
            return conn.Query("select * from Posts", null).ToList();
        }

        public void Finish()
        {
            conn.Close();
        }

    }
}
