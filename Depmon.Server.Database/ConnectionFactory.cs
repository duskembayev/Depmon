using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Depmon.Server.Database
{
    public class ConnectionFactory
    {
        public static ConnectionFactory Instance { get; } = new ConnectionFactory();

        public IDbConnection Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["depmon"];
            if (connectionString == null)
            {
                throw new ApplicationException("Connection string 'depmon' not found");
            }

            var connection = new SQLiteConnection(connectionString.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
