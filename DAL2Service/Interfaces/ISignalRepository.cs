using DAL2Service.DAL.Mongo.Entities;

namespace DAL2Service.Interfaces
{
    public interface ISignalRepository
    {
        Task<MongoSignalEntity> GetAsync(int id);
        Task<IEnumerable<MongoSignalEntity>> GetAllAsync();
        Task<MongoSignalEntity> InsertAsync(MongoSignalEntity record);
        Task<List<MongoSignalEntity>> InsertMultipleAsync(List<MongoSignalEntity> records);
        Task UpdateAsync(MongoSignalEntity entity);
        Task DeleteAsync(MongoSignalEntity identity);
    }
}
