

using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using ECommerce.Service;

namespace ECommerce.API.Controllers;

[ApiController]
public class CheckoutController : ControllerBase
{
    private readonly ILogger<CheckoutController> _logger;
    private readonly ICheckoutService _service;

    public CheckoutController(ICheckoutService service, ILogger<CheckoutController> logger)
    {
        this._logger = logger;
        this._service = service;
    }

    [HttpPost]
    [Route("checkout")]
    public async Task<ActionResult<CheckoutDTO>> CheckoutAsync(CheckoutDTO checkout)
    {
        CheckoutDTO ret = await _service.CheckoutAsync(checkout);
        if (ret.user_id != Guid.Empty)
        {
            _logger.LogInformation("Checkout completed!");
            return Ok(ret);
        }
        _logger.LogError("Unable to complete checkout");
        return BadRequest("Unable to complete checkout");
    }

}

