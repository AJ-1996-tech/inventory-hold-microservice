using InventoryHold.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Domain.Interfaces
{
    public interface IHoldRepository
    {
        Task CreateAsync(Hold hold);
        Task<Hold> GetByIdAsync(string id);
        Task UpdateAsync(Hold hold);
        Task<List<Hold>> GetExpiredHoldsAsync();
    }
}
