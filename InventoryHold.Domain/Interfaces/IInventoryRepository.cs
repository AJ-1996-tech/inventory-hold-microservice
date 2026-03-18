using InventoryHold.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        Task<bool> DeductStockAsync(string productId, int qty);
        Task RestoreStockAsync(string productId, int qty);
        Task<List<InventoryItem>> GetAllAsync();
    }
}
