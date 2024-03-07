using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MonthlyCosts.Infra.Data.SqlServer
{
    public static class DbContext
    {
        public static IDbConnection CreateConnection(IConfiguration configuration)
            => new SqlConnection(configuration.GetConnectionString("DbConnection"));
    }
}
