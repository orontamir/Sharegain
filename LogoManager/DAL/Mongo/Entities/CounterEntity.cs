using MongoDB.Bson.Serialization.Attributes;

namespace LogoManager.DAL.Mongo.Entities
{
    public class CounterEntity
    {
        [BsonId]
        public string Id { get; set; }
        public int Value { get; set; }
    }
}
