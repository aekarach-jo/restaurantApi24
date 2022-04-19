using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using ProjectCafeApi.Models;

namespace ProjectCafeApi.Services
{
    public class TableService
    {
        private readonly IMongoCollection<Table> _table;
        public TableService(DatabaseSettings settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            _table = database.GetCollection<Table>(settings.TableCollection);
        }
        public Table CreatedTable(Table table)
        {
             _table.InsertOne(table);
             return table;
        }

        public List<Table> GetTableAll() => _table.Find(tb => tb.Status != "deleted").ToList();
        public List<Table> GetTableAllForApi() => _table.Find(tb => true).ToList();
        public Table GetTableByNo(string tableNo) => _table.Find(tb => tb.Table_NO == tableNo).FirstOrDefault();
        public Table GetTableById(string id) => _table.Find(tb => tb.Table_id == id).FirstOrDefault();
        public void UpdateTable(string id, Table table) => _table.ReplaceOne(tb => tb.Table_id == id, table);
    }
}