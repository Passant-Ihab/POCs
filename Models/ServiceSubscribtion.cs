using System;

namespace Project1.Models
{
    public class ServiceSubscribtion
    {
        public Guid ServiceId { set; get; }
        public bool IsPending { set; get; }
        public bool IsActive { set; get; }
        public DateTime SubscribtionDate { set; get; }
        public DateTime UnsubscribtionDate { set; get; }
    }
}
