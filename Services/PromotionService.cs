using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using ProjectCafeApi.Models;

namespace ProjectCafeApi.Services
{
    public class PromotionService
    {
        private readonly IMongoCollection<Promotion> _promotion;
        public PromotionService(DatabaseSettings settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            _promotion = database.GetCollection<Promotion>(settings.PromotionCollection);
        }
        public Promotion CreatePromotion(Promotion promotion)
        {
             _promotion.InsertOne(promotion);
             return promotion;
        }

        public List<Promotion> GetPromotionForManage() => _promotion.Find(pro => true).ToList();
        public List<Promotion> GetPromotionForShow() => _promotion.Find(pro => pro.Status == true).ToList();
        public Promotion GetPromotionById(string id) => _promotion.Find(pro => pro.Promotion_id == id).FirstOrDefault();
        public void UpdatePromotion(string id, Promotion promotion) => _promotion.ReplaceOne(pro => pro.Promotion_id == id, promotion);
    }
}