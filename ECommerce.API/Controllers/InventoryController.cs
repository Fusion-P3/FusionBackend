using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService service, ILogger<InventoryController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<List<InventoryDTO>>> GetInventoryAsync(Guid userId)
        {
            List<InventoryDTO> inv = await _service.GetInventoryAsync(userId);
            return Ok(inv);
        }


        [HttpPost("{userId}/create/{productId}")]
        public async Task<ActionResult> CreateInventoryItem(Guid userId, Guid productId, [FromBody] int quantity)
        {
            await _service.CreateInventoryItem(userId, productId, quantity);
            return Ok();
        }

        [HttpPut("{userId}/update/{productId}")]
        public async Task<ActionResult> UpdateInventoryItem(Guid userId, Guid productId, [FromBody] int diff)
        {
            await _service.UpdateInventoryItem(userId, productId, diff);
            return Ok();
        }
    }
}