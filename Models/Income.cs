using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProjectCafeApi.Models
{
    public class Income
    {
        [BsonId]
        public string Income_id { get; set; }
        public string Inc { get; set; }
        public string Day { get; set; }
    }
}