using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Ptrack;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Data
{
    public class ApplicationPTrackDbContext : DbContext, IApplicationPTrackDbContext
    {
        public ApplicationPTrackDbContext(DbContextOptions<ApplicationPTrackDbContext> options) : base(options)
        {
        }

        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<ErrorLog> ErrorLog {get; set;} 
        public DbSet<rm_RMPOS_headers> RMPOSheaders {get; set;} 
        public DbSet<rm_RMPOs> rmRMPOs {get; set;} 
        public DbSet<Master_OperationGroup> Master_OperationGroup {get; set;}
        public DbSet<Master_Section> MstrSection {get; set;}
        public DbSet<Master_MachineType> MstrMachineType {get; set;}
        //public DbSet<Master_Operation> MstrOperation {get; set;}
        public DbSet<Trans_ftywiseSections> TrsftywiseSections {get; set;}
        public DbSet<Trans_ftywiseOperations> TrsftywiseOperations {get; set;}
        public DbSet<MstrEmployee> MstrEmployee { get; set; }
        public DbSet<Master_Operation> Master_Operation {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<rm_RMPOs>().HasNoKey();
            // modelBuilder.Entity<rm_RMPOS_headers>().HasNoKey();
        }

    }
}