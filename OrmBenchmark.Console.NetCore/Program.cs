using Microsoft.Extensions.Configuration;
using OrmBenchmark.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;

namespace OrmBenchmark.ConsoleUI.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
                        
            string connStringName = configuration.GetValue<string>("DefaultConnectionStringName");
            string connStr = configuration.GetConnectionString(connStringName);

            // Set up data directory
            string runDir = System.AppContext.BaseDirectory;
            //string runDir = AppDomain.CurrentDomain.BaseDirectory;
            connStr = connStr.Replace("|DataDirectory|", runDir.TrimEnd('\\'));

            bool warmUp = false;

            var benchmarker = new Benchmarker(connStr, 500);

            benchmarker.RegisterOrmExecuter(new Ado.PureAdoExecuter());
            //benchmarker.RegisterOrmExecuter(new Ado.PureAdoExecuterGetValues());
            //benchmarker.RegisterOrmExecuter(new SimpleData.SimpleDataExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperBufferedExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperFirstOrDefaultExecuter());
            benchmarker.RegisterOrmExecuter(new Dapper.DapperContribExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFastExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFetchExecuter());
            benchmarker.RegisterOrmExecuter(new PetaPoco.PetaPocoFetchFastExecuter());
            benchmarker.RegisterOrmExecuter(new OrmToolkit.OrmToolkitExecuter());
            benchmarker.RegisterOrmExecuter(new OrmToolkit.OrmToolkitNoQueryExecuter());
            //benchmarker.RegisterOrmExecuter(new OrmToolkit.OrmToolkitAutoMapperExecuter());
            benchmarker.RegisterOrmExecuter(new OrmToolkit.OrmToolkitTestExecuter());
            //benchmarker.RegisterOrmExecuter(new EntityFramework.EntityFrameworkExecuter());
            benchmarker.RegisterOrmExecuter(new EntityFramework.EntityFrameworNoTrackingExecuter());
            benchmarker.RegisterOrmExecuter(new InsightDatabase.InsightDatabaseExecuter());
            benchmarker.RegisterOrmExecuter(new InsightDatabase.InsightSingleDatabaseExecuter());
            benchmarker.RegisterOrmExecuter(new OrmLite.OrmLiteExecuter());
            benchmarker.RegisterOrmExecuter(new OrmLite.OrmLiteNoQueryExecuter());
            //benchmarker.RegisterOrmExecuter(new DevExpress.DevExpressQueryExecuter());

            Console.WriteLine("ORM Benchmark");

            Console.Write("\nDo you like to have a warm-up stage(y/[n])?");
            var str = Console.ReadLine();
            if (str.Trim().ToLower() == "y" || str.Trim().ToLower() == "yes")
                warmUp = true;

            var ver = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
            Console.WriteLine(ver);
            Console.WriteLine("Connection string: {0}", connStr);
            Console.Write("\nRunning...");
            benchmarker.Run(warmUp);
            Console.WriteLine("Finished.");

            Console.ForegroundColor = ConsoleColor.Red;

            if (warmUp)
            {
                Console.WriteLine("\nPerformance of Warm-up:");
                ShowResults(benchmarker.resultsWarmUp, false, false);
            }

            Console.WriteLine("\nPerformance of select and map a row to a POCO object over 500 iterations:");
            ShowResults(benchmarker.results, true);

            Console.WriteLine("\nPerformance of select and map a row to a Dynamic object over 500 iterations:");
            ShowResults(benchmarker.resultsForDynamicItem, true);

            Console.WriteLine("\nPerformance of mapping 5000 rows to POCO objects in one iteration:");
            ShowResults(benchmarker.resultsForAllItems);

            Console.WriteLine("\nPerformance of mapping 5000 rows to Dynamic objects in one iteration:");
            ShowResults(benchmarker.resultsForAllDynamicItems);

            Console.ReadLine();
        }

        static void ShowResults(List<BenchmarkResult> results, bool showFirstRun = false, bool ignoreZeroTimes = true)
        {
            var defaultColor = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Gray;

            int i = 0;
            var list = results.OrderBy(o => o.ExecTime);
            if (ignoreZeroTimes)
                list = results.FindAll(o => o.ExecTime > 0).OrderBy(o => o.ExecTime);

            foreach (var result in list)
            {
                Console.ForegroundColor = i < 3 ? ConsoleColor.Green : ConsoleColor.Gray;

                if (showFirstRun)
                    Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms (First run: {3,3} ms)", ++i, result.Name, result.ExecTime, result.FirstItemExecTime));
                else
                    Console.WriteLine(string.Format("{0,2}-{1,-40} {2,5} ms", ++i, result.Name, result.ExecTime));
            }

            Console.ForegroundColor = defaultColor;
        }
    }
}
