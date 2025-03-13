using Base.Services;
using DALService.DAL.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DALService.DAL.Sql
{
    public abstract class RepositoryBase
    {
        protected IConfiguration _configuration;
        public RepositoryBase(IConfiguration configuration) 
        {
            _configuration = configuration; 
        }

        protected virtual AppDbContext? CreateDbContext()
        {
            try
            {
                var connectionString = _configuration["DB_CONNECTION_STRING"];
                var serverVersion = new MySqlServerVersion(new Version(8, 0 , 17));
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseMySql(connectionString, serverVersion,
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 100,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                        )

                    )
                    .Options;
                return new AppDbContext( options );
            }
            catch (Exception ex) {
                LogService.LogError($"Exception when Create Db Context, Error messsage : {ex.Message}");
                return null;
            }
        }

        public AppDbContext GetDbCtx() => CreateDbContext();
        public async Task CreateOrUpdateAsync<TEntity>(TEntity entity) where TEntity : class 
        {
            await using var ctx = GetDbCtx();
            var table = ctx.Set<TEntity>();
            if (await table.ContainsAsync(entity)) 
            {
                table.Update(entity);
            }
            else
            {
                await table.AddAsync(entity);
            }

            await ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await using var ctx = GetDbCtx();
            var table = ctx.Set<TEntity>();
            table.Update(entity);
            await ctx.SaveChangesAsync();
            
        }

        public async Task DeleteAsync<TEntity>(TEntity id) where TEntity : class
        {
            await using var ctx = GetDbCtx();
            var table = ctx.Set<TEntity>();
            table.Remove(id);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<SignalEntity>> GetAllSignals()
        {
            await using var ctx = GetDbCtx();
            return await ctx.Signals.ToListAsync();
        }

        public async Task<IEnumerable<SignalEntity>> GetErrorSignal(int number)
        {
            await using var ctx = GetDbCtx();
            return ctx.Signals.Where(o => o.Error == true).Take(number).ToList<SignalEntity>();

        }

        public async Task<IEnumerable<SignalEntity>> GetSin(int number)
        {
            await using var ctx = GetDbCtx();
            return ctx.Signals.Where(o => o.Type == "Sin" && o.Error == false).OrderByDescending(x => x.TimeStemp).ToList<SignalEntity>();
        }

        public async Task<IEnumerable<SignalEntity>> GetState(int number)
        {
            await using var ctx = GetDbCtx();
            return ctx.Signals.Where(o => o.Type == "State" & o.Error == false).OrderByDescending(x => x.TimeStemp).ToList<SignalEntity> ();
        }



    }
}
