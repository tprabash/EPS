using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.MTrack;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Data
{
    public class ApplicationMTrackDbContext : DbContext, IApplicationMTrackDbContext
    {
        public ApplicationMTrackDbContext(DbContextOptions<ApplicationMTrackDbContext> options) : base(options)
        {

        }

        public IDbConnection Connection => Database.GetDbConnection();

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}