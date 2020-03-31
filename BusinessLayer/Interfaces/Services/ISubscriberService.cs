using System.Threading.Tasks;

using Models;

namespace BusinessLayer.Interfaces.Services
{
    public interface ISubscriberService
    {
        Task ProccessMessage (MessageModelRequest message);
        //void Subscribe ( Subscriber newSubscribtion );
        //void UnSubscribe ( Subscriber subscribtion );
        //void UpdatePendingStatus ( Subscriber pendingSubscribtion );
    }
}
