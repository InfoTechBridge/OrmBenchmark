using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.Core
{
    public class BenchmarkResult
    {
        public string Name { get; set; }
        public TimeSpan ExecTime { get; set; }
    }
}
