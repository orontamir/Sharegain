using DAL2Service.DAL.Sql.Entities;

namespace DAL2Service.Models.Extensions
{
    public static class SignalExtensions
    {
        public static SignalEntity ToEntity(this SignalDbModel signal)
        {
            return new SignalEntity
            {
                Id = signal.Id,
                Date = signal.Date,
                Error = signal.Error,
                Time = signal.Time,
                TimeStemp = signal.TimeStemp,
                Type = signal.Type,
                Value = signal.Value,
            };
        }

        public static SignalDbModel ToModel(this SignalEntity signal)
        {
            return new SignalDbModel
            {
                Id = signal.Id,
                Date = signal.Date,
                Error = signal.Error,
                Time = signal.Time,
                TimeStemp = signal.TimeStemp,
                Type = signal.Type,
                Value = signal.Value,
            };
        }
    }
}
