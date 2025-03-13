using DAL2Service.DAL.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL2Service.DAL.Sql
{
    public class AppDbContext : DbContext
    {
        public DbSet<SignalEntity> Signals { get; set; }

        public DbSet<UserEntity> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
