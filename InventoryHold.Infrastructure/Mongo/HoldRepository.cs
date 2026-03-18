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
    public class HoldRepository : IHoldRepository
    {
        private readonly IMongoCollection<Hold> _collection;

        public HoldRepository(MongoDbContext context)
        {
            _collection = context.Holds;
        }

        public async Task CreateAsync(Hold hold)
        {
            await _collection.InsertOneAsync(hold);
        }

        public async Task<Hold> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Hold hold)
        {
            await _collection.ReplaceOneAsync(x => x.Id == hold.Id, hold);
        }

        public async Task<List<Hold>> GetExpiredHoldsAsync()
        {
            return await _collection
                .Find(x => !x.IsReleased && x.Expiry < DateTime.UtcNow)
                .ToListAsync();
        }
    }
}
