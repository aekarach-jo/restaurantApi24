using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProjectCafeApi.Models
{
    public class Food
    {
        [BsonId]
        public string Food_id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Typeid { get; set; }
        public int Price { get; set; }
        public string imgPath { get; set; }
        public Boolean Status { get; set; }
        public int Amount { get; set; }
    }
}