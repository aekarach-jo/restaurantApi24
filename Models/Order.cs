using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectCafeApi.Models
{
    public class Order
    {
        [BsonId]
        public string Order_id { get; set; }
        public string Table_NO { get; set; }
        public string TypeOrder { get; set; }
        public int Number { get; set; }
        public int PriceTotal { get; set; }
        public Food[] FoodList { get; set; }
        public string Status { get; set; }
        public string Promotion { get; set; }
        public DateTime? CreationDatetime { get; set; }


        public string Emp_name { get; set; }
        public DateTime? Paytime { get; set; }
        public int NetPrice { get; set; }
        public int ValuePromotion { get; set; }
    }    
}