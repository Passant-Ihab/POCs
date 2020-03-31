using MongoDB.Driver;

using Project1.Models;
using Project1.MongoDB.Repository.Interfaces;

namespace Project1.MongoDB.Repository.Implementation
{
    public class SubscriberRepository : MongoRepository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository ( IMongoClient mongoClient ) : base ( mongoClient, "Subscriber", "Training" )
        {

        }

        //public async Task<List<ServiceSubscribtion>> GetAllUserServiceSubscriptionsAsync ( Guid id )
        //{
        //    try
        //    {
        //       return await _collection.FindAsync ( s => s.Id == id ).Result.FirstOrDefaultAsync ().Result.Services;


        //    }
        //    catch ( MongoException exception )
        //    {
        //        throw new DatabaseException ( "Getting data from DB failed", exception );
        //    }

        //}
    }
}
