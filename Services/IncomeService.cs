using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using ProjectCafeApi.Models;

namespace ProjectCafeApi.Services
{
    public class IncomeService
    {
        private readonly IMongoCollection<Income> _income;
        public IncomeService(DatabaseSettings settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            _income = database.GetCollection<Income>(settings.IncomeCollection);
        }
        public Income CreateIncome(Income income)
        {
             _income.InsertOne(income);
             return income;
        }

        public List<Income> GetIncomeAll() => _income.Find(emp => true).ToList();
    }
}
