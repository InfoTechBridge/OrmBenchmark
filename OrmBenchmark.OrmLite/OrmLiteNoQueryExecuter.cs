using OrmBenchmark.Core;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OrmBenchmark.OrmLite
{
    public class OrmLiteNoQueryExecuter : IOrmExecuter
    {
        IDbConnection conn;
        OrmLiteConnectionFactory dbFactory;

        public string Name
        {
            get
            {
                return "Orm Lite (No Query)";
            }
        }

        public void Init(string connectionStrong)
        {
            dbFactory = new OrmLiteConnectionFactory(connectionStrong, SqlServerDialect.Provider);
            conn = dbFactory.Open();
        }

        public IPost GetItemAsObject(int Id)
        {            
            return conn.SingleById<Post>(Id);
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            var q = conn.From<Post>()
                .Where(p => p.Id == Id)
                .Select(p => new
                {
                    p.Id,
                    p.Text,
                    p.CreationDate,
                    p.LastChangeDate,
                    p.Counter1,
                    p.Counter2,
                    p.Counter3,
                    p.Counter4,
                    p.Counter5,
                    p.Counter6,
                    p.Counter7,
                    p.Counter8,
                    p.Counter9
                });

            return conn.Single<dynamic>(q);
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return conn.Select<Post>().ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            var q = conn.From<Post>()
                .Select(p => new
                {
                    p.Id,
                    p.Text,
                    p.CreationDate,
                    p.LastChangeDate,
                    p.Counter1,
                    p.Counter2,
                    p.Counter3,
                    p.Counter4,
                    p.Counter5,
                    p.Counter6,
                    p.Counter7,
                    p.Counter8,
                    p.Counter9
                });

            return conn.Select<dynamic>(q);
        }
        public void Finish()
        {
            conn.Close();
        }

    }
}
