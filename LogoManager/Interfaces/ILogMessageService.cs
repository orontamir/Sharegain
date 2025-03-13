using LogoManager.DAL.Mongo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogoManager.Interfaces
{
    public interface ILogMessageService
    {
        Task<LogoMessageEntity> InsertLogMessage(LogoMessageEntity logMessage);
        Task<List<LogoMessageEntity>> InsertLogMessages(List<LogoMessageEntity> logMessages);
        Task UpdateLogMessage(LogoMessageEntity logMessage);
    }
}
