using System.Data;
using API.Entities;
using API.Entities.Admin;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationAdminDbContext : DbContext, IApplicationAdminDbContext
    {
        public ApplicationAdminDbContext(DbContextOptions<ApplicationAdminDbContext> options) : base(options)
        {
        }

        public IDbConnection Connection => Database.GetDbConnection();        
        public DbSet<MstrAgents> MstrAgents { get; set; }               
        public DbSet<MstrAgentLevel> MstrAgentLevel {get; set;}
        public DbSet<MstrAgentModule> MstrAgentModule {get; set;}
        public DbSet<SystemModule> SystemModule {get; set;}
        

        
    }
}