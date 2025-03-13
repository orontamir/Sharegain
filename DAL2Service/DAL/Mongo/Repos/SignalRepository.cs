using DAL2Service.DAL.Mongo.Entities;
using DAL2Service.DAL.Mongo.Interfaces;
using DAL2Service.Interfaces;
using MongoDB.Driver;
using Serilog;

namespace DAL2Service.DAL.Mongo.Repos
{
    public class SignalRepository : ISignalRepository
    {
        readonly MongoDb<MongoSignalEntity> _Db;

        public SignalRepository(IMultipleDataBaseSettings settings)
        {
            try
            {
                var mongoClient = new MongoClient(settings.MainDB.ConnectionString);
                _Db = new MongoDb<MongoSignalEntity>(mongoClient, settings.MainDB.DatabaseName, "Signals");

            }
            catch (Exception e)
            {
                Log.Error($"Mongo Connection String getting an error message: {e.Message}");
                throw;
            }

        }
        private const string CounterId = "LogoMessageCounter"; public Task DeleteAsync(MongoSignalEntity identity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MongoSignalEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MongoSignalEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MongoSignalEntity> InsertAsync(MongoSignalEntity record)
        {
            throw new NotImplementedException();
        }

        public Task<List<MongoSignalEntity>> InsertMultipleAsync(List<MongoSignalEntity> records)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(MongoSignalEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
