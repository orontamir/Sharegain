namespace DAL2Service.DAL.Mongo.Interfaces
{
    public interface IDataBaseSettings
    {
        string ConnectionString { get; set; }
        string CollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}
