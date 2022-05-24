using CommunicationWithDB.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CommunicationWithDB.EF
{
    public class UniversityContextFactory : IDesignTimeDbContextFactory<UniversityContext>
    {
        public UniversityContext CreateDbContext(string[] args)
        {
            var dbConfig = new DbConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            optionsBuilder.UseSqlServer(dbConfig.GetConnectionString("connString"));

            return new UniversityContext(optionsBuilder.Options);
        }
    }
}
