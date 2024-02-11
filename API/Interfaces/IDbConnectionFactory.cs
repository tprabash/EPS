using System.Data;
using API.Data;

namespace API.Interfaces
{
    public interface IDbConnectionFactory
    {
         IDbConnection CreateDbConnection(DatabaseConnectionName connectionName);
    }
}