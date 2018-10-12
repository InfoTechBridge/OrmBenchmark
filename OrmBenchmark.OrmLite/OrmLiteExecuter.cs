using OrmBenchmark.Core;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OrmBenchmark.OrmLite
{
    public class OrmLiteExecuter : IOrmExecuter
    {
        IDbConnection conn;
        OrmLiteConnectionFactory dbFactory;

        public string Name
        {
            get
            {
                return "Orm Lite";
            }
        }

        public void Init(string connectionStrong)
        {
            dbFactory = new OrmLiteConnectionFactory(connectionStrong, SqlServerDialect.Provider);
            conn = dbFactory.Open();
        }

        public IPost GetItemAsObject(int Id)
        {
            object param = new { Id = Id };
            return conn.Single<Post>("select * from Posts where Id=@Id", param);
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            object param = new { Id = Id };
            return conn.Single<dynamic>("select * from Posts where Id=@Id", param);
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.Select<Post>("select * from Posts").ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return conn.Select<dynamic>("select * from Posts");
        }
        public void Finish()
        {
            conn.Close();
        }

    }
}
