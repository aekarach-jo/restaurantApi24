using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectCafeApi.Models
{
    public class Promotion
    {
        [BsonId]
        public string Promotion_id { get; set; }
        public string Promotion_name { get; set; }
        public Boolean Status { get; set; }
        public string Detail { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
    }
}