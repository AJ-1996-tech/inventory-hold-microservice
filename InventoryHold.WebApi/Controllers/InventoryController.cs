using InventoryHold.Domain.Entities;
using InventoryHold.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryHold.WebApi.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _repo;
        private readonly ICacheService _cache;

        public InventoryController(IInventoryRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cache = await _cache.GetAsync<List<InventoryItem>>("inventory:all");

            if (cache != null) return Ok(cache);

            var data = await _repo.GetAllAsync();

            await _cache.SetAsync("inventory:all", data, TimeSpan.FromSeconds(60));

            return Ok(data);
        }
    }
}
