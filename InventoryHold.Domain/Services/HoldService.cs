using InventoryHold.Contracts.DTOs;
using InventoryHold.Contracts.Events;
using InventoryHold.Domain.Entities;
using InventoryHold.Domain.Interfaces;
using InventoryHold.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Domain.Services
{
    public class HoldService
    {
        private readonly IInventoryRepository _inventory;
        private readonly IHoldRepository _hold;
        private readonly IMessagePublisher _publisher;
        private readonly ICacheService _cache;

        public HoldService(
            IInventoryRepository inventory,
            IHoldRepository hold,
            IMessagePublisher publisher,
            ICacheService cache)
        {
            _inventory = inventory;
            _hold = hold;
            _publisher = publisher;
            _cache = cache;
        }

        public async Task<Hold> CreateHoldAsync(List<HoldItem> items)
        {
            foreach (var item in items)
            {
                var success = await _inventory.DeductStockAsync(item.ProductId, item.Quantity);
                if (!success)
                    throw new Exception($"Insufficient inventory for {item.ProductId}");
            }

            var hold = new Hold
            {
                Id = Guid.NewGuid().ToString(),
                Items = items,
                Expiry = DateTime.UtcNow.AddMinutes(15),
                IsReleased = false
            };

            await _hold.CreateAsync(hold);

            await _publisher.PublishAsync(new HoldCreatedEvent(
                hold.Id,
                items.Select(x => new HoldItemDto
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList(),
                hold.Expiry));

            await _cache.RemoveAsync("inventory:all");

            return hold;
        }

        public async Task<Hold> GetHoldAsync(string id)
        {
            var hold = await _hold.GetByIdAsync(id);
            if (hold == null) throw new Exception("Hold not found");

            if (hold.IsExpired())
                throw new Exception("Hold expired");

            return hold;
        }

        public async Task ReleaseHoldAsync(string id)
        {
            var hold = await _hold.GetByIdAsync(id);
            if (hold == null) throw new Exception("Not found");

            if (hold.IsReleased) return;

            foreach (var item in hold.Items)
            {
                await _inventory.RestoreStockAsync(item.ProductId, item.Quantity);
            }

            hold.IsReleased = true;

            await _hold.UpdateAsync(hold);

            await _publisher.PublishAsync(new HoldReleasedEvent(id));

            await _cache.RemoveAsync("inventory:all");
        }
    }
}
