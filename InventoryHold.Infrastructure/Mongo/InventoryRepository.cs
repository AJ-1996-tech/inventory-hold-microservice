using InventoryHold.Domain.Entities;
using InventoryHold.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Infrastructure.Mongo
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoCollection<InventoryItem> _collection;

        public InventoryRepository(MongoDbContext context)
        {
            _collection = context.Inventory;
        }

        public async Task<bool> DeductStockAsync(string productId, int qty)
        {
            var filter = Builders<InventoryItem>.Filter.And(
                Builders<InventoryItem>.Filter.Eq(x => x.Id, productId),
                Builders<InventoryItem>.Filter.Gte(x => x.Quantity, qty));

            var update = Builders<InventoryItem>.Update.Inc(x => x.Quantity, -qty);

            var result = await _collection.FindOneAndUpdateAsync(filter, update);

            return result != null;
        }

        public async Task RestoreStockAsync(string productId, int qty)
        {
            var filter = Builders<InventoryItem>.Filter.Eq(x => x.Id, productId);
            var update = Builders<InventoryItem>.Update.Inc(x => x.Quantity, qty);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<List<InventoryItem>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
