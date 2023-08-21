using Npgsql;
using System.Data;

namespace DapperRelization.Context
{
    public class DbConnectionFactory: IDbConnectionFactory, IDisposable
    {
        private readonly string? _connectionString;

        public DbConnectionFactory(string? connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException($"{nameof(connectionString)} is null");
        }

        private NpgsqlConnection _connection;

        public IDbConnection CreateConnection()
        {
            if(_connection == null || _connection?.State == ConnectionState.Closed)
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();

                return _connection;
            }
            else if(_connection?.State == ConnectionState.Open)
            {
                return _connection;
            }

            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
