using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Kubernox.Infrastructure.Core.Persistence
{
    public class SqlDbConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly SqlConnection sqlConnection;

        public SqlDbConnectionFactory(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("db", "dbBinding") 
                ?? configuration.GetConnectionString("Default");
            this.sqlConnection = new SqlConnection(connectionString);
        }

        public IDbConnection GetConnection()
        {
            return sqlConnection;
        }
    }

}
