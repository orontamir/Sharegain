using LogoManager.DAL.Mongo.Entities;
using LogoManager.Interfaces;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace LogoManager.DAL.Mongo.Repos
{
    public class LogoMessageRepository : ILogoMessageRepository
    {
        readonly MongoDb<LogoMessageEntity> _Db;
        readonly MongoDb<CounterEntity> _CounterDb;
        private const string CounterId = "LogoMessageCounter";

        public LogoMessageRepository(IMultipleDatabaseSettings settings)
        {
            try
            {
                var mongoClient = new MongoClient(settings.MainDB.ConnectionString);
                _Db = new MongoDb<LogoMessageEntity>(mongoClient, settings.MainDB.DatabaseName, "LogoMessages");
                _CounterDb = new MongoDb<CounterEntity>(mongoClient, settings.MainDB.DatabaseName, "Counters");

                var c = _CounterDb.GetOne(a => a.Id == CounterId);
                if (c == null)
                {
                    var counter = new CounterEntity();
                    counter.Id = CounterId;
                    counter.Value = 0;
                    _CounterDb.InsertOne(counter);
                }



            }
            catch (Exception e)
            {
                Log.Error($"Mongo Connection String getting an error message: {e.Message}");
                throw;
            }

        }

        public async Task DeleteAsync(LogoMessageEntity entity) => await _Db.DeleteOneAsync(u => u.Pid == entity.Pid);

       

        public async Task<IEnumerable<LogoMessageEntity>> GetAllAsync()
        {
            return await _Db.Query.ToListAsync();
        }

        public async Task<LogoMessageEntity> GetAsync(int id) => await _Db.GetOneAsync(u => u.Pid == id);

        public virtual async Task<LogoMessageEntity> InsertAsync(LogoMessageEntity entity)
        {
            if (entity.Pid != 0)
                return null;
            try
            {
               
                var c = _CounterDb.GetOne(a => a.Id == CounterId);
                c.Value++;
                await _CounterDb.ReplaceOneAsync(a => a.Id == c.Id, c);
                entity.Pid = c.Value;
                await _Db.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task UpdateAsync(LogoMessageEntity entity) =>
            await _Db.ReplaceOneAsync(e => e.Pid == entity.Pid, entity);

        public async Task<List<LogoMessageEntity>> InsertMultipleAsync(List<LogoMessageEntity> records)
        {
            List<LogoMessageEntity> res = new List<LogoMessageEntity>();
            foreach (var record in records)
            {
                res.Add(await InsertAsync(record));
            }
            return res;
        }
    }
}
