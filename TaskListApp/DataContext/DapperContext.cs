using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace TaskListApp.DataContext
{
    public sealed class DapperContext : IDapperContext
    {

        private readonly string? connectionString;

        public DapperContext(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("ConnectionStrings")["connection"];        
        }

        public DbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
