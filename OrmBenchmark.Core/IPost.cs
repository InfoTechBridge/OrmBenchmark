using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.Core
{
    public interface IPost
    {
        int Id { get; set; }
        string Text { get; set; }
        DateTime CreationDate { get; set; }
        DateTime LastChangeDate { get; set; }
        int? Counter1 { get; set; }
        int? Counter2 { get; set; }
        int? Counter3 { get; set; }
        int? Counter4 { get; set; }
        int? Counter5 { get; set; }
        int? Counter6 { get; set; }
        int? Counter7 { get; set; }
        int? Counter8 { get; set; }
        int? Counter9 { get; set; }
    }
}
