using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Contracts.DTOs
{
    public class HoldRequestDto
    {
        public List<HoldItemDto> Items { get; set; }
    }

    public class HoldItemDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
