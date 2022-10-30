using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workers_Manager.Models;
using Task = Workers_Manager.Models.Task;

namespace Workers_Manager.Tools
{
    internal class DataContext : DbContext
    {
        private const string connectionString =
            "Data Source=KINGA;Initial Catalog=DbWorkers;Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
            "TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DbSet<Worker>? Workers { get; set; }
        public DbSet<Task>? Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
