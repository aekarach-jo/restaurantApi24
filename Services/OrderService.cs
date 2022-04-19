using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using ProjectCafeApi.Models;

namespace ProjectCafeApi.Services
{   
    public class OrderService
    {
        private readonly IMongoCollection<Order> _order;
        public OrderService(DatabaseSettings settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            _order = database.GetCollection<Order>(settings.OrderCollection);
        }
        public Order CreateOrder(Order order)
        {
             _order.InsertOne(order);
             return order;
        }

        public List<Order> GetOrderAll() => _order.Find(emp => true).ToList();
        public List<Order> GetOrderSuccess() => _order.Find(or => or.Status == "success").ToList();
        public List<Order> GetOrderSuccessByMonth(int date) => _order.Find(or => or.CreationDatetime.Value.Month == date).ToList();

        public Order GetById(string id) => _order.Find(emp => emp.Order_id == id).FirstOrDefault();
        public List<Order> GetAllForApi() => _order.Find(emp => true).ToList();

        public void UpdateOrder(string id, Order order) => _order.ReplaceOne<Order>(or => or.Order_id == id, order);
    }
}