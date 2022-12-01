using ECommerce.Data;
using ECommerce.Data.Entities;
using ECommerce.Models;

namespace ECommerce.Service;

public class CartService : ICartService
{
    private readonly IRepository _repo;
    private readonly ProductService _pservice;

    public CartService(IRepository repo)
    {
        _repo = repo;
        _pservice = new ProductService(repo);
    }

    public Cart GetCartByUserId(Guid user_id)
    {
        List<CartItem> items = _repo.GetCartItemsByUserId(user_id);
        List<Models.Product> products = _pservice.GetAllProducts();
        Cart cart = new();
        cart.cartCount = items.Count;
        cart.totalPrice = 0;
        foreach (CartItem item in items)
        {
            cart.products.Add(new ProductInternal
            {
                quantity = item.Quantity,
                product = products.Find(x => x.id == item.ProductId)
            });
        }
        return cart;
    }

    public async Task<CartDto> UpdateOrCreateCart(CartDto cart)
    {
        await _repo.ClearCart(cart.userId);
        foreach (Models.ProductInternal p in cart.cart!.products)
        {
            CartItem item = new();
            item.Id = Guid.NewGuid();
            item.ProductId = p.product!.id;
            item.Quantity = p.quantity;
            item.UserId = cart.userId;
            await _repo.AddCartItem(item);
        }
        return cart;
    }
}