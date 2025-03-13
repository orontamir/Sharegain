using DAL2Service.DAL.Mongo.Settings;

namespace DAL2Service.DAL.Mongo.Interfaces
{
    public interface IMultipleDataBaseSettings
    {
        public DataBaseSettings MainDB { get; set; }
        public DataBaseSettings EventDB { get; set;}
    }
}
