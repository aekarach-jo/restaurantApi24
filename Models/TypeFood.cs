using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectCafeApi.Models
{
    public class TypeFood
    {
        [BsonId]
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
    }
}