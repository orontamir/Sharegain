using LogoManager.DAL.Mongo.Entities;
using LogoManager.Models;

namespace LogoManager.Extensions
{
    public static class LogoMessageExtension
    {
        public static LogoMessageEntity ToEntity(this LogoMessageModel model)
        {
            if (model == null)
                return new LogoMessageEntity();

            return new LogoMessageEntity
            {
                time = model.time,
                clientInfo = model.clientInfo,
                message = model.message,
            };
        }
    }
}
