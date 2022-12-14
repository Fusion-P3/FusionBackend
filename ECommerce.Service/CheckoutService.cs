using ECommerce.Models;
using ECommerce.Data;
using ECommerce.Data.Entities;

namespace ECommerce.Service;

public class CheckoutService : ICheckoutService
{
    private readonly IRepository _repo;
    private readonly IInventoryService _inventoryService;

    public CheckoutService(IRepository repo)
    {
        _inventoryService = new InventoryService(repo);
        _repo = repo;
    }

    public async Task<CheckoutDto> CheckoutAsync(CheckoutDto checkout)
    {
        List<CartItem> items = _repo.GetCartItemsByUserId(checkout.user_id);
        OrderDetail detail = new OrderDetail
        {
            Id = Guid.NewGuid(),
            UserId = checkout.user_id,
            OrderDate = DateTime.Now,
            Amount = 0,
            OrderItems = new List<OrderItem>()
        };
        foreach (CartItem item in items)
        {
            Data.Entities.Product p = _repo.SubtractProductQuantity(item.ProductId, item.Quantity);
            detail.Amount += p.ProductPrice * item.Quantity;
            detail.OrderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                Product = item.Product,
                Quantity = item.Quantity
            });
            bool updated = await _repo.UpdateInventoryItem(checkout.user_id, item.ProductId!.Value, item.Quantity!.Value);
            if (!updated)
            {
                await _inventoryService.CreateInventoryItem(checkout.user_id, item.ProductId.Value, item.Quantity.Value);
            }
        }
        bool check = await _repo.UpdateInventoryItem(checkout.user_id, Guid.Parse("33914a4d-5e85-48cf-a443-e70672eb07d0"), -1 * detail.Amount.Value);
        if (!check)
        {
            return new CheckoutDto { user_id = Guid.Empty };
        }
        OrderDetail ret = await _repo.AddOrderDetailsAsync(detail);
        if (ret.UserId == Guid.Empty)
        {
            return new CheckoutDto { user_id = Guid.Empty };
        }
        await _repo.ClearCart(checkout.user_id);
        return checkout;
    }



}