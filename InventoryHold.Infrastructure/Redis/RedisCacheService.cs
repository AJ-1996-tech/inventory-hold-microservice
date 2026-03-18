using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Infrastructure.Redis
{
    using InventoryHold.Domain.Interfaces;
    using StackExchange.Redis;
    using System.Text.Json;

    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (_db == null)
            {
                Console.WriteLine("Redis not available - skipping cache");
                return default;
            }

            var value = await _db.StringGetAsync(key);
            return value.IsNull ? default : JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
        {
            if (_db == null)
            {
                Console.WriteLine("Redis not available - skipping cache");
                return;
            }
            await _db.StringSetAsync(key, JsonSerializer.Serialize(value), ttl);
        }

        public async Task RemoveAsync(string key)
        {
            if (_db == null)
            {
                Console.WriteLine("Redis not available - skipping cache");
                return;
            }
            await _db.KeyDeleteAsync(key);
        }
    }
}
