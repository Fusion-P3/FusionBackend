using ECommerce.Models;

namespace ECommerce.Service;

public interface ICartService
{
    Cart GetCartByUserId(Guid user_id);
    CartDto UpdateOrCreateCart(CartDto cart);
}