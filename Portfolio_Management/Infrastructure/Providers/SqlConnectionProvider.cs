using Microsoft.Extensions.Configuration;
using Npgsql;
using Portfolio_Management.Infrastructure.Providers.Interface;

namespace Portfolio_Management.Infrastructure.Providers
{
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NpgsqlConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        }
    }
}