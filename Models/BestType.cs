using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectCafeApi.Models
{
    public class BestType
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalAmount { get; set; }
        public int TotalPrice { get; set; }
        public float Rate { get; set; }
    }
}

