using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Volare.Enums;

namespace Volare.Models
{
    public class MongoViewModel
    {
        [BsonId()]
        public ObjectId IdMongo { get; set; }

        [BsonElement("recvTime")]
        public DateTime DataRegistro { get; set; }

        [BsonElement("attrValue")]
        public int ValorId { get; set; }

        [BsonElement("attrName")]
        public string Tipo { get; set; }

        [BsonElement("attrType")]
        public string TipoDado { get; set; }
    }
}