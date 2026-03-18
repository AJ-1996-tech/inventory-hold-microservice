using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Domain.ValueObjects
{
    public class HoldItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
