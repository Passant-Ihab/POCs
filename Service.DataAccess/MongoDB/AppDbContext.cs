using System;
using System.Collections.Generic;
using System.Text;
using Common.Interfaces;
using MongoDB.Driver;
using Project1.Models;

namespace Service.DataAccess.MongoDB
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _db;

        public AppDbContext ( IMongoClient client, string dbName )
        {
            _db = client.GetDatabase ( dbName );
        }

        public IMongoCollection<Subscriber> Subscribers => _db.GetCollection<Subscriber> ( "subscribers" );
        public IMongoCollection<Project1.Models.Service> Services => _db.GetCollection<Project1.Models.Service> ( "services" );

        public IMongoCollection<T> GetCollectionByName<T> (string collectionName) where T : IDBEntity
        {
            return _db.GetCollection<T> ( collectionName );
        }
    }
}
