using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LogoManager.DAL.Mongo.Entities
{
    public class LogoMessageEntity
    {
        [BsonId]
        public int Pid { get; set; }
        public string message { get; set; }
        public DateTime time { get; set; }
        public string clientInfo { get; set; }
    }
}
