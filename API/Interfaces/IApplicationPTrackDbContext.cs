using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Ptrack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Interfaces
{
    public interface IApplicationPTrackDbContext
    {
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<ErrorLog> ErrorLog {get; set;} 
        DbSet<rm_RMPOS_headers> RMPOSheaders {get; set;} 
        DbSet<rm_RMPOs> rmRMPOs {get; set;} 
        DbSet<Master_OperationGroup> Master_OperationGroup {get; set;}
        DbSet<Master_Section> MstrSection {get; set;}     
        DbSet<Master_MachineType> MstrMachineType {get; set;}       
        DbSet<MstrEmployee> MstrEmployee { get; set; }
        DbSet<Master_Operation> Master_Operation {get; set;}

    }
}