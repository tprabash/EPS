using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.MTrack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Interfaces
{
    public interface IApplicationMTrackDbContext
    {
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
   


    }
}