using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using ProjectCafeApi.Models;

namespace ProjectCafeApi.Models
{
    public class FoodService
    {
        private readonly IMongoCollection<Food> _food;
        public FoodService(DatabaseSettings settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            _food = database.GetCollection<Food>(settings.FoodCollection);
        }
        public Food CreateFood(Food food)
        {
             _food.InsertOne(food);
             return food;
        }

        public List<Food> GetAllForApi() => _food.Find(food => true).ToList();
        public List<Food> GetFoodAll() => _food.Find(food => true).ToList();
        public Food GetFoodById(string id) => _food.Find(food => food.Food_id == id).FirstOrDefault();

        public void UpdateFood(string id, Food food) => _food.ReplaceOne(food => food.Food_id == id, food);
    }
}