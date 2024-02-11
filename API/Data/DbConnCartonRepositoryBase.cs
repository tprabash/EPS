using System.Data;
using API.Interfaces;

namespace API.Data
{
    public class DbConnCartonRepositoryBase
    {
        public IDbConnection DbConnection { get; private set; }

        public DbConnCartonRepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
            this.DbConnection = dbConnectionFactory.CreateDbConnection(DatabaseConnectionName.CartonDbConnection);
        }
    }
}