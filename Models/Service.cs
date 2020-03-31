using System;
using System.Collections.Generic;
using Project1.MongoDB.Interfaces;

namespace Project1.Models
{
    public class Service : IMongoEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
        public List<string> OptInKeyWord { get; set; }
        public List<string> OptOutKeyWord { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
