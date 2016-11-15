using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.Core
{
    public interface IOrmExecuter
    {
        string Name { get; }
        void Init(string connectionStrong);
        IPost GetItemAsObject(int Id);
        dynamic GetItemAsDynamic(int Id);
        IList<IPost> GetAllItemsAsObject();
        IList<dynamic> GetAllItemsAsDynamic();
        void Finish();
    }
}
