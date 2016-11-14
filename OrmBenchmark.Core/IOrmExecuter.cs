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
        object GetItemAsObject(int Id);
        dynamic GetItemAsDynamic(int Id);
        IList<object> GetAllItemsAsObject();
        IList<dynamic> GetAllItemsAsDynamic();
        void Finish();
    }
}
