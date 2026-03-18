using InventoryHold.Contracts.DTOs;
using InventoryHold.Domain.Services;
using InventoryHold.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHold.WebApi.Controllers
{
    [ApiController]
    [Route("api/holds")]
    public class HoldsController : ControllerBase
    {
        private readonly HoldService _service;

        public HoldsController(HoldService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(HoldRequestDto dto)
        {
            var hold = await _service.CreateHoldAsync(
                dto.Items.Select(x => new HoldItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList());

            return Ok(new HoldResponseDto
            {
                HoldId = hold.Id,
                Expiry = hold.Expiry
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var hold = await _service.GetHoldAsync(id);
            return Ok(hold);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.ReleaseHoldAsync(id);
            return NoContent();
        }
    }
}
