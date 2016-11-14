using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.Core
{
    public class Benchmarker
    {
        private List<IOrmExecuter> executers { get; set; }
        public List<BenchmarkResult> results { get; set; }
        public List<BenchmarkResult> resultsForAllItems { get; set; }
        public List<BenchmarkResult> resultsForDynamicitem { get; set; }
        public List<BenchmarkResult> resultsForAllDynamicItems { get; set; }
        private int IterationCount { get; set; }
        private string ConnectionString { get; set; }

        public Benchmarker(string connectionString, int iterationCount)
        {
            ConnectionString = connectionString;
            IterationCount = iterationCount;
            executers = new List<IOrmExecuter>();
            results = new List<BenchmarkResult>();
            resultsForDynamicitem = new List<BenchmarkResult>();
            resultsForAllItems = new List<BenchmarkResult>();
            resultsForAllDynamicItems = new List<BenchmarkResult>();
        }

        public void RegisterOrmExecuter(IOrmExecuter executer)
        {
            executers.Add(executer);
        }

        public void Run()
        {
            PrepareDatabase();

            results.Clear();
            resultsForAllItems.Clear();

            var rand = new Random();
            foreach (IOrmExecuter executer in executers.OrderBy(ignore => rand.Next()))
            {
                executer.Init(ConnectionString);
                // Object
                Stopwatch watch = new Stopwatch();
                for (int i = 1; i <= IterationCount; i++)
                {
                    watch.Start();
                    dynamic obj = executer.GetItemAsObject(i);
                    watch.Stop();
                    //if (obj.Id != i)
                    //    throw new ApplicationException("Invalid object returned.");
                }
                results.Add(new BenchmarkResult { Name = executer.Name, ExecTime = watch.ElapsedMilliseconds });
                
                // Dynamic
                Stopwatch watchForDynamic = new Stopwatch();
                for (int i = 1; i <= IterationCount; i++)
                {
                    watchForDynamic.Start();
                    dynamic obj = executer.GetItemAsDynamic(i);
                    watchForDynamic.Stop();
                    //if (obj.Id != i)
                    //    throw new ApplicationException("Invalid object returned.");
                }
                resultsForDynamicitem.Add(new BenchmarkResult { Name = executer.Name, ExecTime = watchForDynamic.ElapsedMilliseconds });
                
                // All Objects
                Stopwatch watchForAllItems = new Stopwatch();
                watchForAllItems.Start();
                executer.GetAllItemsAsObject();
                watchForAllItems.Stop();
                resultsForAllItems.Add(new BenchmarkResult { Name = executer.Name, ExecTime = watchForAllItems.ElapsedMilliseconds });

                // All Dynamics
                Stopwatch watchForAllDynamicItems = new Stopwatch();
                watchForAllDynamicItems.Start();
                executer.GetAllItemsAsDynamic();
                watchForAllDynamicItems.Stop();
                resultsForAllDynamicItems.Add(new BenchmarkResult { Name = executer.Name, ExecTime = watchForAllDynamicItems.ElapsedMilliseconds });

                executer.Finish();
            }
        }

        private void PrepareDatabase()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    if (OBJECT_ID('Posts') is null)
                    begin
	                    create table Posts
	                    (
		                    Id int identity primary key, 
		                    [Text] varchar(max) not null, 
		                    CreationDate datetime not null, 
		                    LastChangeDate datetime not null,
		                    Counter1 int,
		                    Counter2 int,
		                    Counter3 int,
		                    Counter4 int,
		                    Counter5 int,
		                    Counter6 int,
		                    Counter7 int,
		                    Counter8 int,
		                    Counter9 int
	                    )
	   
	                    set nocount on 

	                    declare @i int
	                    declare @c int
	                    declare @id int
	                    set @i = 0

	                    while @i <= 5001
	                    begin 
		                    insert Posts ([Text], CreationDate, LastChangeDate) values (replicate('x', 2000), GETDATE(), GETDATE())
		                    set @id = @@IDENTITY
		
		                    set @i = @i + 1
	                    end
                    end";

                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
