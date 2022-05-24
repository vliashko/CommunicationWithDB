using CommunicationWithDB.Common;
using CommunicationWithDB.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunicationWithDB.EF
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() { }
        public UniversityContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Student { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Coursework> Coursework { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfig = new DbConfiguration();

            optionsBuilder.UseSqlServer(dbConfig.GetConnectionString("connString"));
        }
    }
}
