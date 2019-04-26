using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OrmBenchmark.DevExpress
{
    public class DevExpressQueryExecuter : IOrmExecuter
    {
        //IDbConnection conn;
        //IDataLayer dbFactory;
        UnitOfWork uow;

        public string Name
        {
            get
            {
                return "DevExpress Xpo";
            }
        }

        public void Init(string connectionStrong)
        {
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionStrong, AutoCreateOption.SchemaAlreadyExists);
            uow = new UnitOfWork();
        }

        public IPost GetItemAsObject(int Id)
        {

            return uow.Query<Post>().Where(i => i.Id == Id).First();
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            var q = uow.Query<Post>()
                .Where(i => i.Id == Id)
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

            return q.Single<dynamic>();
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return uow.Query<Post>().ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            var q = uow.Query<Post>()
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

            return q.ToList<dynamic>();
        }
        public void Finish()
        {
            
        }

    }
}
