using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Infrastructure.Mongo
{
    using InventoryHold.Domain.Entities;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;

    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config["Mongo:ConnectionString"]);
            _db = client.GetDatabase(config["Mongo:Database"]);
        }

        public IMongoCollection<InventoryItem> Inventory =>
            _db.GetCollection<InventoryItem>("inventory");

        public IMongoCollection<Hold> Holds =>
            _db.GetCollection<Hold>("holds");
    }
}
