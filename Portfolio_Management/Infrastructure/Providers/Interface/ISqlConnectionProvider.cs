using Npgsql;

namespace Portfolio_Management.Infrastructure.Providers.Interface
{
    public interface ISqlConnectionProvider
    {
        NpgsqlConnection GetConnection();
    }
}