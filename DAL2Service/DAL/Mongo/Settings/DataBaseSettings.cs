using DAL2Service.DAL.Mongo.Interfaces;

namespace DAL2Service.DAL.Mongo.Settings
{
    public class DataBaseSettings : IDataBaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
