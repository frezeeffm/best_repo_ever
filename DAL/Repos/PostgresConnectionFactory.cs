using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace DAL.Repos
{
    public class PostgresConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        
        public PostgresConnectionFactory(string connectionString) => _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var sqlConnection = new NpgsqlConnection(_connectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            await sqlConnection.OpenAsync().ConfigureAwait(false);
            return sqlConnection;
        }
    }
}