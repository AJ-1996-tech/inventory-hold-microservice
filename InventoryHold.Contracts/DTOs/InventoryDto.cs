using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Contracts.DTOs
{
    public class InventoryDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
