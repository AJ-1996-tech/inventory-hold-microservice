using InventoryHold.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Infrastructure.Mongo
{
    public class SeedData
    {
        public static async Task InitializeAsync(MongoDbContext context)
        {
            var collection = context.Inventory;

            var count = await collection.CountDocumentsAsync(Builders<InventoryItem>.Filter.Empty);

            if (count > 0) return;

            var items = new List<InventoryItem>
        {
            new InventoryItem { Id = "1", ProductName = "Laptop", Quantity = 10 },
            new InventoryItem { Id = "2", ProductName = "Phone", Quantity = 20 },
            new InventoryItem { Id = "3", ProductName = "Headphones", Quantity = 15 },
            new InventoryItem { Id = "4", ProductName = "Keyboard", Quantity = 25 },
            new InventoryItem { Id = "5", ProductName = "Mouse", Quantity = 30 }
        };

            await collection.InsertManyAsync(items);
        }
    }
}
