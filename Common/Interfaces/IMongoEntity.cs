using System;
using Common.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace Project1.MongoDB.Interfaces
{
    public interface IMongoEntity : IDBEntity
    {
        [BsonId]
        Guid Id { get; set; }
    }
}
