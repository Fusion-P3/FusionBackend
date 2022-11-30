using ECommerce.Service;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;

namespace ECommerce.API.Controllers;

[ApiController]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;
    private readonly ICartService _service;

    public CartController(ICartService service, ILogger<CartController> logger)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("api/cart/{user_id}")]
    public ActionResult<Cart> GetCart(Guid user_id)
    {
        Cart ret = _service.GetCartByUserId(user_id);
        if (ret.cartCount != -1)
        {
            _logger.LogInformation("Sending cart for user");
            return Ok(ret);
        }
        _logger.LogError("Unable to send user cart");
        return BadRequest("Unable to send cart... user does not exist");
    }

    [HttpPut]
    [Route("api/cart")]
    public ActionResult<CartDto> SetCart(CartDto cart)
    {
        CartDto dto = _service.UpdateOrCreateCart(cart);
        if (dto.userId != Guid.Empty)
        {
            _logger.LogInformation("Cart Updated");
            return Ok(dto);
        }
        _logger.LogError("Unable to update cart");
        return BadRequest("Unable to update cart");
    }
}