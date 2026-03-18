using InventoryHold.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Domain.Entities
{
    public class Hold
    {
        public string Id { get; set; }
        public List<HoldItem> Items { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsReleased { get; set; }

        public bool IsExpired() => DateTime.UtcNow > Expiry;
    }
}
