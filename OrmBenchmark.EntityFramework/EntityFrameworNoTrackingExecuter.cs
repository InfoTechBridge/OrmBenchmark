#if NETSTANDARD
using Microsoft.EntityFrameworkCore;
#endif
using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrmBenchmark.EntityFramework
{
    public class EntityFrameworNoTrackingExecuter : IOrmExecuter
    {
        OrmBenchmarkContext ctx;

        public string Name
        {
            get
            {
                return "Entity Framework (NoTracking)";
            }
        }

        public void Init(string connectionStrong)
        {
            ctx = new OrmBenchmarkContext(connectionStrong);
        }

        public IPost GetItemAsObject(int Id)
        {
            return ctx.Posts.AsNoTracking().Where(p => p.Id == Id) as IPost;

        }

        public dynamic GetItemAsDynamic(int Id)
        {
            return ctx.Posts.AsNoTracking().Where(p => p.Id == Id).Select(p => new {
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
                p.Counter9,
            });
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return ctx.Posts.AsNoTracking().ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return ctx.Posts.AsNoTracking().Select(p => new {
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
                p.Counter9,
            }).ToList<dynamic>();
        }
        public void Finish()
        {

        }
    }
}
