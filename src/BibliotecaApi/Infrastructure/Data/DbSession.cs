using Microsoft.Data.Sqlite;
using System.Data;

namespace BibliotecaApi.Infrastructure.Data
{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");

            bool connectionStringInvalida = string.IsNullOrWhiteSpace(connectionString);
            if (connectionStringInvalida)
            {
                throw new InvalidOperationException("Connection string 'Database' não encontrada.");
            }

            Connection = new SqliteConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Connection.Dispose();
        }
    }
}