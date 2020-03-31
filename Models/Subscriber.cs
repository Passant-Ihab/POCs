using System;
using System.Collections.Generic;
using Project1.MongoDB.Interfaces;

namespace Project1.Models
{
    public class Subscriber : IMongoEntity
    {
        public Guid Id { get; set; }
        public string MobileNumber { get; set; }
        public decimal Credit { get; set; }
        public List<ServiceSubscribtion> Services { get ; set ; }
        public DateTime CreatedDate { get; set; }
    }
}
