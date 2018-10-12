#if NETSTANDARD
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrmBenchmark.EntityFramework
{
    class OrmBenchmarkContext : DbContext
    {
        private string ConnectionStrong;

        public OrmBenchmarkContext(string connectionStrong)
        {
            ConnectionStrong = connectionStrong;
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrong);
        }
    }
}
#endif