using DAL2Service.DAL.Mongo.Interfaces;

namespace DAL2Service.DAL.Mongo.Settings
{
    public class MultipleDataBaseSettings : IMultipleDataBaseSettings
    {
        public DataBaseSettings MainDB { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DataBaseSettings EventDB { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
