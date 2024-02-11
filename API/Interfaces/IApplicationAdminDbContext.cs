using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Interfaces
{
    public interface IApplicationAdminDbContext
    {
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<MstrAgents> MstrAgents { get; set; } 
        DbSet<MstrAgentLevel> MstrAgentLevel {get; set;}
        DbSet<MstrAgentModule> MstrAgentModule {get; set;}
        DbSet<SystemModule> SystemModule {get; set;}
        
    }
}