using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Project1.CustomExceptions;
using Project1.Models;
using Project1.MongoDB.Repository.Interfaces;
using Service.DataAccess.MongoDB;

namespace Project1.MongoDB.Repository.Implementation
{
    public class SubscriberRepository : MongoRepository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository ( AppDbContext dbContext ) : base ( dbContext, "subscribers")
        {
            
        }


        //public override async Task UpdateAsync ( Subscriber entity )
        //{
        //    try
        //    {

        //        var filter = Builders<BsonDocument>.Filter.Eq ( "_id", entity.Id ).ToBsonDocument();
        //        var update = Builders<BsonDocument>.Update.Set ( "Credit", entity.Credit )
        //                                                  .Set("MobileNumber" ,entity.MobileNumber)
        //                                                  .Set("Services", entity.Services).ToBsonDocument();
        //        await _collection.UpdateOneAsync ( filter, update,new UpdateOptions () { IsUpsert = true} );
        //    }
        //    catch ( MongoException exception )
        //    {
        //        throw new DatabaseException ( "Updating entity in DB failed", exception );
        //    }
        //}
    }
}
