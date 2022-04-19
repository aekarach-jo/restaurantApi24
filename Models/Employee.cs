using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ProjectCafeApi.Models
{
    public class Employee
    {
        [BsonId]
        public string Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Tel { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
    }
}