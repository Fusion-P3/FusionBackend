using ECommerce.Models;

namespace ECommerce.Service;

public interface ICheckoutService
{
    public Task<CheckoutDto> CheckoutAsync(CheckoutDto checkout);
}