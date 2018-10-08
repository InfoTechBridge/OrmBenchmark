using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark.EntityFramework
{
    class OrmBenchmarkContext : DbContext
    {
        public OrmBenchmarkContext(string connectionStrong) 
            //: base(GetSqlConnection(connectionStrong), true)
            //: base("name=sqlServerLocal")
            : base(connectionStrong)
        {
            

        }

        public DbSet<Post> Posts { get; set; }

        private static DbConnection GetSqlConnection(string connectionStrong)
        {
            // Initialize the EntityConnectionStringBuilder. 
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

            // Set the provider name. 
            entityBuilder.Provider = "System.Data.SqlClient";

            // Set the provider-specific connection string. 
            entityBuilder.ProviderConnectionString = connectionStrong;

            // Set the Metadata location. 
            //entityBuilder.Metadata = "res://*/Models.TestModel.csdl|res://*/Models.TestModel.ssdl|res://*/Models.TestModel.msl";

            return new EntityConnection(entityBuilder.ToString());
        }
    }
}
