using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.EntityFramework
{
    public class EntityFrameworkExecuter : IOrmExecuter
    {
        OrmBenchmarkContext ctx;

        public string Name
        {
            get
            {
                return "Entity Framework";
            }
        }

        public void Init(string connectionStrong)
        {

            ctx = new OrmBenchmarkContext(connectionStrong);
            
        }

        public IPost GetItemAsObject(int Id)
        {
            return ctx.Posts.Where(p => p.Id == Id) as IPost;
             
        }

        public dynamic GetItemAsDynamic(int Id)
        {
            return null;
        }

        public IList<IPost> GetAllItemsAsObject()
        {
            return ctx.Posts.ToList<IPost>();
        }

        public IList<dynamic> GetAllItemsAsDynamic()
        {
            return null;
        }
        public void Finish()
        {
            
        }
    }
}
