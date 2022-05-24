
using Microsoft.Extensions.Configuration;

namespace CommunicationWithDB.Common
{
    /// <summary>
    /// Class that provide configuration for connections for db.
    /// </summary>
    public class DbConfiguration
    {
        private readonly IConfiguration _configuration;

        public DbConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

            _configuration = builder.Build();
        }

        /// <summary>
        /// Method that return connection string by provided name.
        /// </summary>
        /// <param name="name">Name of connection.</param>
        /// <returns></returns>
        public string GetConnectionString(string name)
        {
            var connectionString = _configuration.GetConnectionString(name);

            return connectionString;
        }
    }
}
