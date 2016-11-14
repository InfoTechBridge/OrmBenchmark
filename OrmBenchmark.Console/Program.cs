using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = ConfigurationManager.ConnectionStrings["OrmBenchmark.ConsoleUI.Properties.Settings.OrmBenchmarkConnectionString"].ConnectionString;
            //string connStr = ConfigurationManager.ConnectionStrings["sqlServerLocal"].ConnectionString;

            var benchmarker = new Benchmarker(connStr, 500);
            
            benchmarker.RegisterOrmExecuter(new PureAdoExecuter());
            //benchmarker.RegisterOrmExecuter(new PureAdoExecuterGetValues());
            benchmarker.RegisterOrmExecuter(new SimpleData.SimpleDataExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperBufferedExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperDynamicExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFastExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFetchExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFetchFastExecuter());
            benchmarker.RegisterOrmExecuter(new OrmToolkit.OrmToolkitExecuter());
            benchmarker.RegisterOrmExecuter(new OrmToolkit.OrmToolkitDynamicExecuter());
            

            Console.Write("Running...");
            benchmarker.Run();
            Console.WriteLine("Finished.");

            Console.WriteLine("\nResults for one item:");
            int i = 0;
            foreach (var result in benchmarker.results.OrderBy(o => o.ExecTime))
                Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms", ++i, result.Name, result.ExecTime));

            Console.WriteLine("\nResults for all items:");
            i = 0;
            foreach (var result in benchmarker.resultsForAllItems.OrderBy(o => o.ExecTime))
                Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms", ++i, result.Name, result.ExecTime));

            Console.ReadLine();
        }
    }
}
