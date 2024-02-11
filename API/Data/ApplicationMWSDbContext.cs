using System.Data;
using System.Threading;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.MWS;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Data
{
    public class ApplicationMWSDbContext : DbContext, IApplicationMWSDbContext
    {
        public ApplicationMWSDbContext(DbContextOptions<ApplicationMWSDbContext> options) : base(options)
        {

        }

        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<ErrorLog> ErrorLog {get; set;} 

    
    }
}
