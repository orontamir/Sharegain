using LogoManager.DAL.Mongo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogoManager.Interfaces
{
    public interface ILogoMessageRepository
    {
        Task<LogoMessageEntity> GetAsync(int id);
        Task<IEnumerable<LogoMessageEntity>> GetAllAsync();
        Task<LogoMessageEntity> InsertAsync(LogoMessageEntity record);
        Task<List<LogoMessageEntity>> InsertMultipleAsync(List<LogoMessageEntity> records);
        Task UpdateAsync(LogoMessageEntity entity);
        Task DeleteAsync(LogoMessageEntity identity);
    }
}
