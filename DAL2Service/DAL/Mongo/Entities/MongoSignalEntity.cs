using MongoDB.Bson.Serialization.Attributes;

namespace DAL2Service.DAL.Mongo.Entities
{
    public class MongoSignalEntity
    {
        [BsonId]
        public int Pid { get; set; }
        public DateTime TimeStemp { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public double Value { get; set; }
        public string Type { get; set; }
        public bool Error { get; set; }
    }
}
