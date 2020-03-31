using System;
using System.Collections.Generic;

namespace Project1
{
    public class SubscriberEntity : ISubscriberEntity
    {
        public Guid Id { get; set; }
        public string MobileNumber { set; get; }
        public Dictionary<int, bool> ServiceStatus { set; get; }
        public bool IsBlocked { set; get; }
        public decimal credit { set; get; }
    }
}
