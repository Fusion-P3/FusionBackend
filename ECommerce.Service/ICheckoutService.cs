using ECommerce.Models;

namespace ECommerce.Service;

public interface ICheckoutService
{
    public Task<CheckoutDTO> CheckoutAsync(CheckoutDTO checkout);
}