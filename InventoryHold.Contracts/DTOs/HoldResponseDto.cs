using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Contracts.DTOs
{
    public class HoldResponseDto
    {
        public string HoldId { get; set; }
        public DateTime Expiry { get; set; }
    }
}
