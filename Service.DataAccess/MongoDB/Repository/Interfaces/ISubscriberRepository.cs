
using System.Threading.Tasks;
using Project1.Models;
using Project1.MongoDB.Repository.Implementation;

namespace Project1.MongoDB.Repository.Interfaces
{
    public interface ISubscriberRepository : IMongoRepository<Subscriber>
    {
        
    }
}
