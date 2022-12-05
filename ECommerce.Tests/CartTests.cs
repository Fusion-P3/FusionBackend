using ECommerce.Data;
using ECommerce.Models;
using ECommerce.Service;
using ECommerce.Data.Entities;
using Moq;

namespace Tests;

public class CartTests
{
    [Fact]
    public void CartCanBeCreated()
    {
        Cart cart = new();
        Assert.NotNull(cart);
    }

    [Fact]
    public void CartDtoCanBeCreated()
    {
        CartDto dto = new();
        Assert.NotNull(dto);
    }

    [Fact]
    public void ProductInternalCreates()
    {
        ProductInternal inter = new();
        Assert.NotNull(inter);
    }

    public static Guid idA = Guid.NewGuid(), idB = Guid.NewGuid();
    public static CartService CreateService()
    {
        var repo = new Mock<IRepository>();

        repo.Setup(repo => repo.GetCartItemsByUserId(It.IsAny<Guid>())).Returns((Guid id) =>
        {
            return new List<CartItem> {new CartItem{
                UserId = id,
                ProductId = idA,
                Quantity = 1,
                Product = new()
            }, new CartItem{
                UserId = id,
                ProductId = idB,
                Quantity = 40,
                Product = new()
            }};
        });
        repo.Setup(repo => repo.GetAllProducts()).Returns(() =>
        {
            return new List<ECommerce.Data.Entities.Product> {
                new ECommerce.Data.Entities.Product{Id = idA}, new ECommerce.Data.Entities.Product{Id = idB}
            };
        });

        return new CartService(repo.Object);
    }

    [Fact]
    public async Task CartServiceWorks()
    {
        CartService service = CreateService();
        Cart cart = service.GetCartByUserId(idA);
        Assert.NotNull(cart);
        CartDto dto = await service.UpdateOrCreateCart(new CartDto
        {
            userId = idA,
            cart = cart
        });
        Assert.NotNull(dto);
    }
}