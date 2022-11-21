using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IRepository repo, ILogger<ProductController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetOne(int id)
        {
            _logger.LogInformation("api/product/{id} triggered");
            try
            {
                _logger.LogInformation("api/product/{id} completed successfully");
                return Ok(await _repo.GetProductByIdAsync(id));
            }
            catch
            {
                _logger.LogWarning("api/product/{id} completed with errors");
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<Product[]>> GetAll()
        {
            _logger.LogInformation("api/product triggered");
            try
            {
                _logger.LogInformation("api/product completed successfully");
                return Ok(await _repo.GetAllProductsAsync());
            }
            catch
            {
                _logger.LogWarning("api/product completed with errors");
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<ActionResult<Product[]>> Purchase([FromBody] ProductDTO[] purchaseProducts)
        {
            _logger.LogInformation("PATCH api/product triggered");
            List<Product> products = new List<Product>();
            try
            {
                foreach(ProductDTO item in purchaseProducts)
                {
                    Product tmp = await _repo.GetProductByIdAsync(item.id);
                    if ((tmp.quantity - item.quantity) >= 0)
                    {
                        await _repo.ReduceInventoryByIdAsync(item.id, item.quantity);
                        products.Add(await _repo.GetProductByIdAsync(item.id));
                    }
                    else
                    {
                        throw new Exception("Insuffecient inventory.");
                    }
                }
                _logger.LogInformation("PATCH api/product completed successfully");
                return Ok(products);
            }
            catch
            {
                _logger.LogWarning("PATCH api/product completed with errors");
                return BadRequest();

            }


        }

    }
}
