using System;
using System.Collections.Generic;

namespace Project1
{

    public interface ISubscriberEntity : IDBEntity<Guid>
    {
        string MobileNumber { set; get; }
        Dictionary<int , bool> ServiceStatus { set; get; }
        bool IsBlocked { set; get; }
        decimal credit { set; get; }
    }
}
