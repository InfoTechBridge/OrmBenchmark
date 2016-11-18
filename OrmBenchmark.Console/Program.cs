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
            
            benchmarker.RegisterOrmExecuter(new Ado.PureAdoExecuter());
            //benchmarker.RegisterOrmExecuter(new Ado.PureAdoExecuterGetValues());
            benchmarker.RegisterOrmExecuter(new SimpleData.SimpleDataExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperBufferedExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFastExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFetchExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFetchFastExecuter());
            benchmarker.RegisterOrmExecuter(new OrmToolkit.OrmToolkitExecuter());

            Console.Write("Running...");
            benchmarker.Run();
            Console.WriteLine("Finished.");

            Console.WriteLine("\nPerformance of select and map a row to a POCO object over 500 iterations:");
            int i = 0;
            foreach (var result in benchmarker.results.OrderBy(o => o.ExecTime))
                Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms", ++i, result.Name, result.ExecTime));

            Console.WriteLine("\nPerformance of select and map a row to a Dynamic object over 500 iterations:");
            i = 0;
            foreach (var result in benchmarker.resultsForDynamicitem.OrderBy(o => o.ExecTime))
                Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms", ++i, result.Name, result.ExecTime));

            Console.WriteLine("\nPerformance of mapping 5000 rows to POCO objects in one iteration:");
            i = 0;
            foreach (var result in benchmarker.resultsForAllItems.OrderBy(o => o.ExecTime))
                Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms", ++i, result.Name, result.ExecTime));

            Console.WriteLine("\nPerformance of mapping 5000 rows to Dynamic objects in one iteration:");
            i = 0;
            foreach (var result in benchmarker.resultsForAllDynamicItems.OrderBy(o => o.ExecTime))
                Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms", ++i, result.Name, result.ExecTime));

            Console.ReadLine();
        }
    }
}
