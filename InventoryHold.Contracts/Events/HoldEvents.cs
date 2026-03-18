using InventoryHold.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryHold.Contracts.Events
{
    public record HoldCreatedEvent(string HoldId, List<HoldItemDto> Items, DateTime Expiry);
    public record HoldReleasedEvent(string HoldId);
    public record HoldExpiredEvent(string HoldId);
}
