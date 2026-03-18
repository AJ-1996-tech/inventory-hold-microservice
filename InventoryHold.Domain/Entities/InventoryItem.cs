using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Domain.Entities
{
    public class InventoryItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
