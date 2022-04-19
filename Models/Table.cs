using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectCafeApi.Models
{
    public class Table
    {
        [BsonId]
        public string Table_id { get; set; }
        public string Table_NO { get; set; }
        public string Status { get; set; }
        public string Qrcode { get; set; }
    }
}