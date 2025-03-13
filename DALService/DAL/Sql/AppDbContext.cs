using DALService.DAL.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace DALService.DAL.Sql
{
    public class AppDbContext: DbContext
    {
        public DbSet<SignalEntity> Signals {  get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
    }
}
