using System.Data;
using API.Interfaces;

namespace API.Data
{
    public class DbConnMWSRepositoryBase
    {
        public IDbConnection DbConnection { get; private set; }
        public DbConnMWSRepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
            // Now it's the time to pick the right connection string!
            // Enum is used. No magic string!
            this.DbConnection = dbConnectionFactory.CreateDbConnection(DatabaseConnectionName.MWSDbConnection);
        }
    }
}

