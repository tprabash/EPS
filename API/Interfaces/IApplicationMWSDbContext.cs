using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.MWS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Interfaces
{
    public interface IApplicationMWSDbContext
    {
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<ErrorLog> ErrorLog {get; set;}   
    }
}

