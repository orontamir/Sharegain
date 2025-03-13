using LogoManager.DAL.Mongo.Entities;
using LogoManager.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogoManager.Services
{
    public class LogMessageService : ILogMessageService
    {
        public ILogoMessageRepository _LogMessageRepository { get; set; }

        public LogMessageService(ILogoMessageRepository logMessageRepository ) 
        {
            _LogMessageRepository = logMessageRepository;
        }

        
        public async Task<LogoMessageEntity> InsertLogMessage(LogoMessageEntity logMessage) => await _LogMessageRepository.InsertAsync(logMessage);

        public async Task<List<LogoMessageEntity>> InsertLogMessages(List<LogoMessageEntity> logMessages)  => await _LogMessageRepository.InsertMultipleAsync(logMessages);

        public async Task UpdateLogMessage(LogoMessageEntity logMessage) => await _LogMessageRepository.UpdateAsync(logMessage);
       
    }
}
